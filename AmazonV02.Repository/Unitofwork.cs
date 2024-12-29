using AmazonV02.Core;
using AmazonV02.Core.Entites;
using AmazonV02.Core.Repository;
using AmazonV02.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Repository
{
	public class Unitofwork : IUnitofwork
	{
		private readonly AmazonDbContext _dbContext;
		private Hashtable _repository;

		public Unitofwork(AmazonDbContext dbContext )
        {
			_dbContext = dbContext;
			_repository = new Hashtable();
		}
		public async Task<int> Complete()
		   => await _dbContext.SaveChangesAsync();

		public async ValueTask DisposeAsync()
			 => await _dbContext.DisposeAsync();

		public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseModel
		{
			var type = typeof(TEntity).Name;
			if (!_repository.ContainsKey(type))
			{
				var repo = new GenericRepository<TEntity>(_dbContext);
				_repository.Add(type, repo);	

			}
			return _repository[type] as IGenericRepository<TEntity>;
		}
	}
}
