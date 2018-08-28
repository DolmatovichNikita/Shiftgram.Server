using Shiftgram.Core.Enums;
using System.Threading.Tasks;

namespace Shiftgram.Core.Strategy
{
	public interface IUpdatableAccount
	{
		Task<DbAnswerCode> Update(AccountUpdateViewModel model);
	}
}
