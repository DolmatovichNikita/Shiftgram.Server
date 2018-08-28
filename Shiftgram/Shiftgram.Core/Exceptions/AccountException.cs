using System;
using System.Runtime.Serialization;

namespace Shiftgram.Core.Exceptions
{
	[Serializable]
	public class AccountException: ApplicationException
	{
		public AccountException() { }
		public AccountException(string message)
			: base(message) { }
		public AccountException(string message, Exception inner)
			:base(message, inner) { }
		public AccountException(SerializationInfo info, StreamingContext context)
			:base(info, context) { }
	}
}
