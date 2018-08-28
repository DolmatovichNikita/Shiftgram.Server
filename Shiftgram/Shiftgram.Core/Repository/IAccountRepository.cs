using Shiftgram.Core.Models;
using Shiftgram.Core.Strategy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shiftgram.Core.Repository
{
	public interface IAccountRepository: IRepository<Account>, IUpdatableAccount
	{
		Task<IEnumerable<Account>> GetAll();
		Task<Account> GetByPhone(string phone);
		ShiftgramContext Context { get; }
		IUpdatableAccount Updatable { get; set; }
	}
}
