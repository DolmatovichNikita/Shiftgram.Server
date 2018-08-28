using System;
using System.Collections.Generic;
using System.Data.Entity;
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

		public Task<Account> GetByPhone(string phone)
		{
			throw new NotImplementedException();
		}

		public async Task<DbAnswerCode> Update(AccountUpdateViewModel model)
		{
			return await this.Updatable.Update(model);
		}
	}
}
