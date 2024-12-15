using AmazonV02.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Core.Specifications
{
	//  step0 => query Comp => where(P=>P.id==id) , includes(p=>p.id) 2 make 2 prop  order(p=>p.ayaahag)
	// step 1 => make Genric interface have the same sigmnate for prop of quwy comp 
	public interface ISepcification<T> where T : BaseModel
	{
        public Expression<Func<T,bool>> Criteria { get; set; }

        public List<Expression<Func<T,object>>> Includes { get; set; }
		public Expression<Func<T,object>> OrderBy { get; set; }
		public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool isPagnationEnabled { get; set; }


    }
}
