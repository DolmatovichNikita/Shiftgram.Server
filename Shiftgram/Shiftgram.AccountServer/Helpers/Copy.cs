using Shiftgram.AccountServer.Models;
using Shiftgram.Core.Models;

namespace Shiftgram.AccountServer.Helpers
{
	public static class Copy
	{
		public static Verification CopyToVerification(PhoneVerifyViewModel model)
		{
			return new Verification
			{
				Id = model.Id,
				VerifyCode = model.Code.ToString()
			};
		}

		public static Friend CopyToFriend(int accountAId, int accountBId)
		{
			return new Friend
			{
				AccountAId = accountAId,
				AccountBId = accountBId
			};
		}

		public static AccountViewModel CopyToAccountViewModel(Account account)
		{
			return new AccountViewModel
			{
				Id = account.Id,
				FirstName = account.FirstName,
				LastName = account.LastName,
				Bio = account.Bio,
				Phone = account.Phone,
				GenderName = account.GenderId != null ? account.Gender.Name : string.Empty,
				Username = account.Username,
				PhotoUrl = account.PhotoUrl,
				IsAuth = account.IsAuth != null ? account.IsAuth.Value : false
			};
		}

		public static Account CopyToAccount(AccountViewModel model)
		{
			return new Account
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Username = model.Username,
				Bio = model.Bio,
				Phone = model.Phone,
				PhotoUrl = model.PhotoUrl,
				GenderId = model.GenderName == "Male" ? 1 : 2
			};
		}
	}
}