using Microsoft.AspNetCore.Mvc;

namespace Kick_X.Controllers;

public class HomeController : Controller{
    public ActionResult Error404(){
        return View();
    }
}