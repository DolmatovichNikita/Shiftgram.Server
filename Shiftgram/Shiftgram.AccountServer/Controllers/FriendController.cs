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

		public FriendController(IFriendRepository friendRepository)
		{
			this._friendRepository = friendRepository;
		}

		[HttpPost]
		public async Task<IHttpActionResult> AddFriend(FriendViewModel model)
		{
			try
			{
				var item = Copy.CopyToFriend(model);
				int rows = await this._friendRepository.Add(item);

				if(rows > 0)
				{
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
