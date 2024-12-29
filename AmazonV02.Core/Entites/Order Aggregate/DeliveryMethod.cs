using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Core.Entites.Order_Aggregate
{
	public class DeliveryMethod :BaseModel
	{
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string description ,  decimal cost, string shortname, string deliveryTime)
        {
            Description = description;
            Cost = cost;
            ShortName = shortname;
           DeliveryTime = deliveryTime;
            
        }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string ShortName { get; set; }
        public string DeliveryTime { get; set; }
    }
}
