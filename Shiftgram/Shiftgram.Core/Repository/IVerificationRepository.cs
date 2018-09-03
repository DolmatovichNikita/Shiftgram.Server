using Shiftgram.Core.Models;
using System.Threading.Tasks;

namespace Shiftgram.Core.Repository
{
	public interface IVerificationRepository
	{
		Task AddCode(Verification item);
		Task DeleteCode(string phone);
		Task<Verification> GetById(int id);
	}
}
