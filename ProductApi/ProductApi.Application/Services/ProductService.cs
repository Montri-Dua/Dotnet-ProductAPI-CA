using ProductApi.Core.Entities;
using ProductApi.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
namespace ProductApi.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<string> GethealthZ(){
            return _productRepository.GethealthZ();
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return _productRepository.GetAllProductsAsync();
        }

        public Task<Product> GetProductByIdAsync(string opc)
        {
            return _productRepository.GetProductByIdAsync(opc);
        }

        public Task AddProductAsync(Product product)
        {
            return _productRepository.AddProductAsync(product);
        }

        public Task UpdateProductAsync(Product product)
        {
            return _productRepository.UpdateProductAsync(product);
        }

        public Task DeleteProductAsync(string opc)
        {
            return _productRepository.DeleteProductAsync(opc);
        }

/*        public Task<IEnumerable<Product>> GetProductByopcAsync(string opc)
        {
            return _productRepository.GetProductByopcAsync(opc);
        }*/

        public Task<IEnumerable<Product>> GetProductbyopcclo(string opc, string clo, int startposition, int pagesize)
        {
            return _productRepository.GetProductbyopccloAsync(opc, clo, startposition, pagesize);
        }
    }
}
