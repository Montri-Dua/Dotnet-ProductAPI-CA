using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ProductApi.Core.Entities;
using ProductApi.Core.Interfaces;
using ProductApi.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductApi.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }
        async Task<string> IProductRepository.GethealthZ()
        {
            return await Task.FromResult("OK");
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
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

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
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



        public async Task<IEnumerable<Product>> GetProductbyopccloAsync(string opc, string clo, int startPosition, int pageSize)
        {
            if(opc == "null"){
                return await _context.Products.Skip(startPosition)
                        .Take(pageSize)
                        .ToListAsync();
            }else if( clo == "null"){
                return await _context.Products.Where(product => product.OPC == opc  )
                        .Skip(startPosition)
                        .Take(pageSize)
                        .ToListAsync();
            }else{
                return await _context.Products.Where(product => product.OPC == opc && product.CLO == clo )
                        .Skip(startPosition)
                        .Take(pageSize)
                        .ToListAsync();
            }
            // return await _context.Products.SingleAsync(product => product.OPC == opc);
        }

        async Task<IEnumerable<Product>> IProductRepository.GetProductByopcAsync(string opc)
        {
            return await _context.Products.Where(Product => Product.OPC == opc).ToListAsync();
        }
    }
}
