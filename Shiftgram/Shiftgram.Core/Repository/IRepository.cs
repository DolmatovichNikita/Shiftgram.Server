using Shiftgram.Core.Enums;
using System.Threading.Tasks;

namespace Shiftgram.Core.Repository
{
	public interface IRepository<TEntity> where TEntity: class
	{
		Task<int> Add(TEntity item);
		Task<DbAnswerCode> Delete(int id);
		Task<TEntity> GetById(int id);
	}
}
