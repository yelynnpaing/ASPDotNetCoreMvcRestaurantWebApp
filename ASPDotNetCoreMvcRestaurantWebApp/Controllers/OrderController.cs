using ASPDotNetCoreMvcRestaurantWebApp.Data;
using ASPDotNetCoreMvcRestaurantWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASPDotNetCoreMvcRestaurantWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Repository<Product> _products;
        private Repository<Order> _orders;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _products = new Repository<Product>(context);
            _orders = new Repository<Order>(context);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
            {
                OrderItems = new List<OrderItemViewModel>(),
                Products = await _products.GetAllAsync()
            };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddItem(int prodId, int prodQty)
        {
            var product = await _context.Products.FindAsync(prodId);
            if(product is null)
            {
                return NotFound();
            }

            //retrieve or create an OrderViewModel from session or other state management
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
            {
                OrderItems = new List<OrderItemViewModel>(),
                Products = await _products.GetAllAsync()
            };
            
            //Check if the product is already in the order
            var existingItem = model.OrderItems.FirstOrDefault(oi => oi.ProductId == prodId);

            //if the product is already in the order, update the quantity
            if(existingItem != null)
            {
                existingItem.Quantity += prodQty;
            }
            else
            {
                model.OrderItems.Add(new OrderItemViewModel
                {
                    ProductId = product.ProductId,
                    Price = product.Price,
                    Quantity = prodQty,
                    ProductName = product.Name
                });
            }

            //update the total amount 
            model.TotalAmount = model.OrderItems.Sum(oi => oi.Quantity * oi.Price);

            //save update OrderViewModel to the session
            HttpContext.Session.Set("OrderViewModel", model);

            //redirect back to create to show updated order items
            return RedirectToAction("Create", model);

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
