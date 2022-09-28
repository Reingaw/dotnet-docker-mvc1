using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mvc1.Models;

namespace mvc1.Controllers;

public class HomeController : Controller
{
    private IRepository repository;
    private string message;
    public HomeController(IRepository repo, IConfiguration config)
    {
        repository = repo;
        message = $"Docker - ({config["HOSTNAME"]})";
    }
    public IActionResult Index()
    {
        ViewBag.Message = message;
        return View(repository.Produtos);
    }
}
