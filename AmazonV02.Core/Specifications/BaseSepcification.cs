using AmazonV02.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Core.Specifications
{
	// step 02 implement interface
	public class BaseSepcification<T> : ISepcification<T> where T : BaseModel
	{
		public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
		public Expression<Func<T, object>> OrderBy { get; set; }
		public Expression<Func<T, object>> OrderByDesc { get ; set ; }
		public int Take { get; set ; }
		public int Skip { get ; set ; }
		public bool isPagnationEnabled { get; set ; }

		public BaseSepcification()// rep getall
        { 
        }
        public BaseSepcification(Expression<Func<T,bool>> criteria)
        {
            Criteria = criteria;
        }

        public void AddOrderBy(Expression<Func<T,object>> expression)
        {
            OrderBy = expression;
        }
        public void AddOrderByDesc(Expression<Func<T,object>> expression)
        {

            OrderByDesc= expression;
        }
        public void ApplyPagination(int take, int skip)
        {
            isPagnationEnabled = true;
            Take = take;
            Skip = skip;
        }
    }
}
