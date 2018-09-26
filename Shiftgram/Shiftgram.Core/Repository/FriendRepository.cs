using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Shiftgram.Core.Enums;
using Shiftgram.Core.Exceptions;
using Shiftgram.Core.Models;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;
using System.Configuration;

namespace Shiftgram.Core.Repository
{
	public class FriendRepository : IFriendRepository
	{
		private ShiftgramContext _context;

		public FriendRepository()
		{
			this._context = new ShiftgramContext();
		}

		public async Task<int> Add(Friend item)
		{
			this._context.Friends.Add(item);
			int rows = await this._context.SaveChangesAsync();

			if(rows > 0)
			{
				return item.Id;
			}

			throw new AccountException();
		}

		public async Task<DbAnswerCode> Delete(int accountAId, int accountBId)
		{
			var dbEntry = await this._context.Friends.FirstOrDefaultAsync(x => x.AccountBId == accountBId && x.AccountAId == accountAId);

			if(dbEntry != null)
			{
				this._context.Friends.Remove(dbEntry);
				int rows = await this._context.SaveChangesAsync();

				if(rows > 0)
				{
					return DbAnswerCode.Ok;
				}
			}

			return DbAnswerCode.Bad;
		}

		public async Task<Friend> GetById(int id)
		{
			var dbEntry = await this._context.Friends.FirstOrDefaultAsync(x => x.Id == id);

			if(dbEntry != null)
			{
				return dbEntry;
			}

			throw new AccountException();
		}

		public async Task<IEnumerable<Account>> GetFriends(int accountAId)
		{
			var friends = await this._context.Friends.Where(x => x.AccountAId == accountAId).Select(x => x.AccountBId).ToListAsync();
			var accounts = await this._context.Accounts.Where(x => friends.Contains(x.Id)).ToListAsync();

			return accounts;
		}

		private async Task<bool> IsExistAccountA(int id)
		{
			var dbEntry = await this._context.Friends.FirstOrDefaultAsync(x => x.AccountAId == id);

			if(dbEntry != null)
			{
				return true;
			}

			return false;
		}
	}
}
