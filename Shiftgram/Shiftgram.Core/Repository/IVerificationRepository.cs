using Shiftgram.Core.Models;
using System.Threading.Tasks;

namespace Shiftgram.Core.Repository
{
	public interface IVerificationRepository
	{
		Task AddCode(Verification item);
		Task DeleteCode(int id);
		Task<Verification> GetById(int id);
	}
}
