using AmazonV02.Core.Entites;
using AmazonV02.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Core.Repository
{
	public interface IGenericRepository<T> where T : BaseModel
	{
		Task<IReadOnlyList<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);
		Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISepcification<T> spec);
		Task<T> GetByIdWithSpecAsync(ISepcification<T> spec);
		Task<int> GetCountWithSpec(ISepcification<T> spec);
		Task Add(T entity);

		void Update(T entity);
		void Delete(T entity);


	}
}
