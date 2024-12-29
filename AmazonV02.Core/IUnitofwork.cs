using AmazonV02.Core.Entites;
using AmazonV02.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Core
{
	public interface IUnitofwork:IAsyncDisposable
	{
		IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseModel;
		Task<int> Complete(); 
	}
}
