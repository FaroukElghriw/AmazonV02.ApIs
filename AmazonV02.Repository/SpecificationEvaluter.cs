using AmazonV02.Core.Entites;
using AmazonV02.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Repository
{
	public static class SpecificationEvaluter<T> where T : BaseModel
	{
		public static IQueryable<T> GetQuery(IQueryable<T> query, ISepcification<T> spec)
		{
			var Query = query;

			if (spec.Criteria != null)
				Query = Query.Where(spec.Criteria);
			if(spec.isPagnationEnabled)
				Query=Query.Skip(spec.Skip).Take(spec.Take);
			if(spec.OrderBy is not null)
				Query = Query.OrderBy(spec.OrderBy);

			if(spec.OrderByDesc is not null)
				Query = Query.OrderByDescending(spec.OrderByDesc);

			Query = spec.Includes.Aggregate(Query, (currentQuery, queryexp) => currentQuery.Include(queryexp));


			return Query;
		}
	}
}
