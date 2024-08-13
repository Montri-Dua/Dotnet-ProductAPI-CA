using Microsoft.AspNetCore.Mvc;
using ProductApi.Mvc.Models;
using ProductApi.Mvc.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ProductApi.Mvc.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(ProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int? startPosition = 0, int? pageSize = 10)
        {

            _logger.LogInformation("Navigated to Index page." + startPosition + "-" + pageSize);
            int actualStartPosition = startPosition ?? 0;
            int actualPageSize = pageSize ?? 10;

            var products = await _productService.GetProductByOpcAsync(null, null,actualStartPosition, actualPageSize);
            ViewBag.StartPosition = actualStartPosition;
            ViewBag.PageSize = actualPageSize;
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Search(string opc, string clo, int? startPosition = 0, int? pageSize = 10)
        {
            _logger.LogInformation("Navigated to Index page." + opc + "-" + clo);
            int actualStartPosition = startPosition ?? 0;
            int actualPageSize = pageSize ?? 10;

            var products = await _productService.GetProductByOpcAsync(opc, clo, actualStartPosition, actualPageSize);
            ViewBag.StartPosition = actualStartPosition;
            ViewBag.PageSize = actualPageSize;

            if (products == null)
            {
                return View("NotFound");
            }

            return View("Index", products);
        }
        public async Task<JsonResult> GetProductDetails(string opc)
        {
            _logger.LogInformation("GetProductDetails.------------------>" + opc );
            /*            var product = new Product
                        {
                            OPC = "Sample Description",
                            CLO = "xx",
                            RPL = "Sample Product",

                        };*/
            var product = await _productService.GetProductByIdAsync(opc);
            _logger.LogInformation("GetProductDetails product.------------------>" + product);
            if (product != null)
            {
                return Json(new
                {
                    OPC = product.OPC,
                    CLO = product.CLO,
                    ItemCode = product.ItemCode,
                    RPL = product.RPL,
                    ModifyDateTime = product.ModifyDateTime
                });

            }
            return Json(null);
        }
    }
}
