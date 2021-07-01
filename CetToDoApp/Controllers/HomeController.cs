using CetToDoApp.Data;
using CetToDoApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CetToDoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }

        public  async Task<IActionResult> Index(SearchViewModel searchModel)
        {
            var query = dbContext.ToDoItems
                .Include(t=> t.Category)
                .Where(t => t.IsCompleted == false).AsQueryable();

            if (!String.IsNullOrWhiteSpace(searchModel.SearchText))
            {
                query = query.Where(t => t.Title.Contains(searchModel.SearchText));
            }


            query = query.OrderBy(t => t.DueDate).Take(3);
            searchModel.Result = await query.ToListAsync();
            return View(searchModel);
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
}
