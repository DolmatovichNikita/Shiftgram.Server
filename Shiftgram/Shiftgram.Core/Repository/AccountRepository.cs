using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Shiftgram.Core.Enums;
using Shiftgram.Core.Exceptions;
using Shiftgram.Core.Models;
using Shiftgram.Core.Strategy;

namespace Shiftgram.Core.Repository
{
	public class AccountRepository : IAccountRepository
	{
		private ShiftgramContext _context;

		public AccountRepository()
		{
			this._context = new ShiftgramContext();
		}

		public ShiftgramContext Context => this._context;

		public IUpdatableAccount Updatable { get; set; }

		public async Task<int> Add(Account item)
		{
			this._context.Accounts.Add(item);
			int rows = await this._context.SaveChangesAsync();

			if(rows > 0)
			{
				await this.CreateFriendView(item.Id);
				return item.Id;
			}

			throw new AccountException();
		}

		public async Task<DbAnswerCode> Delete(int id)
		{
			var dbEntry = await this._context.Accounts.FirstOrDefaultAsync<Account>(x => x.Id == id);

			if(dbEntry != null)
			{
				this._context.Accounts.Remove(dbEntry);
				int rows = await this._context.SaveChangesAsync();

				if(rows > 0)
				{
					return DbAnswerCode.Ok;
				}
			}

			return DbAnswerCode.Bad;
		}

		public async Task<IEnumerable<Account>> GetAll()
		{
			return await this._context.Accounts.ToListAsync<Account>();
		}

		public async Task<Account> GetById(int id)
		{
			var dbEntry = await this._context.Accounts.FirstOrDefaultAsync<Account>(x => x.Id == id);

			if(dbEntry != null)
			{
				return dbEntry;
			}

			throw new AccountException();
		}

		public async Task<Account> GetByPhone(string phone)
		{
			var dbEntry = await this._context.Accounts.FirstOrDefaultAsync<Account>(x => x.Phone == phone);

			if(dbEntry != null)
			{
				return dbEntry;
			}

			throw new AccountException();
		}

		public async Task<DbAnswerCode> Update(AccountUpdateViewModel model)
		{
			return await this.Updatable.Update(model);
		}

		private async Task CreateFriendView(int id)
		{
			string strConnection = "data source=aarsp4er1pxxe9.coqmx0efxhue.eu-central-1.rds.amazonaws.com,1433;initial catalog=Shiftgram;user id=shiftgram;password=7757739n;multipleactiveresultsets=True;application name=EntityFramework";

			using (SqlConnection connection = new SqlConnection(strConnection))
			{
				await connection.OpenAsync();
				using (SqlCommand command = new SqlCommand("CreateFriendView", connection))
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.Parameters.Add(new SqlParameter { ParameterName = "@NameView", Value = "Account" + id });
					command.Parameters.Add(new SqlParameter { ParameterName = "@AccountId", Value = id });

					await command.ExecuteNonQueryAsync();
				}

				connection.Close();
			}
		}
	}
}
