using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCNorthwindClient.DTOModels;
using MVCNorthwindClient.Models;
using MVCNorthwindClient.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MVCNorthwindClient.Controllers
{
    public class CategoryController : Controller
    {
        public readonly ICategoryService service;
        private const int pageSize = 4;
        private int totalPages;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Category(int id)
        {
            var category = await service.GetById(id);
            return View(category);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View("CreateCategory");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            await service.Create(category);
            return RedirectToAction("Categories");
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            await service.DeleteById(id);
            return RedirectToAction("Categories");
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Category category)
        {
            await service.Update(id, category);
            return RedirectToAction("Categories");
        }

        public async Task<IActionResult> Categories(int currentPage = 1)
        {
            totalPages = (int)Math.Ceiling((decimal)(await service.GetCount()) / pageSize);
            var collection = await service.GetMany((currentPage - 1) * pageSize, pageSize);
            return View(new ItemList<Category>()
            {
                Items = collection,
                PagingInfo = new PagingInfo()
                {
                    ItemCount = collection.Count(),
                    CurrentPage = currentPage,
                    TotalPages = totalPages,
                    ItemsPerPage = pageSize,
                }
            });
        }

        public async Task<IActionResult> RemovePicture(int categoryId)
        {
            await service.RepmovePicture(categoryId);
            return RedirectToAction("Categories");
        }

        public async Task<IActionResult> UpdatePicture(int categoryId, IFormFile picture)
        {
            await service.UpdatePicture(categoryId, picture.OpenReadStream());
            return RedirectToAction("Categories");
        }
    }
}
