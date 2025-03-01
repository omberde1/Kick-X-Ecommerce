using System.Diagnostics;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Kick_X.Models;
using Kick_X.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Kick_X.Controllers;

// [Authorize(Roles = "Customer")]
public class CustomerController : Controller
{

    private readonly MyDBContext _context;
    public CustomerController(MyDBContext context)
    {
        _context = context;
    }

    /* ----- REGISTER | LOGIN | LOGOUT ----- */

    private static bool IsValidEmail(string email)
    {
        string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

        return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Register()
    {
        return View("LoginRegister");
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(string? custName, string? custEmail, string? custPassword, string? custRepassword)
    {
        string? name = custName?.Trim();
        string? email = custEmail?.Trim();
        string? pwd = custPassword?.Trim();
        string? repwd = custRepassword?.Trim();
        if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(pwd) && !string.IsNullOrWhiteSpace(repwd))
        {

            if (await _context.tbl_customer.AnyAsync(c => c.customer_email == email))
            {
                TempData["RegisterError"] = "registerEmailExists";
            }
            else if (pwd != repwd)
            {
                TempData["RegisterError"] = "registerPassword";
            }
            else if (!IsValidEmail(email))
            {
                TempData["RegisterError"] = "registerEmail";
            }
            else if (name.Length < 2)
            {
                TempData["RegisterError"] = "registerName";
            }
            else
            {
                var newCustomer = new Customer
                {
                    customer_name = name,
                    customer_email = email,
                    customer_password = pwd
                };
                await _context.tbl_customer.AddAsync(newCustomer);
                await _context.SaveChangesAsync();
                TempData["RegisterSuccess"] = "success";
            }
        }
        else
        {
            TempData["RegisterError"] = "registerNull";
        }
        return View("LoginRegister");
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login()
    {
        return View("LoginRegister");
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(string? custEmail, string? custPassword)
    {
        if (!string.IsNullOrWhiteSpace(custEmail) && !string.IsNullOrWhiteSpace(custPassword))
        {
            var customer = await _context.tbl_customer.FirstOrDefaultAsync(c => c.customer_email == custEmail);
            if (customer != null && customer.customer_password == custPassword)
            {
                return await SignInUser(customer.customer_id.ToString(), custEmail, "Customer");
            }
            else
            {
                TempData["LoginError"] = "loginError";
            }
        }
        else
        {
            TempData["LoginError"] = "loginEmpty";
        }
        return View("LoginRegister");
    }

    /* ----- CLAIM BASED ROLES ----- */

    private async Task<IActionResult> SignInUser(string userId, string email, string role)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.NameIdentifier, email),
            new Claim(ClaimTypes.Role, role)
        };

        var cookie = CookieAuthenticationDefaults.AuthenticationScheme;
        var claimsIdentity = new ClaimsIdentity(claims, cookie);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true
        };

        await HttpContext.SignInAsync
        (
            cookie,
            new ClaimsPrincipal(claimsIdentity),
            authProperties
        );

        if (role == "Customer") return RedirectToAction("Home", "Customer");

