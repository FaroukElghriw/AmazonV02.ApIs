using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Core.Entites
{
	public class Product:BaseModel
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }

		public string PictureUrl { get; set; }


		public int ProductBrandId { get; set; } // is a FK for realtionship and not all null as is int  not nullable
		public ProductBrand ProductBrand { get; set; } // is Naigtional prop for one => One to mnat Product and brand

		public int ProductTypeId { get; set; }// is a FK in relatioof typer not allow null
		public ProductType ProductType { get; set; }// Navigtial prop for relation onetoman Type
	}
}
