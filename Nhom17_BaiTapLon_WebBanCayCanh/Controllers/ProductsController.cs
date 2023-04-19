using Microsoft.AspNetCore.Mvc;
using Nhom17_BaiTapLon_WebBanCayCanh.Dao;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;
using Nhom17_BaiTapLon_WebBanCayCanh.ViewModel;
using Nhom17_BaiTapLon_WebBanCayCanh.ViewModels;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public ProductsController(IWebHostEnvironment enviroment, IConfiguration configuration)
        {
            _environment = enviroment;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult Create()
        {
            List<Category> categories = CategoryDao.GetCategoiesSelection(_configuration);
            return new JsonResult(categories);
        }
        public JsonResult GetProducts()
        {
            List<Product> products = ProductDao.GetProducts(_configuration);

            return new JsonResult(products);
        }
        [HttpPost]
        public JsonResult Create(ProductViewModel product)
        {
            string image = UploadFile(product);
            
            Product model = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Image = image,
                Availability = product.Availability,
                CategoryId = product.CategoryId
            };
            ProductDao.CreateProduct(_configuration, model);

            return new JsonResult("success");
        }
        public JsonResult Update(int id)
        {
            Product product = ProductDao.GetProduct(_configuration, id);
            List<Category> categories = CategoryDao.GetCategoiesSelection(_configuration);
            
            ProductWithCategories productWithCategories = new ProductWithCategories
            {
                Product = product,
                Categories = categories
            };

            return new JsonResult(productWithCategories);
        }
        [HttpPost]
        public JsonResult Update(ProductViewModel product)
        {
            Product oldProduct = ProductDao.GetProduct(_configuration, product.Id);
            
            string image;
            if (product.ProfileImage != null)
            {
                image = UpdateFile(product, oldProduct.Image);
            } else
            {
                image = oldProduct.Image;
            }

            Product model = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Image = image,
                Availability = product.Availability,
                CategoryId = product.CategoryId
            };
            ProductDao.UpdateProduct(_configuration, model);

            return new JsonResult(model);
        }
        public JsonResult Details(int id)
        {
            Product product = ProductDao.GetProductWithCategoryAndOptions(_configuration, id);

            return new JsonResult(product);
        }
        private string UploadFile(ProductViewModel product)
        {
            string uniqueFileName = null;
            if (product.ProfileImage != null)
            {
                string uploadFolder = Path.Combine(_environment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ProfileImage.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    product.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private string UpdateFile(ProductViewModel product, string existingFileName)
        {
            string newFileName = null;
            if (product.ProfileImage != null)
            {
                string uploadFolder = Path.Combine(_environment.WebRootPath, "images");
                newFileName = Guid.NewGuid().ToString() + "_" + product.ProfileImage.FileName;
                string filePath = Path.Combine(uploadFolder, newFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    product.ProfileImage.CopyTo(fileStream);
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
