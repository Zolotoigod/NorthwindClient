using MVCNorthwindClient.DTOModels;
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

        public readonly string Rout;
        public readonly string BasePath;
        private const string Path = "/api/products";

        public ProductService(string path)
        {
            BasePath = path;
            Rout = path + Path;
        }        

        public async Task<IEnumerable<ViewProduct>> GetMany(int offset, int limit)
        {
            var responce = await client.GetAsync(Rout + $"/{offset}/{limit}");
            var stringProd = await responce.Content.ReadAsStreamAsync();

            var products = await JsonSerializer.DeserializeAsync<List<Product>>(stringProd, Defines.JsonOptions);

            List<ViewProduct> res = new List<ViewProduct>();

            foreach (var product in products)
            {
                Category category = null;
                if (product.CategoryId is not null)
                {
                    var r = await client.GetAsync(BasePath + "/api/categories" + $"/{product.CategoryId}");
                    var cat = await r.Content.ReadAsStreamAsync();

                    category = await JsonSerializer.DeserializeAsync<Category>(cat, Defines.JsonOptions);
                }                

                res.Add(new ViewProduct() { Product = product, Category =  category});
            }

            return res;
        }

        public async Task<Product> GetById(int productId)
        {
            var responce = await client.GetAsync(Rout + $"/{productId}");
            var stringProd = await responce.Content.ReadAsStreamAsync();
            var product = await JsonSerializer.DeserializeAsync<Product>(stringProd, Defines.JsonOptions);

            return product;
        }

        public async Task Create(Product product)
        {
            var content = JsonContent.Create(product, null, Defines.JsonOptions);
            await client.PostAsync($"{Rout}", content);
        }

        public async Task DeleteById(int productId)
        {
            await client.DeleteAsync(Rout + $"/{productId}");
        }

        public async Task Update(int productId, Product product)
        {
            var content = JsonContent.Create(product, null, Defines.JsonOptions);
            await client.PutAsync($"{Rout}/{productId}", content);
        }

        public async Task<int> GetCount()
        {
            var responce = await client.GetAsync(Rout);
            return await JsonSerializer.DeserializeAsync<int>(await responce.Content.ReadAsStreamAsync(), Defines.JsonOptions);
        }
    }
}
