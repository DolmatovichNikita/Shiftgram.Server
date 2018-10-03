using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Shiftgram.Core.Enums;
using Shiftgram.Core.Exceptions;
using Shiftgram.Core.Models;
using Shiftgram.Core.ViewModels;
using Shiftgram.Core.Views;

namespace Shiftgram.Core.Repository
{
	public class FriendRepository : IFriendRepository
	{
		private ShiftgramContext _context;
		private ViewCreator creator;

		public FriendRepository()
		{
			this._context = new ShiftgramContext();
			this.creator = new FriendViewCreator();
		}

		public async Task<int> Add(Friend item)
		{
			if (item.AccountAId != item.AccountBId)
			{
				var model = await this._context.Friends.FirstOrDefaultAsync<Friend>(x => x.AccountAId == item.AccountAId && x.AccountBId == item.AccountBId);
				if (model == null)
				{
					this._context.Friends.Add(item);
					int rows = await this._context.SaveChangesAsync();

					if (rows > 0)
					{
						return item.Id;
					}
				}
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

		public async Task<IEnumerable<AccountFriendViewModel>> GetFriends(int accountAId)
		{
			var view = this.creator.CreateView() as FriendView;
			return await view.GetFriends(accountAId);
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
