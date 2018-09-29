using System.Reflection;
using System.Resources;

namespace Shiftgram.Core.Views
{
	internal abstract class View
	{
		protected readonly string _connection;

		public View()
		{
			this._connection = new ResourceManager("Shiftgram.Core.Resources", Assembly.GetExecutingAssembly()).GetString("DatabaseConnection");
		}
	}
}
