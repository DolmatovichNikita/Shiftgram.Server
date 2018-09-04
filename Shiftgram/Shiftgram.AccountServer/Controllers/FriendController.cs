using Shiftgram.AccountServer.Helpers;
using Shiftgram.AccountServer.Models;
using Shiftgram.Core.Enums;
using Shiftgram.Core.Exceptions;
using Shiftgram.Core.Repository;
using System.Threading.Tasks;
using System.Web.Http;

namespace Shiftgram.AccountServer.Controllers
{
	[RoutePrefix("api/friend")]
    public class FriendController : ApiController
    {
		private IFriendRepository _friendRepository;
		private IAccountRepository _accountRepository;

		public FriendController(IFriendRepository friendRepository, IAccountRepository accountRepository)
		{
			this._friendRepository = friendRepository;
			this._accountRepository = accountRepository;
		}

		[HttpPost]
		public async Task<IHttpActionResult> AddFriend(FriendViewModel model)
		{
			try
			{
				var accountB = await this._accountRepository.GetByPhone(model.AccountBPhone);
				if(accountB != null)
				{
					var item = Copy.CopyToFriend(model.AccountAId, accountB.Id);
					int rows = await this._friendRepository.Add(item);

					if (rows > 0)
					{
						return Ok();
					}
				}
			}
			catch(AccountException)
			{
				return BadRequest();
			}

			return BadRequest();
		}

		[HttpDelete]
		public async Task<IHttpActionResult> DeleteFriend(int accountBId)
		{
			var code = await this._friendRepository.Delete(accountBId);

			if(code == DbAnswerCode.Ok)
			{
				return Ok();
			}

			return BadRequest();
		}
    }
}
