using MVCNorthwindClient.DTOModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCNorthwindClient.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetMany(int offset, int limit);

        public Task<Product> GetById(int productId);

        public Task Create(Product product);

        public Task Update(int productId, Product product);

        public Task DeleteById(int productId);

        public Task<int> GetCount();
    }
}
