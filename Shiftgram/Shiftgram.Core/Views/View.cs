using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace Shiftgram.Core.Views
{
	internal abstract class View
	{
		protected readonly string _connection;

		public View()
		{
			this._connection = new ResourceManager("Shiftgram.Core.Resources", Assembly.GetExecutingAssembly()).GetString("DatabaseConnection");
		}

		public abstract Task CreateView(params object[] parameters);
	}
}
