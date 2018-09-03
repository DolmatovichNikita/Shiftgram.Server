using Shiftgram.Core.Models;
using System.Threading.Tasks;

namespace Shiftgram.Core.Repository
{
	public interface IVerificationRepository: IRepository<Verification>
	{
		Task Update(string phone);
	}
}
