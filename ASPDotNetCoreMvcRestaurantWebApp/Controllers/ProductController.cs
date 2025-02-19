using ASPDotNetCoreMvcRestaurantWebApp.Data;
using ASPDotNetCoreMvcRestaurantWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPDotNetCoreMvcRestaurantWebApp.Controllers
{
    public class ProductController : Controller
    {
        private Repository<Product> products;

        public ProductController(ApplicationDbContext context)
        {
            products = new Repository<Product>(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await products.GetAllAsync());
        }


        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            if(id == 0)
            {
                ViewBag.Title = "Add";
                return View(new Product());
            }
            else
            {
                ViewBag.Title = "Edit";
                return View();
            }
        }
    }
}
