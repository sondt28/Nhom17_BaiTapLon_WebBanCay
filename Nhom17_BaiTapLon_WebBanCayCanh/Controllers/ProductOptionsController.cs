using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nhom17_BaiTapLon_WebBanCayCanh.Dao;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Controllers
{
    public class ProductOptionsController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public ProductOptionsController(IWebHostEnvironment enviroment, IConfiguration configuration)
        {
            _environment = enviroment;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            ProductOptionViewModel model = ProductOptionDao.GetProductOptions(_configuration);
            return View(model);
        }
        public IActionResult Create()
        {
            List<Product> product = ProductDao.GetProducstSelect(_configuration);
            ProductOptionViewModel model = new ProductOptionViewModel();
            model.ProductList = product.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }).ToList();
            return new JsonResult(model);
        }
        [HttpPost]
        public IActionResult Create(ProductOptionViewModel productOption)
        {
            string image = UploadFile(productOption);

            ProductOption model = new ProductOption
            {
                Value = productOption.Value,
                Image = image,
                Price = productOption.Price,
                StockQuantity = productOption.StockQuantity,
                ProductId = productOption.ProductId
            };
            ProductOptionDao.CreateProductOption(_configuration, model);

            return RedirectToAction("Index", "ProductOptions");
        }
        public IActionResult Update(int id)
        {
            ProductOption productOption = ProductOptionDao.GetProductOption(_configuration, id);
            List<Product> products = ProductDao.GetProducstSelect(_configuration);

            ProductOptionViewModel model = new ProductOptionViewModel()
            {
                Id = productOption.Id,
                Value = productOption.Value,
                Price = productOption.Price,
                StockQuantity = productOption.StockQuantity,
                ProductId = productOption.Product.Id,
                ProductList = products.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }).ToList()
            };

            return new JsonResult(model);
        }
        [HttpPost]
        public IActionResult Update(ProductOptionViewModel productOption)
        {
            ProductOption oldProductOption = ProductOptionDao.GetProductOption(_configuration, productOption.Id);

            string image;
            if (productOption.ProfileImage != null)
            {
                image = UpdateFile(productOption, oldProductOption.Image);
            }
            else
            {
                image = oldProductOption.Image;
            }

            ProductOption model = new ProductOption
            {
                Id = productOption.Id,
                Value = productOption.Value,
                Image = image,
                Price = productOption.Price,
                StockQuantity = productOption.StockQuantity,
                ProductId = productOption.ProductId
            };
            ProductOptionDao.UpdateProductOption(_configuration, model);

            return RedirectToAction("Index", "ProductOptions");
        }
        public IActionResult Details(int id)
        {
            ProductOption productOption = ProductOptionDao.GetProductOption(_configuration, id);

            ProductOptionViewModel model = new ProductOptionViewModel()
            {
                Id = productOption.Id,
                Value = productOption.Value,
                Price = productOption.Price,
                StockQuantity = productOption.StockQuantity,
                ProductId = productOption.Product.Id,
                ProductName = productOption.Product.Name,
                Image = productOption.Image
            };

            return new JsonResult(model);
        }
        public IActionResult Delete(int id)
        {
            ProductOption category = ProductOptionDao.GetProductOption(_configuration, id);
            ProductOptionViewModel model = new ProductOptionViewModel()
            {
                Id = category.Id,
                Value = category.Value
            };
            return new JsonResult(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ProductOptionViewModel model)
        {
            ProductOptionDao.DeleteProductOption(_configuration, model.Id);

            return RedirectToAction("Index", "ProductOptions");
        }
        private string UploadFile(ProductOptionViewModel productOption)
        {
            string uniqueFileName = null;
            if (productOption.ProfileImage != null)
            {
                string uploadFolder = Path.Combine(_environment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + productOption.ProfileImage.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    productOption.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private string UpdateFile(ProductOptionViewModel productOption, string existingFileName)
        {
            string newFileName = null;
            if (productOption.ProfileImage != null)
            {
                string uploadFolder = Path.Combine(_environment.WebRootPath, "images");
                newFileName = Guid.NewGuid().ToString() + "_" + productOption.ProfileImage.FileName;
                string filePath = Path.Combine(uploadFolder, newFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    productOption.ProfileImage.CopyTo(fileStream);
                }
                if (!string.IsNullOrEmpty(existingFileName))
                {
                    string oldFilePath = Path.Combine(uploadFolder, existingFileName);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
            }
            else
            {
                newFileName = existingFileName;
            }

            return newFileName;
        }
    }
}
