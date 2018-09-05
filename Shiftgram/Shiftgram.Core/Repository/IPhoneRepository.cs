using Shiftgram.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shiftgram.Core.Repository
{
	public interface IPhoneRepository
	{
		Task<IEnumerable<Phone>> GetPhones();
	}
}
