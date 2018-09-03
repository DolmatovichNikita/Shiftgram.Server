using System.Data.Entity;
using System.Threading.Tasks;
using Shiftgram.Core.Exceptions;
using Shiftgram.Core.Models;

namespace Shiftgram.Core.Repository
{
	public class VerificationRepository : IVerificationRepository
	{
		private ShiftgramContext _context;

		public VerificationRepository()
		{
			this._context = new ShiftgramContext();
		}

		public async Task AddCode(Verification item)
		{
			this._context.Verifications.Add(item);
			int rows = await this._context.SaveChangesAsync();

			if(rows == 0)
			{
				throw new AccountException();
			}
		}

		public async Task DeleteCode(string phone)
		{
			var dbEntry = await this._context.Verifications.FirstOrDefaultAsync(x => x.Account.Phone == phone);

			if(dbEntry != null)
			{
				this._context.Verifications.Remove(dbEntry);
				await this._context.SaveChangesAsync();
			}
		}

		public async Task<Verification> GetById(int id)
		{
			var dbEntry = await this._context.Verifications.FirstOrDefaultAsync(x => x.Id == id);

			if(dbEntry != null)
			{
				return dbEntry;
			}

			throw new AccountException();
		}
	}
}
