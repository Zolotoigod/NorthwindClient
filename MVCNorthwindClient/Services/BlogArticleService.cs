using MVCNorthwindClient.DTOModels;
using MVCNorthwindClient.DTOModels.Blog;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVCNorthwindClient.Services
{
    public class BlogArticleService : IBlogArticleService
    {
        private readonly HttpClient client = new HttpClient();

        public readonly string Rout;
        public readonly string BasePath;
        private const string Path = "/api/articles";

        public BlogArticleService(string path)
        {
            this.Rout = path + Path;
        }

        public async Task<int> CreateArticle(ArticleAddRequest request)
        {
            var content = JsonContent.Create(request, null, Defines.JsonOptions);
            var responce = await client.PostAsync(Rout, content);
            return await JsonSerializer.DeserializeAsync<int>(
                await responce.Content.ReadAsStreamAsync(), Defines.JsonOptions);
        }

        public async Task UpdateArticle(int articleId, ArticleUpdateRequest request)
        {
            var content = JsonContent.Create(request, null, Defines.JsonOptions);
            await client.PutAsync(Rout + $"/{articleId}", content);
        }

        public async Task<ArticleResponce> GetArticle(int articleId)
        {
            var responce = await client.GetAsync(Rout + $"/{articleId}");
            return await JsonSerializer.DeserializeAsync<ArticleResponce>(
                await responce.Content.ReadAsStreamAsync(), Defines.JsonOptions);
        }

        public async Task<IEnumerable<ArticleResponce>> GetCollection(int offset, int limit)
        {
            var responce = await client.GetAsync(Rout + $"/{offset}/{limit}");
            return await JsonSerializer.DeserializeAsync<List<ArticleResponce>>(
                await responce.Content.ReadAsStreamAsync(), Defines.JsonOptions);
        }

        public async Task DeleteArticle(int articleId)
        {
            await client.DeleteAsync(Rout + $"/{articleId}");
        }

        public async Task<int> CreateLink(int articleId, int productId)
        {
            var responce = await client.PostAsync(Rout + $"{articleId}/products/{productId}", null);
            return await JsonSerializer.DeserializeAsync<int>(
                await responce.Content.ReadAsStreamAsync(), Defines.JsonOptions);
        }

        public async Task RemoveLink(int articleId, int productId)
        {
            await client.DeleteAsync(Rout + $"{articleId}/products/{productId}");
        }

        public async Task DeleteComment(int commentId)
        {            
            await client.DeleteAsync(Rout + $"{commentId}/comments");
        }

        public async Task<IEnumerable<ArticleComment>> ReadComments(int articleId)
        {
            var responce = await client.GetAsync(Rout + $"{articleId}/comments");
            return await JsonSerializer.DeserializeAsync<IEnumerable<ArticleComment>>(
                await responce.Content.ReadAsStreamAsync(), Defines.JsonOptions);
        }

        public async Task<int> AddComment(int articleId, string text)
        {            
            var content = JsonContent.Create(text, null, Defines.JsonOptions);
            var responce = await client.PostAsync(Rout + $"{articleId}/comments", content);
            return await JsonSerializer.DeserializeAsync<int>(
                await responce.Content.ReadAsStreamAsync(), Defines.JsonOptions);
        }

        public async Task UpdateComment(int commentId, string text)
        {
            var content = JsonContent.Create(text, null, Defines.JsonOptions);
            await client.PutAsync(Rout + $"{commentId}/comments", content);
        }

        public async Task<IEnumerable<Product>> GetRelatedProduct(int articleId)
        {
            var responce = await client.GetAsync(Rout + $"{articleId}/products");
            return await JsonSerializer.DeserializeAsync<IEnumerable<Product>>(
                await responce.Content.ReadAsStreamAsync(), Defines.JsonOptions);
        }
    }
}
