using Microsoft.AspNetCore.Mvc;
using SerilogAndAutofacAspNetCore6.Models;
using SerilogAndAutofacAspNetCore6.Services;
using System.Diagnostics;

namespace SerilogAndAutofacAspNetCore6.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly TestService _testService;

    public HomeController(ILogger<HomeController> logger, TestService testService)
    {
        _logger = logger;
        _testService = testService;
    }

    public IActionResult Index()
    {
        _logger.LogInformation(_testService.TestValue);
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
