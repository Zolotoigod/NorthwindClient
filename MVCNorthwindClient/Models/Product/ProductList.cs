using MVCNorthwindClient.DTOModels;
using System.Collections.Generic;

namespace MVCNorthwindClient.Models
{
    public class ProductList
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
