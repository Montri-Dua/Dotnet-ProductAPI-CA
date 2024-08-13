using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using ProductApi.Mvc.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace ProductApi.Mvc.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductService> _logger;
        private readonly IConfiguration _configuration;

        public ProductService(HttpClient httpClient, ILogger<ProductService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Product> GetProductByIdAsync(string opc)
        {
            await AddJwtTokenAsync();
            _logger.LogInformation("GetProductByIdAsync Service=====>" + opc);
            var response = await _httpClient.GetAsync($" http://localhost:5234/api/Products/Product?opc={opc}");
            if (response.IsSuccessStatusCode)
            {
               
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("GetProductByIdAsync Service 2=====>" + content);
                return JsonSerializer.Deserialize<Product>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }   
            return null;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(int startPosition, int pageSize)
        {
            string url = $"http://localhost:5234/api/Products/Products?opc=&clo=&startposition={startPosition}&pagesize={pageSize}";
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
            await AddJwtTokenAsync();
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

        private async Task AddJwtTokenAsync()
        {
            var token = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("jwt")["token"];
            _logger.LogInformation($"Jwt token: {token}");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
