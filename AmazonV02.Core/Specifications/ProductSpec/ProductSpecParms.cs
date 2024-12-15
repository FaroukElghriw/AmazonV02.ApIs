using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Core.Specifications.ProductSpec
{
    public class ProductSpecParms
    {
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        public int PageIndex { get; set; }

        private const int maxPageSize = 10;
        private int pageSize = 5;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > maxPageSize ? maxPageSize : value; }
        }

    }
}
