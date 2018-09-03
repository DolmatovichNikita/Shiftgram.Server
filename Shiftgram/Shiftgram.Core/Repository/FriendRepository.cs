using System.Data.Entity;
using System.Threading.Tasks;
using Shiftgram.Core.Enums;
using Shiftgram.Core.Exceptions;
using Shiftgram.Core.Models;

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

		public async Task<DbAnswerCode> Delete(int id)
		{
			var dbEntry = await this._context.Friends.FirstOrDefaultAsync(x => x.AccountBId == id);

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
	}
}
