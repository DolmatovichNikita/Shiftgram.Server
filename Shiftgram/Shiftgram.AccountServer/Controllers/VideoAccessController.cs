using Shiftgram.AccountServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Twilio.Jwt.AccessToken;

namespace Shiftgram.AccountServer.Controllers
{
	[RoutePrefix("api/videoaccess")]
    public class VideoAccessController : ApiController
    {
		[HttpPost]
		public IHttpActionResult GetAccessToken(AccessTokenModel model)
		{
			string accountSid = "ACf2a5fcdbf1a63fc1e5f21610b4cd68a0";
			string apiKeySid = "SKbee5c87f96823b291c1a1b745e379c2f";
			string apiKeySecret = "SKbee5c87f96823b291c1a1b745e379c2f";
			string identity = model.Name;
			string room = model.Room;

			var grant = new VideoGrant();
			var grants = new HashSet<IGrant> { grant };
			var token = new Token(accountSid, apiKeySid, apiKeySecret, identity: identity, grants: grants);

			return Ok(token.ToJwt());
		}
    }
}
