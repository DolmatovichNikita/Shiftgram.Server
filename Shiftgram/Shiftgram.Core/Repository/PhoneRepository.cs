using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Shiftgram.Core.Models;

namespace Shiftgram.Core.Repository
{
	public class PhoneRepository : IPhoneRepository
	{
		private ShiftgramContext _context;

		public PhoneRepository()
		{
			this._context = new ShiftgramContext();
		}

		public async Task<IEnumerable<Phone>> GetPhones()
		{
			return await this._context.Phones.ToListAsync();
		}
	}
}
