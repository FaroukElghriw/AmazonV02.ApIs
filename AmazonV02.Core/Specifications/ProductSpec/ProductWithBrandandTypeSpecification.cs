using AmazonV02.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Core.Specifications.ProductSpec
{
    public class ProductWithBrandandTypeSpecification : BaseSepcification<Product>
	{
        public ProductWithBrandandTypeSpecification(ProductSpecParms parms) 
			
        {
			Includes.Add(P => P.ProductBrand);
			Includes.Add(P => P.ProductType);
			 //AddOrderBy(P => P.Price);
			if (!string.IsNullOrEmpty(parms.Sort))
			{
				switch (parms.Sort)
				{
					case "PriceAsc":
						AddOrderBy(P => P.Price);
						break;
					case "PriceDesc":
						AddOrderByDesc(P => P.Price);
						break;
					default: 
						AddOrderBy(P=>P.Name);
						break;

				}
			}
			ApplyPagination(parms.PageSize,parms.PageSize*(parms.PageIndex-1));
        }
    }
}
