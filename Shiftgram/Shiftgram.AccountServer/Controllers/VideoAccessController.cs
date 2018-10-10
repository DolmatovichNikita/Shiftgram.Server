using Shiftgram.AccountServer.Models;
using System.Collections.Generic;
using System.Web.Http;
using Twilio.Jwt.AccessToken;
using System.Resources;
using System.Reflection;

namespace Shiftgram.AccountServer.Controllers
{
	[RoutePrefix("api/videoaccess")]
    public class VideoAccessController : ApiController
    {
		private ResourceManager _manager;

		public VideoAccessController()
		{
			this._manager = new ResourceManager("Shiftgram.AccountServer.Resources", Assembly.GetExecutingAssembly());
		}

		[HttpPost]
		public IHttpActionResult GetAccessToken(AccessTokenModel model)
		{
			string accountSid = this._manager.GetString("AccountSID");
			string apiKeySid = this._manager.GetString("AccountApiKeySID");
			string apiKeySecret = this._manager.GetString("ApiKeySecret");
			string identity = model.Name;
			string room = model.Room;

			var grant = new VideoGrant();
			var grants = new HashSet<IGrant> { grant };
			var token = new Token(accountSid, apiKeySid, apiKeySecret, identity: identity, grants: grants);

			return Ok(token.ToJwt());
		}
    }
}
