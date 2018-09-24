using Shiftgram.AccountServer.Helpers;
using Shiftgram.AccountServer.Models;
using Shiftgram.Core.Enums;
using Shiftgram.Core.Exceptions;
using Shiftgram.Core.Models;
using Shiftgram.Core.Repository;
using System.Collections.Generic;
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
				if (accountB != null)
				{
					var item = Copy.CopyToFriend(model.AccountAId, accountB.Id);
					await this._friendRepository.Add(item);
                    return Ok();
                }
			}
			catch(AccountException)
			{
				return BadRequest();
			}

            return BadRequest();
		}

		[HttpDelete]
		public async Task<IHttpActionResult> DeleteFriend(FriendViewModel model)
		{
			var code = await this._friendRepository.Delete(model.AccountAId, model.AccountBId);

			if(code == DbAnswerCode.Ok)
			{
				return Ok();
			}

			return BadRequest();
		}

		[HttpGet]
		[Route("{accountAId:int}")]
		public async Task<IHttpActionResult> GetFriends(int accountAId)
		{
			List<Account> accounts = await this._friendRepository.GetFriends(accountAId) as List<Account>;
			var friends = Copy.CopyToAccountFriendViewModel(accounts);

			return Ok(friends);
		}
    }
}
