using Shiftgram.Core.Enums;
using Shiftgram.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shiftgram.Core.Repository
{
	public interface IFriendRepository
	{
		Task<int> Add(Friend item);
		Task<DbAnswerCode> Delete(int accountAId, int accountBId);
		Task<Friend> GetById(int id);
		Task<IEnumerable<Account>> GetFriends(int accountAId);
	}
}
