using Shiftgram.Core.Exceptions;
using Shiftgram.Core.Models;

namespace Shiftgram.Core.Strategy
{
	public class AccountUpdate
	{
		private ShiftgramContext _context;

		public AccountUpdate(ShiftgramContext context)
		{
			this._context = context;
		}

		public IUpdatableAccount CreateUpdate(string updateType)
		{
			switch(updateType)
			{
				case "InitialsUpdate":
					return new InitialsUpdate(this._context);
				case "BioUpdate":
					return new BioUpdate(this._context);
				case "UsernameUpdate":
					return new UsernameUpdate(this._context);
				case "PhotoUpdate":
					return new PhotoUpdate(this._context);
				case "GenderUpdate":
					return new GenderUpdate(this._context);
				case "PhoneUpdate":
					return new PhoneUpdate(this._context);
				case "AuthUpdate":
					return new AuthUpdate(this._context);
			}

			throw new AccountException();
		}
	}
}
