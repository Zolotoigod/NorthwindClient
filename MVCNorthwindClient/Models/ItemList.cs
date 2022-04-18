using System.Collections.Generic;

namespace MVCNorthwindClient.Models
{
    public class ItemList<T>
    {
        public IEnumerable<T> Items { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
