using Microsoft.AspNetCore.Mvc;
using Nhom17_BaiTapLon_WebBanCayCanh.Dao;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;
using Nhom17_BaiTapLon_WebBanCayCanh.ViewModel;

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
        public JsonResult GetProducts()
        {

            return new JsonResult("");
        }

        public IActionResult Create()
        {
            return View();
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
    }
}
