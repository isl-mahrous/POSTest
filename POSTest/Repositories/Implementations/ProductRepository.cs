using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using POSTest.Data;
using POSTest.Models;
using POSTest.Payloads;
using POSTest.Repositories.Interfaces;
using POSTest.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace POSTest.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductRepository(AppDbContext dbContext, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }


        public async Task<ProductPayload> CreateAsync(ProductPayload productPayload)
        {
            var product = new Product()
            {
                Name = productPayload.Name,
                Price = productPayload.Price,
            };
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            if (productPayload.Picture != null)
            {
                string UniqueFileName = "";
                string FilePath = "";
                string UploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "ProductImages");
                UniqueFileName = Guid.NewGuid().ToString() + "_" + productPayload.Picture.FileName;
                FilePath = Path.Combine(UploadsFolder, UniqueFileName);
                productPayload.Picture.CopyTo(new FileStream(FilePath, FileMode.Create));

                product.PictureUrl = $"/ProductImages/{UniqueFileName}";
                await _dbContext.SaveChangesAsync();
            }

            productPayload.Id = product.Id;

            return productPayload;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return 0;
            }
            else
            {
                EntityEntry entityEntry = _dbContext.Entry<Product>(product);
                entityEntry.State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
                return 1;
            }

        }

        public async Task<List<ProductVm>> GetAllAsync()
        {
            var products = await _dbContext.Products.Include(p => p.Sizes).ToListAsync();
            var productsVm = new List<ProductVm>();
            foreach (var product in products)
            {
                productsVm.Add(new ProductVm()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Sizes = product.Sizes,
                    PictureUrl = product.PictureUrl
                });
            }

            return productsVm;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _dbContext.Products.Include(p => p.Sizes).SingleOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task RemoveSize(int productId, Size size)
        {
            var product = await _dbContext.Products.Include(x => x.Sizes).SingleOrDefaultAsync(x => x.Id == productId);
            product.Sizes.Remove(size);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddSize(int productId, Size size)
        {
            var sizes = await _dbContext.Sizes.AddAsync(new Size() { Name = size.Name, ProductId = productId, Price = size.Price });
            await _dbContext.SaveChangesAsync();
        }


        public async Task UpdateAsync(int id, ProductVm productPayload)
        {
            var product = await _dbContext.Products.Include(x => x.Sizes).SingleOrDefaultAsync(x => x.Id == id);
            product.Name = productPayload.Name;
            product.Price = productPayload.Price;

            await _dbContext.SaveChangesAsync();

        }
    }
}
