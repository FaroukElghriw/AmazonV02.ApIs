using AmazonV02.Core.Entites;
using AmazonV02.Core.Repository;
using AmazonV02.Core.Specifications;
using AmazonV02.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
	{
		private readonly AmazonDbContext _amazonDb;

		public GenericRepository(AmazonDbContext amazonDb)
        {
			_amazonDb = amazonDb;
		}

		public async Task Add(T entity)
		{
			await _amazonDb.Set<T>().AddAsync(entity);
		}

		public  void Delete(T entity)
		{
			_amazonDb.Set<T>().Remove(entity);
		}

		public async Task<IReadOnlyList<T>> GetAllAsync()
			             => await _amazonDb.Set<T>().ToListAsync();

		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISepcification<T> spec)
		{
			return  await ApplySpecification(spec).ToListAsync();
				
		}

		public async Task<T> GetByIdAsync(int id)
					  => await _amazonDb.Set<T>().FindAsync(id);

		public async Task<T> GetByIdWithSpecAsync(ISepcification<T> spec)
		{
		    return  await ApplySpecification(spec).FirstOrDefaultAsync();
		}

		public async Task<int> GetCountWithSpec(ISepcification<T> spec)
		{
			return await ApplySpecification(spec).CountAsync();
		}

		public void Update(T entity)
		{
			 _amazonDb.Set<T>().Update(entity);
		}

		private IQueryable<T> ApplySpecification(ISepcification<T> spec)
		{
			return SpecificationEvaluter<T>.GetQuery(_amazonDb.Set<T>(), spec);
		}
	}
}
