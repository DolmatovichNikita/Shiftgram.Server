using Shiftgram.AccountServer.Helpers;
using Shiftgram.AccountServer.Models;
using Shiftgram.Core.Exceptions;
using Shiftgram.Core.Extensions;
using Shiftgram.Core.Repository;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Shiftgram.AccountServer.Controllers
{
	[RoutePrefix("api/verify")]
    public class PhoneVerifyController : ApiController
    {
		private readonly string _accountSid = "ACf2a5fcdbf1a63fc1e5f21610b4cd68a0";
		private readonly string _accountAuth = "f0fffdcf057adcd84828cfe1534c41e1";
		private readonly string _from = "+48732230630";
		private int _number;
		private IVerificationRepository _verificationRepository;

		public PhoneVerifyController(IVerificationRepository verificationRepository)
		{
			this._number = this.GenerateCode();
			this._verificationRepository = verificationRepository;
		}

		[HttpGet]
		[Route("{phone}")]
		public IHttpActionResult SendSMS(string phone)
        {
			TwilioClient.Init(this._accountSid, this._accountAuth);
			string[] arr = phone.ParsePhoneVerify() as string[];

			var numberPhone = "+" + arr[1];
			var to = new PhoneNumber(numberPhone);
			var from = new PhoneNumber(this._from);
			var body = $"Your verification code: {this._number}";
			var message = MessageResource.Create(to: to, from: from, body: body);
			PhoneVerifyViewModel model = new PhoneVerifyViewModel() { Id = int.Parse(arr[0]), Number = numberPhone };
			model.Code = this._number;
			var item = Copy.CopyToVerification(model);

			try
			{
				this._verificationRepository.AddCode(item);
			}
			catch(AccountException)
			{
				return BadRequest();
			}

			return Ok(arr);
		}

		[HttpPost]
		public async Task<IHttpActionResult> IsAuth(PhoneVerifyViewModel model)
		{
			if(model != null)
			{
				try
				{
					var verification = await this._verificationRepository.GetById(model.Id);
					if (verification != null)
					{
						if (verification.Account.Phone == model.Number && verification.VerifyCode == model.Code.ToString())
						{
							await this._verificationRepository.DeleteCode(model.Number);
							return Ok();
						}
					}
				}
				catch(AccountException)
				{
					return BadRequest();
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