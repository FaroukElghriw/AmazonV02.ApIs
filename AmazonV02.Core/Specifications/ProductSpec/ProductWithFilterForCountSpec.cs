using AmazonV02.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Core.Specifications.ProductSpec
{
	public class ProductWithFilterForCountSpec:BaseSepcification<Product>
	{
        public ProductWithFilterForCountSpec(ProductSpecParms parms):
             base(P=>
			(!parms.BrandId.HasValue || P.ProductBrandId == parms.BrandId) &&
			(!parms.TypeId.HasValue || P.ProductTypeId == parms.TypeId))
        {
            
        }
    }
}
