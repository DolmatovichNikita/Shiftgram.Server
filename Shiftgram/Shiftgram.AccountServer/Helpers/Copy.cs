using Shiftgram.AccountServer.Models;
using Shiftgram.Core.Models;
using System.Collections.Generic;

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
				GenderName = account.GenderId != null ? account.Gender.Name : null,
				Username = account.Username,
				PhotoUrl = account.PhotoUrl,
				IsAuth = bool.Parse(account.IsAuth.ToString())
			};
		}

		public static IEnumerable<AccountFriendViewModel> CopyToAccountFriendViewModel(List<Account> accounts)
		{
			List<AccountFriendViewModel> models = new List<AccountFriendViewModel>();

			accounts.ForEach(item =>
			{
				models.Add(new AccountFriendViewModel
				{
					Id = item.Id,
					PhotoUrl = item.PhotoUrl,
					Username = item.Username
				});
			});

			return models;
		}
	}
}