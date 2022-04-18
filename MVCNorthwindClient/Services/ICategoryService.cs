using MVCNorthwindClient.DTOModels;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MVCNorthwindClient.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetMany(int offset, int limit);

        public Task<Category> GetById(int categoryId);

        public Task Create(Category product);

        public Task Update(int categoryId, Category product);

        public Task DeleteById(int categoryId);

        public Task<int> GetCount();

        Task RepmovePicture(int categoryId);

        Task UpdatePicture(int categoryId, Stream picture);
    }
}
