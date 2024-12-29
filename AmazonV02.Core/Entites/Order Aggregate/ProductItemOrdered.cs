using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Core.Entites.Order_Aggregate
{
    public class ProductItemOrdered
	{
        public ProductItemOrdered()
        {
            
        }
        public ProductItemOrdered(int id , string name,string url)
        {
            ProductId = id ;
            ProductName = name ;
            ProductPicture= url ;   
        }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductPicture { get; set; }
    }
}
