using Shiftgram.AccountServer.Models;
using System;
using System.Web;
using System.Web.Http;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Shiftgram.AccountServer.Controllers
{
	[RoutePrefix("api/phone")]
    public class PhoneVerifyController : ApiController
    {
		private readonly string _accountSid = "ACf2a5fcdbf1a63fc1e5f21610b4cd68a0";
		private readonly string _accountAuth = "f0fffdcf057adcd84828cfe1534c41e1";
		private readonly string _from = "+48732230630";
		private int _number;

		public PhoneVerifyController()
		{
			this._number = this.GenerateCode();
		}

		[HttpGet]
		[Route("{phone}")]
		public IHttpActionResult SendSMS(string phone)
        {
			TwilioClient.Init(this._accountSid, this._accountAuth);

			var numberPhone = "+" + phone;
			var to = new PhoneNumber(numberPhone);
			var from = new PhoneNumber(this._from);
			var body = $"Your verification code: {this._number}";
			var message = MessageResource.Create(to: to, from: from, body: body);

			return Ok(message.Sid);
		}

		[HttpPost]
		public IHttpActionResult IsAuth(PhoneVerifyViewModel model)
		{
			if(model != null)
			{
				if(HttpContext.Current.Session[model.Number] as string == model.Code)
				{
					return Ok();
				}
			}

			return BadRequest();
		}

		private int GenerateCode()
		{
			Random ran = new Random();

			return ran.Next(99999);
		}
    }
}