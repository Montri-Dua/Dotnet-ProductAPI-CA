using Microsoft.EntityFrameworkCore;
using ProductApi.Core.Entities;
using ProductApi.Core.Interfaces;
using ProductApi.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ProductApi.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;
        private readonly ILogger<ProductRepository> _logger;
        public ProductRepository(ProductDbContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        async Task<string> IProductRepository.GethealthZ()
        {
            return await Task.FromResult("OK");
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(string opc)
        {
            return await _context.Products.SingleAsync(product => product.OPC == opc);
        }

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(string opc)
        {
            var product = await _context.Products.FindAsync(opc);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Product> GetProductByopcAsync(string opc)
        {
            return await _context.Products.SingleAsync(product => product.OPC == opc);
        }



        public async Task<IEnumerable<Product>> GetProductbyopccloAsync(string  opc, string clo, int startPosition, int pageSize)
        {
            _logger.LogInformation("1 Logging in to function GetProductbyopccloAsync " + opc + "-" + clo);

            if (opc is null && clo is not null)
            {
                return await _context.Products.Where(product => product.CLO == clo)
                    .OrderBy(x => x.OPC)
                    .Skip(startPosition)
                    .Take(pageSize)
                    .ToListAsync();
            }
            else if (opc is not null &&  clo is null)
            {
                return await _context.Products.Where(product => product.OPC == opc)
                    .OrderBy(x => x.OPC)
                    .Skip(startPosition)
                    .Take(pageSize)
                    .ToListAsync();
            }
            else if (opc is  null && clo is  null)
            {
                return await _context.Products
                    .OrderBy(x => x.OPC)
                    .Skip(startPosition)
                    .Take(pageSize)
                    .ToListAsync();
            }
            else
            {
                return await _context.Products.Where(product => product.OPC == opc && product.CLO == clo)
                  .OrderBy(x => x.OPC)
                 .Skip(startPosition)
                 .Take(pageSize)
                 .ToListAsync();
            }
        }

/*        async Task<<Product> IProductRepository.GetProductByopcAsync(string opc)
        {
            return await _context.Products.Where(Product => Product.OPC == opc).ToListAsync();
        }*/
    }
}
