using POSTest.Models;
using POSTest.Payloads;
using POSTest.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POSTest.Repositories.Interfaces
{
    public interface IProductRepository
    {
        //CRUD --> Create, Read, Update, Delete /// ViewModel Payload 
        Task<ProductPayload> CreateAsync(ProductPayload product);
        Task UpdateAsync(int id, ProductVm product);
        Task<int> DeleteAsync(int id);
        Task<Product> GetByIdAsync(int id);
        Task<List<ProductVm>> GetAllAsync();
        Task AddSize(int productId, Size size);
        Task RemoveSize(int productId, Size size);

    }
}
