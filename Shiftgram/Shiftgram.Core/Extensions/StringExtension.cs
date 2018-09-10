using System.Collections.Generic;

namespace Shiftgram.Core.Extensions
{
	public static class StringExtension
	{
		public static IEnumerable<string> ParsePhoneVerify(this string phone)
		{
			string id = string.Empty;
			string phoneNumber = string.Empty;
			int index = 0;

			for(var i = 0; i < phone.Length; i++)
			{
				if(phone[i] == ':')
				{
					index = i;
					break;
				}
			}

			id = phone.Substring(0, index);
			phoneNumber = phone.Substring(index + 1);

			return new string[] { id, phoneNumber };
		}
	}
}
