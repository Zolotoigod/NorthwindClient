using System;

namespace MVCNorthwindClient.DTOModels.Blog
{
    public class Article
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime PublishDate { get; set; }

        public int EmployeeId { get; set; }
    }
}
