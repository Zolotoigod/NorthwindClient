using System;

namespace MVCNorthwindClient.Models
{
    public class PagingInfo
    {
        public int ItemCount { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}
