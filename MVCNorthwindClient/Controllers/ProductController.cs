using Microsoft.AspNetCore.Mvc;
using MVCNorthwindClient.Models;
using MVCNorthwindClient.Services;
using System.Threading.Tasks;
using System.Linq;
using MVCNorthwindClient.DTOModels;
using System;

namespace MVCNorthwindClient.Controllers
{
    public class ProductController : Controller
    {
        public readonly IProductService service;
        private const int pageSize = 5;
        private int totalPages;

        public ProductController(IProductService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Product(int productId)
        {
            var product = await service.GetById(productId);
            return View(product);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View("CreateProduct");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            await service.Create(product);
            return RedirectToAction("Products");
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int productId)
        {
            await service.DeleteById(productId);
            return RedirectToAction("Products");
        }

        [HttpPost]
        public async Task<IActionResult> Update(int productId, Product product)
        {
            await service.Update(productId, product);
            return RedirectToAction("Products");
        }

        public async Task<IActionResult> Products(int currentPage = 1)
        {
            totalPages = (int)Math.Ceiling((decimal)(await service.GetCount()) / pageSize);
            var collection = await service.GetMany((currentPage - 1) * pageSize, pageSize);
            return View(new ItemList<ViewProduct>()
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
    }
}
