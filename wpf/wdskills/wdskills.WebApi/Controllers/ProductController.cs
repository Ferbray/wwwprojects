using Microsoft.AspNetCore.Mvc;
using wdskills.WebApi.Service;
using wdskills.WebServer;
using wdskills.WebServer.Model;

namespace wdskills.WebApi.Controllers
{
    [ApiController]
    [Route("api/Products")]
    public class ProductController : ControllerBase
    {
        private static Random random = new();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly ILogger<ProductController> _logger;
        private readonly AppDbService _appDbService;
        private readonly DbValidationService _validationService;

        public ProductController(
            ILogger<ProductController> logger,
            AppDbService appDbService,
            DbValidationService validationService)
        {
            _logger = logger;
            _appDbService = appDbService;
            _validationService = validationService;
        }

        [HttpGet]
        [Route("GetAllProduct")]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return Ok(await _appDbService.GetProductsListAsync());
        }

        [HttpGet]
        [Route("GetAllCategory")]
        public async Task<ActionResult<IEnumerable<string>>> GetCategory()
        {
            return Ok(await _appDbService.GetCategoriesListAsync());
        }

        [HttpGet]
        [Route("GetAllProvider")]
        public async Task<ActionResult<IEnumerable<string>>> GetProvider()

        {
            return Ok(await _appDbService.GetProviderListAsync());
        }

        [HttpPost]
        [Route("AddNewProduct")]
        public async Task<ActionResult<Product>> Post(string? login, string? password, Product product) {
            string articleNumber;
            Product? findIdenticalProduct;

            do
            {
                articleNumber = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
                findIdenticalProduct = await _appDbService.FindProductToArticleAsync(articleNumber);
            }
            while (findIdenticalProduct is not null);

            product.ProductArticleNumber = articleNumber;
            string answerValidNewProduct = await _validationService.IsValidAddNewProductAsync(login, password, product);

            if(answerValidNewProduct == string.Empty)
            {
                await _appDbService.AddProductAsync(product);
                _logger.LogInformation("Add new product", product.ProductArticleNumber);
                return Ok($"Продукт {product.ProductArticleNumber} добавлен");
            }

            return NotFound(answerValidNewProduct);
        }

        [HttpPost]
        [Route("UpdateProduct")]
        public async Task<ActionResult<Product>> PostUpdate(string? login, string? password, Product product)
        {
            string answerValidUpdateProduct = await _validationService.IsValidUpdateProductAsync(login, password, product);
            if (answerValidUpdateProduct == string.Empty)
            {
                await _appDbService.UpdateProductToAsync(product);
                _logger.LogInformation("Update product", product.ProductArticleNumber);
                return Ok($"Продукт {product.ProductArticleNumber} изменен");
            }
            return NotFound(answerValidUpdateProduct);
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<ActionResult<Product>> Delete(string? login, string? password, string? articleProduct)
        {
            string answerValidDeleteProduct = await _validationService.IsValidDeleteProductAsync(login, password, articleProduct);
            if (answerValidDeleteProduct == string.Empty)
            {
                await _appDbService.DeleteProductAsync(articleProduct!);
                _logger.LogInformation("Delete product", articleProduct);
                return Ok($"Продукт {articleProduct} удален");
            }
            return NotFound(answerValidDeleteProduct);
        }
    }
}