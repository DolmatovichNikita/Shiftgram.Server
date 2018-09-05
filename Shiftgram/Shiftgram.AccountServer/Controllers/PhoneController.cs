using Shiftgram.Core.Repository;
using System.Threading.Tasks;
using System.Web.Http;

namespace Shiftgram.AccountServer.Controllers
{
	[RoutePrefix("api/phone")]
    public class PhoneController : ApiController
    {
		private IPhoneRepository _phoneRepository;

		public PhoneController(IPhoneRepository phoneRepository)
		{
			this._phoneRepository = phoneRepository;
		}

		[HttpGet]
		public async Task<IHttpActionResult> GetPhones()
		{
			var phones = await this._phoneRepository.GetPhones();

			return Ok(phones);
		}
    }
}
