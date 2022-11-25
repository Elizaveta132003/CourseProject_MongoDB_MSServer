using Domain.Core.Models;

namespace Domain.Interfaces
{
	public interface IRepository<T> where T : EntityBase
	{
		public List<T> GetAll();
		public T GetT(int id);
		public bool Create(T item);
		public bool Update(T item);
		public bool Delete(T item);
	}
}