        return View("LoginRegister");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return View("LoginRegister");
    }



    /* ----- UPPER NAV ----- */

    private int GetLoggedInCustomerId()
    {
        var customerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (customerId != null)
        {
            return int.Parse(customerId);
        }
        else
        {
            return -1;
        }
    }

    // 1) UPPER NAV - Profile
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> Profile()
    {
        int customerId = GetLoggedInCustomerId();
        var customer = await _context.tbl_customer.FirstOrDefaultAsync(x => x.customer_id == customerId);
        if (customerId == -1 || customer == null)
        {
            return RedirectToAction("Home");
        }
        return View(customer);
    }

    [Authorize(Roles = "Customer")]
    [HttpPost]
    public async Task<IActionResult> ProfileEdit(Customer customer)
    {
   
        _context.tbl_customer.Update(customer);
        await _context.SaveChangesAsync();
        
        return RedirectToAction("Profile");
    }


    // 2) UPPER NAV - Cart
    [Authorize(Roles = "Customer")]
    [HttpGet]
    public async Task<IActionResult> Cart()
    {
        var customerId = GetLoggedInCustomerId();
        if (customerId != -1)
        {
            var customerCart = await _context.tbl_cartitem
            .Include(p => p.Product)
            .Where(item => item.customer_id == customerId)
            .ToListAsync();
            if (customerCart != null && customerCart.Any())
            {
                return View(customerCart);
            }
        }
        return View(new List<CartItem>());
    }

    [Authorize(Roles = "Customer")]
    [HttpPost]
    public async Task<IActionResult> CartUnit(string productId, string productQuantity)
    {
        var customerId = GetLoggedInCustomerId();
        var product = await _context.tbl_product.FirstOrDefaultAsync(p => p.product_id == int.Parse(productId));
        int quantity = int.Parse(productQuantity);

        if (customerId == -1 || product == null)
        {
            return Json(new { success = false, message = "Invalid request" });
        }

        var existingProduct = await _context.tbl_cartitem
        .FirstOrDefaultAsync(c => c.customer_id == customerId && c.product_id == product.product_id);

        if (existingProduct != null)
        {
            int existingQuantity = existingProduct.cartitem_product_quantity + quantity;
            if (existingQuantity >= 11)
            {
                return Json(new { success = false, message = "Only 10 quantity per product allowed" });
            }
            else if (existingQuantity <= 0)
            {
                return Json(new { success = false, message = "Quantity must be atleast 1" });
            }
            else
            {
                existingProduct.cartitem_product_quantity += quantity;
            }
        }
        else
        {
            if (quantity >= 11)
            {
                return Json(new { success = false, message = "Only 10 quantity per product allowed" });
            }
            else if (quantity <= 0)
            {
                return Json(new { success = false, message = "Quantity must be atleast 1" });
            }
            else
            {
                var cartItem = new CartItem
                {
                    customer_id = customerId,
                    product_id = product.product_id,
                    cartitem_product_quantity = quantity
                };
                await _context.tbl_cartitem.AddAsync(cartItem);
            }
        }
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Added to cart!" });
    }

    [Authorize(Roles = "Customer")]
    [HttpPost]
    public async Task<IActionResult> CartAll(string productId, string productQuantity)
    {
        var customerId = GetLoggedInCustomerId();
        var product = await _context.tbl_product.FirstOrDefaultAsync(p => p.product_id == int.Parse(productId));
        int quantity = int.Parse(productQuantity);

        if (customerId == -1 || product == null)
        {
            return Json(new { success = false, message = "Invalid request" });
        }

        var existingProduct = await _context.tbl_cartitem
        .FirstOrDefaultAsync(c => c.customer_id == customerId && c.product_id == product.product_id);
        if (existingProduct != null)
        {
            int existingQuantity = existingProduct.cartitem_product_quantity + quantity;
            if (existingQuantity >= 11)
            {
                return Json(new { success = false, message = "Only 10 quantity per product allowed" });
            }
            else if (existingQuantity <= 0)
            {
                return Json(new { success = false, message = "Quantity must be at least 1" });
            }
            else
            {
                existingProduct.cartitem_product_quantity = quantity;
            }
            // _context.tbl_cartitem.Update(existingProduct);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Cart updated successfully!" });
        }
        else
        {
            return Json(new { success = false, message = "Product not found in cart" });
        }
    }

    [Authorize(Roles = "Customer")]
    [HttpPost]
    public async Task<IActionResult> CartRemove(string productId)
    {
        var customerId = GetLoggedInCustomerId();
        var product = await _context.tbl_product.FirstOrDefaultAsync(p => p.product_id == int.Parse(productId));

        if (customerId == -1 || product == null)
        {
            return Json(new { success = false, message = "Invalid request" });
        }

        var existingProduct = await _context.tbl_cartitem
        .FirstOrDefaultAsync(c => c.customer_id == customerId && c.product_id == product.product_id);
        if (existingProduct != null)
        {
            _context.tbl_cartitem.Remove(existingProduct);
            await _context.SaveChangesAsync();

            bool isCartEmpty = !await _context.tbl_cartitem.AnyAsync(c => c.customer_id == customerId);
            if (isCartEmpty)
            {
                return Json(new { success = true, message = "Success. Cart empty!" });
            }
            else
            {
                return Json(new { success = true, message = "Removed from cart!" });
            }
        }
        else
        {
            return Json(new { success = true, message = "Product not found in cart" });
        }
    }


    /* ----- LOWER NAV ----- */


    // 1) LOWER NAV - Home
    [AllowAnonymous]
    public async Task<ActionResult> Home()
    {
        ViewData["ActivePage"] = "Home";

        ViewData["FeaturedProducts"] = await _context.tbl_product
        .Take(6)
        .ToListAsync();

        return View();
    }

    // 2) LOWER NAV - Store
    [AllowAnonymous]
    public async Task<ActionResult> Store(string? orderBy, int page = 1)
    {
        ViewData["ActivePage"] = "Store";
        int pageSize = 10;
        int totalProducts = await _context.tbl_product.CountAsync();

        var query = _context.tbl_product.AsQueryable();

        switch (orderBy)
        {
            case "low-high":
                query = query.OrderBy(p => p.product_price);
                break;
            case "high-low":
                query = query.OrderByDescending(p => p.product_price);
                break;
            case "A-Z":
                query = query.OrderBy(p => p.product_name);
                break;
            case "Z-A":
                query = query.OrderByDescending(p => p.product_name);
                break;
            default:
                query = query.OrderBy(p => p.product_id); // Default ordering
                break;
        }

        var products = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        ViewData["TotalProducts"] = totalProducts;
        ViewData["PageSize"] = pageSize;
        ViewData["CurrentPage"] = page;

        return View(products);
    }

    // 2a) LOWER NAV - Store - Store Search
    [AllowAnonymous]
    public async Task<ActionResult> StoreSearch(string searchInput)
    {
        ViewData["ActivePage"] = "Store";

        var products = await _context.tbl_product
        .Where(p => p.product_name!.ToLower().Contains(searchInput.ToLower()))
        .ToListAsync();
        return View("Store", products);
    }

    // 2b) LOWER NAV - Store - Product Unit
    [AllowAnonymous]
    public async Task<ActionResult> ProductUnit(int? id)
    {
        ViewData["ActivePage"] = "Store";
        ViewData["FeaturedProducts"] = await _context.tbl_product
        .Take(6)
        .ToListAsync();

        var product = await _context.tbl_product
        .Include(c => c.Category)
        .FirstOrDefaultAsync(p => p.product_id == id);
        if (id != null && product != null)
        {
            return View(product);
        }
        else
        {
            return View(null);
        }

    }

    // 3) LOWER NAV - Orders
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> Order()
    {
        ViewData["ActivePage"] = "Order";
        var customerId = GetLoggedInCustomerId();
        var customerOrders = await _context.tbl_order
        .Where(o => o.customer_id == customerId)
        .ToListAsync();

        if (customerId == -1 || customerOrders == null || customerOrders.Count == 0)
        {
            return View(new List<Order>());
        }

        return View(customerOrders);
    }

    // 4) LOWER NAV - About
    [AllowAnonymous]
    public ActionResult About()
    {
        ViewData["ActivePage"] = "About";
        return View();
    }

    /* ----- Checkout ----- */
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> Checkout()
    {

        var customerId = GetLoggedInCustomerId();

        var customer = await _context.tbl_customer.FirstOrDefaultAsync(c => c.customer_id == customerId);
        var cartItem = await _context.tbl_cartitem
        .Include(p => p.Product)
        .Where(c => c.customer_id == customerId)
        .ToListAsync();

        if (customer == null || cartItem == null || cartItem.Count == 0)
        {
            return View("Home");
        }

        var model = new CheckoutViewModel
        {
            Customer = customer,
            CartItems = cartItem ?? new List<CartItem>()
        };

        return View(model);

    }

    [Authorize(Roles = "Customer")]
    [HttpPost]
    public async Task<IActionResult> Checkout(CheckoutViewModel model)
    {
        int customerId = GetLoggedInCustomerId();

        var customer = await _context.tbl_customer.FindAsync(customerId);

        if (customer == null)
        {
            return RedirectToAction("Home");
        }

        if (ModelState.IsValid)
        {
            customer.customer_name = model.Customer!.customer_name;
            customer.customer_address = model.Customer.customer_address;
            customer.customer_city = model.Customer.customer_city;
            customer.customer_pincode = model.Customer.customer_pincode;
            customer.customer_email = model.Customer.customer_email;
            customer.customer_phone = model.Customer.customer_phone;
            customer.customer_country = model.Customer.customer_country;
            _context.tbl_customer.Update(customer);
            await _context.SaveChangesAsync();
            return await GenerateOrder();
        }

        return View(model);
    }


    private async Task<IActionResult> GenerateOrder()
    {
        int customerId = GetLoggedInCustomerId();
        var cartItems = await _context.tbl_cartitem
        .Include(p => p.Product)
        .Where(c => c.customer_id == customerId)
        .ToListAsync();

        if (customerId == -1 || cartItems == null || cartItems.Count == 0)
        {
            return View("Home");
        }

        string uniqueOrderId;
        do
        {
            uniqueOrderId = "#" + Guid.NewGuid().ToString().Substring(0, 7);
        }
        while (await _context.tbl_order.AnyAsync(o => o.order_uid == uniqueOrderId));

        var order = new Order
        {
            order_uid = uniqueOrderId,
            order_subtotal = 0,
            customer_id = customerId
        };

        await _context.tbl_order.AddAsync(order);
        await _context.SaveChangesAsync();
        var currentOrder = await _context.tbl_order.FirstOrDefaultAsync(o => o.order_uid == uniqueOrderId);
        // find / find async searches for Primary Key!

        if (currentOrder == null) return RedirectToAction("Error404", "Home");

        decimal orderTotal = 0;
        List<OrderItem> orderItemsList = new List<OrderItem>();

        foreach (var item in cartItems)
        {
            orderTotal += item.Product!.product_price * item.cartitem_product_quantity;
            var orderItem = new OrderItem
            {
                orderitem_product_quantity = item.cartitem_product_quantity,
                product_id = item.product_id,
                customer_id = customerId,
                order_id = currentOrder.order_id
            };
            orderItemsList.Add(orderItem);
            _context.tbl_cartitem.Remove(item);
        }

        currentOrder.order_subtotal = orderTotal;
        await _context.tbl_orderitem.AddRangeAsync(orderItemsList);
        await _context.SaveChangesAsync();
        TempData["OrderPlaced"] = true;
        TempData["OrderID"] = uniqueOrderId;
        return RedirectToAction("OrderSuccess");
    }

    [Authorize(Roles = "Customer")]
    public IActionResult OrderSuccess()
    {
        if (TempData["OrderPlaced"] == null)
        {
            return RedirectToAction("Home");  // Prevents direct access
        }
        return View();
    }

    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> OrderDetails(int orderId)
    {
        int customerId = GetLoggedInCustomerId();
        var orderItems = await _context.tbl_orderitem
        .Include(p => p.Product)
        .Where(oi => oi.customer_id == customerId && oi.order_id == orderId)
        .ToListAsync();

        if (customerId == -1 || orderItems == null || orderItems.Count == 0)
        {
            return View("Order");
        }

        return View(orderItems);
    }


    // ----- ERROR PAGE -----
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}