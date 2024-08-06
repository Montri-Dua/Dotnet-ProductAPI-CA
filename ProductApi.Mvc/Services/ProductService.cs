using ProductApi.Mvc.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ProductApi.Mvc.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductService> _logger;
        public ProductService(HttpClient httpClient, ILogger<ProductService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:8080/api/products/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Product>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(int startPosition, int pageSize)
        {
            //var response = await _httpClient.GetAsync($"http://localhost:5234/api/Products");
            string url = $"http://localhost:5234/api/Products/Products?opc=null&clo=null&startposition={startPosition}&pagesize={pageSize}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<Product>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        public async Task<IEnumerable<Product>> GetProductByOpcAsync(string opc, string clo, int startPosition, int pageSize)
        {
            //var response = await _httpClient.GetAsync($"http://localhost:5234/api/Products/GetProductbyopc?opc={opc}");
            string url = $"http://localhost:5234/api/Products/Products?opc={opc}&clo={clo}&startposition={startPosition}&pagesize={pageSize}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("---->" + content);
                return JsonSerializer.Deserialize<IEnumerable<Product>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

    }
}
