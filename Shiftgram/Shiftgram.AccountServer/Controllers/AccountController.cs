using Shiftgram.AccountServer.Helpers;
using Shiftgram.AccountServer.Models;
using Shiftgram.Core.Enums;
using Shiftgram.Core.Exceptions;
using Shiftgram.Core.Models;
using Shiftgram.Core.Repository;
using Shiftgram.Core.Strategy;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Shiftgram.AccountServer.Controllers
{
	[RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
		private readonly IAccountRepository _accountRepository;

		public AccountController(IAccountRepository accountRepository)
		{
			this._accountRepository = accountRepository;
		}

		[HttpPost]
		public async Task<int> AddAccount(AccountViewModel model)
		{
			int id = 0;

			try
			{
				Account account = Copy.CopyToAccount(model);
				id = await this._accountRepository.Add(account);
			}
			catch(AccountException)
			{
				id = int.MinValue;
			}

			return id;
		}

		[HttpDelete]
		public async Task<IHttpActionResult> DeleteAccount(int id)
		{
			DbAnswerCode code = await this._accountRepository.Delete(id);

			if(code == DbAnswerCode.Ok)
			{
				return Ok();
			}

			return BadRequest();
		}

		[HttpGet]
		public async Task<IEnumerable<AccountViewModel>> GetAllAccounts()
		{
			var accounts = await this._accountRepository.GetAll() as List<Account>;
			var models = Copy.CopyToEnumerableAccountViewModel(accounts);

			return models;
		}

		[HttpGet]
		[Route("{id:int}")]
		public async Task<IHttpActionResult> GetAccountById(int id)
		{
			try
			{

				Account account = await this._accountRepository.GetById(id);
				var accountViewModel = Copy.CopyToAccountViewModel(account);

				return Ok(accountViewModel);
			}
			catch(AccountException)
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("{phone}")]
		public async Task<IHttpActionResult> GetAccountByPhone(string phone)
		{
			try
			{
				string phoneNumber = $"+{phone}";
				Account account = await this._accountRepository.GetByPhone(phoneNumber);
				var accountViewModel = Copy.CopyToAccountViewModel(account);

				return Ok(accountViewModel);
			}
			catch(AccountException)
			{
				return BadRequest();
			}
		}

		[HttpPut]
		public async Task<IHttpActionResult> UpdateAccount(AccountUpdateViewModel model)
		{
			AccountUpdate update = new AccountUpdate(this._accountRepository.Context);
			this._accountRepository.Updatable = update.CreateUpdate(model.UpdateType);
			DbAnswerCode code = await this._accountRepository.Update(model);

			if(code == DbAnswerCode.Ok)
			{
				return Ok();
			}

			return BadRequest();
		}
    }
}
