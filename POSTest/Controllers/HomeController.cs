using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using POSTest.Models;
using POSTest.Payloads;
using POSTest.Repositories.Interfaces;
using POSTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _productRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var productsVm = await _productRepository.GetAllAsync();
            return View(productsVm);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            var payload = new ProductPayload();
            return View(payload);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ProductPayload productPayload)
        {
            if (ModelState.IsValid)
            {
                var result = await _productRepository.CreateAsync(productPayload);
                return RedirectToAction("AddNewSizes", new { id = result.Id });
            }

            else
            {
                return View(productPayload);
            }

        }


        [HttpGet]
        public IActionResult AddNewSizes(int id)
        {
            var sizePayload = new SizesPayload();
            sizePayload.ProductId = id;
            return View(sizePayload);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewSizes([FromForm] SizesPayload payload)
        {
            if (!String.IsNullOrEmpty(payload.Size1.Name))
                await _productRepository.AddSize(payload.ProductId, payload.Size1);

            if (!String.IsNullOrEmpty(payload.Size2.Name))
                await _productRepository.AddSize(payload.ProductId, payload.Size2);

            if (!String.IsNullOrEmpty(payload.Size3.Name))
                await _productRepository.AddSize(payload.ProductId, payload.Size3);

            if (!String.IsNullOrEmpty(payload.Size4.Name))
                await _productRepository.AddSize(payload.ProductId, payload.Size4);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult>UpdateProduct(int id)
        {
            return View(await _productRepository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductVm productVm)
        {
            await _productRepository.UpdateAsync(productVm.Id, productVm);
            return RedirectToAction("Index");
        }

    }
}
