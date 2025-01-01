using AmazonV02.Core.Entites.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Core.Specifications.OrderSpec
{
	public class OrderSpecification:BaseSepcification<Order>
	{
        public OrderSpecification(string email):base(O=>O.BuyerEmail==email)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O=>O.Items);
            AddOrderByDesc(o=>o.DateTimeOffset);
        }
		public OrderSpecification(string email, int id ) : base(O => O.BuyerEmail == email && O.Id == id )
		{
			Includes.Add(O => O.DeliveryMethod);
			Includes.Add(O => O.Items);
		
		}
	}
}
