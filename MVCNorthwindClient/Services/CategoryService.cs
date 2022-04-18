using MVCNorthwindClient.DTOModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVCNorthwindClient.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient client = new HttpClient();

        public readonly string Rout;
        private const string Path = "/api/categories";

        public CategoryService(string path)
        {
            Rout = path + Path;
        }

        public async Task Create(Category category)
        {
            var content = JsonContent.Create(category, null, Defines.JsonOptions);
            await client.PostAsync($"{Rout}", content);
        }

        public async Task DeleteById(int categoryId)
        {
            await client.DeleteAsync(Rout + $"/{categoryId}");
        }

        public async Task<Category> GetById(int categoryId)
        {
            var responce = await client.GetAsync(Rout + $"/{categoryId}");
            var stringProd = await responce.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Category>(stringProd, Defines.JsonOptions);
        }

        public async Task<int> GetCount()
        {
            var responce = await client.GetAsync(Rout);
            return await JsonSerializer.DeserializeAsync<int>(await responce.Content.ReadAsStreamAsync(), Defines.JsonOptions);
        }

        public async Task<IEnumerable<Category>> GetMany(int offset, int limit)
        {
            var responce = await client.GetAsync(Rout + $"/{offset}/{limit}");
            var stringProd = await responce.Content.ReadAsStreamAsync();

            var categories = await JsonSerializer.DeserializeAsync<List<Category>>(stringProd, Defines.JsonOptions);

            foreach (var category in categories)
            {
                var responcepic = await client.GetAsync(Rout + $"/{category.CategoryId}/picture");

                if (responcepic.StatusCode == HttpStatusCode.OK)
                {
                    var stream = await responcepic.Content.ReadAsStreamAsync();
                    byte[] bytes = new byte[stream.Length];
                    await stream.ReadAsync(bytes, 0, bytes.Length);

                    category.Picture = bytes;
                }
                else
                {
                    category.Picture = Array.Empty<byte>();
                }
            }

            return categories;
        }

        public async Task Update(int categoryId, Category category)
        {
            var content = JsonContent.Create(category, null, Defines.JsonOptions);
            await client.PutAsync($"{Rout}/{categoryId}", content);
        }

        public async Task RepmovePicture(int categoryId)
        {
            await client.DeleteAsync(Rout + $"/{categoryId}/picture");
        }

        public async Task UpdatePicture(int categoryId, Stream picture)
        {
            var content = new StreamContent(picture);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            await client.PutAsync(Rout + $"/{categoryId}/picture", content);
        }
    }
}
