using MVCNorthwindClient.DTOModels;
using MVCNorthwindClient.DTOModels.Blog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCNorthwindClient.Services
{
    public interface IBlogArticleService
    {
        Task<int> CreateArticle(ArticleAddRequest request);

        Task<ArticleResponce> GetArticle(int articleId);

        Task UpdateArticle(int articleId, ArticleUpdateRequest request);

        Task DeleteArticle(int articleId);

        Task<IEnumerable<ArticleResponce>> GetCollection(int offset, int limit);

        Task<int> CreateLink(int articleId, int productId);

        Task RemoveLink(int articleId, int productId);

        Task<int> AddComment(int articleId, string text);

        Task<IEnumerable<ArticleComment>> ReadComments(int articleId);

        Task UpdateComment(int commentId, string text);

        Task DeleteComment(int commentId);

        Task<IEnumerable<Product>> GetRelatedProduct(int articleId);
    }
}
