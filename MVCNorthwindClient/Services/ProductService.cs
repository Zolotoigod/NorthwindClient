using MVCNorthwindClient.DTOModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVCNorthwindClient.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient client = new HttpClient();

        public readonly string Host;
        private const string Path = "/api/products";

        public ProductService(string path)
        {
            Host = path;
        }        

        public async Task<IEnumerable<Product>> GetMany(int offset, int limit)
        {
            var responce = await client.GetAsync(Host + Path + $"/{offset}/{limit}");
            var stringProd = await responce.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<List<Product>>(stringProd, Defines.JsonOptions);
        }

        public async Task<Product> GetById(int productId)
        {
            var responce = await client.GetAsync(Host + Path + $"/{productId}");
            var stringProd = await responce.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Product>(stringProd, Defines.JsonOptions);
        }

        public async Task Create(Product product)
        {
            var content = JsonContent.Create(product, null, Defines.JsonOptions);
            await client.PostAsync($"{Host + Path}", content);
        }

        public async Task DeleteById(int productId)
        {
            await client.DeleteAsync(Host + Path + $"/{productId}");
        }

        public async Task Update(int productId, Product product)
        {
            var content = JsonContent.Create(product, null, Defines.JsonOptions);
            await client.PutAsync($"{Host + Path}/{productId}", content);
        }

        public async Task<int> GetCount()
        {
            var responce = await client.GetAsync(Host + Path);
            return await JsonSerializer.DeserializeAsync<int>(await responce.Content.ReadAsStreamAsync(), Defines.JsonOptions);
        }
    }
}
