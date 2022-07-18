using Microsoft.AspNetCore.Mvc;
using POSTest.Models;
using POSTest.Repositories.Interfaces;
using POSTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSTest.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;

        public OrdersController(
            IProductRepository repo,
            ShoppingCart shoppingCart,
            IOrdersService ordersService
        )
        {
            _productRepository = repo;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _ordersService.GetOrdersByUserId("isl");
            return View(result);
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVm()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }

        public IActionResult NewShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVm()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product != null)
            {
                _shoppingCart.AddItemToCart(product);
            }
            return RedirectToAction(nameof(NewShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var mobile = await _productRepository.GetByIdAsync(id);
            if (mobile != null)
            {
                _shoppingCart.RemoveItemFromCart(mobile);
            }
            return RedirectToAction(nameof(NewShoppingCart));
        }

        public async Task<IActionResult> RemoveAllItemFromShoppingCart(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product != null)
            {
                _shoppingCart.RemoveAllItemFromCart(product);
            }
            return RedirectToAction(nameof(NewShoppingCart));
        } 

        public async Task<IActionResult> Checkout()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            await _ordersService.StoreOrderAsync(items, "isl", "Cairo");
            await _shoppingCart.ClearShoppingCartAsync();
            return RedirectToAction("Index");
        }
        
    }
}
