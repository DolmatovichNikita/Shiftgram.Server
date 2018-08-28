using System.Data.Entity;
using System.Threading.Tasks;
using Shiftgram.Core.Enums;
using Shiftgram.Core.Models;

namespace Shiftgram.Core.Strategy
{
	internal class PhotoUpdate: IUpdatableAccount
	{
		private ShiftgramContext _context;

		public PhotoUpdate(ShiftgramContext context)
		{
			this._context = context;
		}

		public async Task<DbAnswerCode> Update(AccountUpdateViewModel model)
		{
			var dbEntry = await this._context.Accounts.FirstOrDefaultAsync<Account>(x => x.Id == model.Id);

			if (dbEntry != null)
			{
				dbEntry.PhotoUrl = model.PhotoUrl;
				int rows = await this._context.SaveChangesAsync();

				if (rows > 0)
				{
					return DbAnswerCode.Ok;
				}
			}

			return DbAnswerCode.Bad;
		}
	}
}
