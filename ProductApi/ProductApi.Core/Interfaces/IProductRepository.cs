using System.Collections.Generic;
using System.Threading.Tasks;
using ProductApi.Core.Entities;
namespace ProductApi.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<string> GethealthZ();
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<Product>> GetProductByopcAsync(string opc);
        Task<IEnumerable<Product>> GetProductbyopccloAsync(string opc, string clo, int startposition, int pagesize);
    }
}
