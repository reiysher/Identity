namespace Identity.Host.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
