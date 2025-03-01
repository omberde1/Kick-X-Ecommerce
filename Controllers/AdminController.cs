using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kick_X.Models;
using Kick_X.ViewModels.DashboardViewModel;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Kick_X.Controllers;

[Authorize(Policy = "AdminOnly")]
public class AdminController : Controller
{
    private readonly MyDBContext _context;

    public AdminController(MyDBContext context)
    {
        _context = context;
    }

    // Login - Logout
    [AllowAnonymous]
    [HttpGet]
    [Route("/Admin/Login", Name = "AdminLogin")]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true && User.IsInRole("Admin"))
        {
            return RedirectToAction("Home", "Admin"); // Redirect to admin dashboard or home
        }
        return View();
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(string adminEmail, string adminPassword)
    {
        var admin = await _context.tbl_admin.FirstOrDefaultAsync(x => x.admin_email == adminEmail);
        if (admin != null && admin.admin_password == adminPassword)
        {
            // HttpContext.Session.SetString("admin_session", admin.admin_id.ToString());

            return await SignInUser(admin.admin_id.ToString(), adminEmail, "Admin");
        }
        else
        {
            ViewBag.errorMessage = "Incorrect Admin Email or Password*";
            return View();
        }
    }

    private async Task<IActionResult> SignInUser(string adminId, string adminEmail, string role)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, adminId),
            new Claim(ClaimTypes.Email, adminEmail),
            new Claim(ClaimTypes.Role, role),
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true
        };

        await HttpContext.SignInAsync
        (
        CookieAuthenticationDefaults.AuthenticationScheme,
        new ClaimsPrincipal(claimsIdentity),
        authProperties
        );

        if (role == "Admin") return RedirectToAction("Home", "Admin");
        return RedirectToAction("Login");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    // Sidebar Actions
    public async Task<IActionResult> Home()
    {
        ViewData["ActivePage"] = "Home";

        var customers = await _context.tbl_customer
        .ToListAsync();
        var orders = await _context.tbl_order
        .ToListAsync();
        var model = new DashboardViewModel
        {
            Customers = customers,
            Orders = orders
        };
        return View(model);
    }

    // 2
    public async Task<IActionResult> Profile()
    {
        var adminId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (adminId != null)
        {
            var admin_row = await _context.tbl_admin.FirstOrDefaultAsync(x => x.admin_id == int.Parse(adminId));
            return View(admin_row);
        }
        else
        {
            return RedirectToAction("Login");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Profile(Admin admin)
    {
        _context.tbl_admin.Update(admin);
        await _context.SaveChangesAsync();
        return RedirectToAction("Profile");
    }

    // 3
    public async Task<IActionResult> Customer(int page = 1)
    {
        ViewData["ActivePage"] = "Customer";

        int pageSize = 10;
        int totalCustomers = await _context.tbl_customer.CountAsync();

        var customers = await _context.tbl_customer
        .OrderBy(c => c.customer_id)
        .Skip((page - 1) * 10)
        .Take(pageSize)
        .ToListAsync();

        ViewData["TotalCustomers"] = totalCustomers;
        ViewData["PageSize"] = pageSize;
        ViewData["CurrentPage"] = page;
        return View(customers);
    }

    [HttpGet]
    public async Task<IActionResult> CustomerDetails(int? id)
    {
        var customer = await _context.tbl_customer.FirstOrDefaultAsync(c => c.customer_id == id);

        ViewData["ActivePage"] = "Customer";
        if (customer != null)
        {
            return View(customer);
        }
        else
        {
            return BadRequest("Try Again. Enter correct Customer ID.");
        }
    }

    public async Task<IActionResult> CustomerEdit(int? id)
    {
        var customer = await _context.tbl_customer.FirstOrDefaultAsync(cu => cu.customer_id == id);

        ViewData["ActivePage"] = "Customer";
        if (customer != null)
        {
            return View(customer);
        }
        else
        {
            return BadRequest("Try Again. Enter correct Customer ID.");
        }
    }
    [HttpPost]
    public async Task<IActionResult> CustomerEdit(Customer customer)
    {
        ViewData["ActivePage"] = "Customer";
        _context.tbl_customer.Update(customer);
        await _context.SaveChangesAsync();
        return RedirectToAction("Customer");
    }
    public async Task<IActionResult> CustomerDelete(int id)
    {
        ViewData["ActivePage"] = "Customer";
        var customer = await _context.tbl_customer.FirstOrDefaultAsync(cu => cu.customer_id == id);
        if (customer != null)
        {
            return View(customer);
        }
        else
        {
            return BadRequest("Try Again. Enter correct Customer ID.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CustomerDelete(int? customerId)
    {
        ViewData["ActivePage"] = "Customer";
        var customer = await _context.tbl_customer.FirstOrDefaultAsync(cu => cu.customer_id == customerId);
        if (customer != null)
        {
            _context.tbl_customer.Remove(customer);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Customer");
    }

    // 4
    public async Task<IActionResult> Category(int page = 1) // Fetch All Categories
    {
        ViewData["ActivePage"] = "Category";
        int pageSize = 5;
        int totalCategories = await _context.tbl_category.CountAsync();

        var categories = await _context.tbl_category
        .OrderBy(c => c.category_id)
        .Skip((page - 1) * 5)
        .Take(pageSize)
        .ToListAsync();

        ViewData["TotalCategories"] = totalCategories;
        ViewData["PageSize"] = pageSize;
        ViewData["CurrentPage"] = page;
        return View(categories);
    }
    public IActionResult CategoryAdd()
    {
        ViewData["ActivePage"] = "Category";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CategoryAdd(Category category)
    {
        ViewData["ActivePage"] = "Category";
        await _context.tbl_category.AddAsync(category);
        await _context.SaveChangesAsync();
        return RedirectToAction("Category");
    }
    public async Task<IActionResult> CategoryEdit(int id)
    {
        var category = await _context.tbl_category.FirstOrDefaultAsync(ct => ct.category_id == id);

        ViewData["ActivePage"] = "Category";
        if (category != null)
        {
            return View(category);
        }
        else
        {
            return BadRequest("Try Again. Enter correct Category ID.");
        }
    }
    [HttpPost]
    public async Task<IActionResult> CategoryEdit(Category category)
    {
        ViewData["ActivePage"] = "Category";
        _context.tbl_category.Update(category);
        await _context.SaveChangesAsync();
        return RedirectToAction("Category");
    }
    public async Task<IActionResult> CategoryDelete(int id)
    {
        var category = await _context.tbl_category.FirstOrDefaultAsync(ct => ct.category_id == id);

        ViewData["ActivePage"] = "Category";
        if (category != null)
        {
            return View(category);
        }
        else
        {
            return BadRequest("Try Again. Enter correct Category ID.");
        }
    }
    [HttpPost]
    public async Task<IActionResult> CategoryDelete(int? categoryId)
    {
        ViewData["ActivePage"] = "Category";
        var category = await _context.tbl_category.FirstOrDefaultAsync(ct => ct.category_id == categoryId);
        if (category != null)
        {
            _context.tbl_category.Remove(category);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Category");
    }



    // 5    
    public async Task<IActionResult> Product(int page = 1) // Fetch All Products
    {
        ViewData["ActivePage"] = "Product";
        int pageSize = 10;
        int totalProducts = await _context.tbl_product.CountAsync();

        var products = await _context.tbl_product
        .Include(p => p.Category)
        .OrderBy(p => p.product_id)
        .Skip((page - 1) * 10)
        .Take(pageSize)
        .ToListAsync();

        ViewData["TotalProducts"] = totalProducts;
        ViewData["PageSize"] = pageSize;
        ViewData["CurrentPage"] = page;
        return View(products);
    }
    public async Task<IActionResult> ProductAdd()
    {
        ViewData["ActivePage"] = "Product";
        ViewBag.Categories = await _context.tbl_category.ToListAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ProductAdd(Product product, IFormFile product_image)
    {
        ViewData["ActivePage"] = "Product";

        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Product_Img");

        // Ensure the folder exists
        Directory.CreateDirectory(uploadsFolder);

        // Use the original file name
        string fileName = product_image.FileName;
        string filePath = Path.Combine(uploadsFolder, fileName);

        // Save the file
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await product_image.CopyToAsync(fileStream);
        }

        // Save the image name in the database
        product.product_image = fileName;

        await _context.tbl_product.AddAsync(product);
        await _context.SaveChangesAsync();
        return RedirectToAction("Product");
    }
    public async Task<IActionResult> ProductEdit(int id)
    {
        ViewData["ActivePage"] = "Product";
        var product = await _context.tbl_product.FirstOrDefaultAsync(p => p.product_id == id);
        if (product != null)
        {
            ViewBag.categories = await _context.tbl_category.ToListAsync();
            return View(product);
        }
        else
        {
            return BadRequest("Enter correct Product ID.");
        }
    }
    [HttpPost]
    public async Task<IActionResult> ProductEdit(Product product, IFormFile product_image)
    {
        ViewData["ActivePage"] = "Product";
        var existingProduct = await _context.tbl_product.FirstOrDefaultAsync(ep => ep.product_id == product.product_id);
        if (existingProduct == null)
        {
            return BadRequest("Enter correct Product ID.");
        }
        existingProduct.product_name = product.product_name;
        existingProduct.product_description = product.product_description;
        existingProduct.product_price = product.product_price;
        existingProduct.category_id = product.category_id;

        if (product_image != null && product_image.Length > 0)
        {
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Product_Img");

            // Ensure the folder exists
            Directory.CreateDirectory(uploadsFolder);

            // Use the original file name
            string fileName = product_image.FileName;
            string filePath = Path.Combine(uploadsFolder, fileName);

            // Save the file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await product_image.CopyToAsync(fileStream);
            }
            // Save the image name in the database
            existingProduct.product_image = fileName;
        }

        _context.tbl_product.Update(existingProduct);
        await _context.SaveChangesAsync();
        return RedirectToAction("Product");
    }
    public async Task<IActionResult> ProductDelete(int id)
    {
        ViewData["ActivePage"] = "Product";
        var product = await _context.tbl_product
        .Include(p => p.Category)
        .FirstOrDefaultAsync(p => p.product_id == id);
        if (product != null)
        {
            ViewBag.category = product.Category?.category_name;
            return View(product);
        }
        else
        {
            return BadRequest("Try Again. Enter correct Product ID.");
        }
    }
    [HttpPost]
    public async Task<IActionResult> ProductDelete(int? productId)
    {
        ViewData["ActivePage"] = "Product";
        var product = await _context.tbl_product.FirstOrDefaultAsync(p => p.product_id == productId);
        if (product != null)
        {
            string? productImage = product.product_image;
            if (!string.IsNullOrEmpty(productImage))
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Product_Img");
                string imagePath = Path.Combine(uploadsFolder, productImage);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _context.tbl_product.Remove(product);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Product");
    }


    // 6
    public async Task<IActionResult> Order(int orderStatus)
    {
        ViewData["ActivePage"] = "Order";

        var query = _context.tbl_order.AsQueryable();

        switch (orderStatus)
        {
            case 1:
                query = query.Where(o => o.order_status == "Pending");
                break;
            case 2:
                query = query.Where(o => o.order_status == "Shipped");
                break;
            case 3:
                query = query.Where(o => o.order_status == "Delivered");
                break;
            default:
                break;
        }
        
        var order = await query
        .ToListAsync();

        return View(order);
    }

    public async Task<IActionResult> OrderEdit(int id)
    {
        ViewData["ActivePage"] = "Order";
        var order = await _context.tbl_order
        .Include(o => o.Customer)
        .FirstOrDefaultAsync(o => o.order_id == id);
        
        if(order == null)
        {
            return RedirectToAction("Order");
        }
        return View(order);
    }
    [HttpPost]
    public async Task<IActionResult> OrderEdit(int orderId, string orderStatus)
    {
        ViewData["ActivePage"] = "Order";
        var statuses = new List<string> {"Pending", "Shipped", "Delivered"};
        var order = await _context.tbl_order.FirstOrDefaultAsync(o => o.order_id == orderId);
        if(order == null || !statuses.Contains(orderStatus))
        {
            return RedirectToAction("Order");
        }
        order.order_status = orderStatus;
        _context.tbl_order.Update(order);
        await _context.SaveChangesAsync();
        return RedirectToAction("Order");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
