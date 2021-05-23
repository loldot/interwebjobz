using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using interwebzjobs.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.IO.Compression;

namespace interwebzjobs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            if(file == null || file.Length <= 0) return BadRequest("No file uploaded");
            if(Path.GetExtension(file.FileName) != ".zip") return BadRequest("Only zip-files are allowed");

            using (var fs = file.OpenReadStream())
            using(var zip = new ZipArchive(fs))
            {
                zip.ExtractToDirectory(".");
            }

            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
