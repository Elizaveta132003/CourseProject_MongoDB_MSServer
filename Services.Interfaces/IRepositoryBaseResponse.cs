
using Domain.Core.Models;

namespace Services.Interfaces
{
	public interface IRepositoryBaseResponse<T>
	{
		public BaseResponse<List<T>> GetAll();
		public BaseResponse<T> GetT(int id);
		public BaseResponse<bool> Create(T item);
		public BaseResponse<bool> Update(T item);
		public BaseResponse<bool> Delete(T item);
	}
}
