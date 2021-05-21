using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CetToDoApp.Data;
using CetToDoApp.Models;

namespace CetToDoApp.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToDoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ToDo
        public async Task<IActionResult> Index(bool showall=false)
        {
            ViewBag.Showall = showall;

            var applicationDbContext = _context.ToDoItems.Include(t => t.Category).AsQueryable();
               
            if (!showall)
            {
                applicationDbContext = applicationDbContext.Where(t => !t.IsCompleted);
            }

            applicationDbContext = applicationDbContext.OrderBy(t=> t.DueDate);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ToDo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.ToDoItems
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }

        // GET: ToDo/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categorites, "Id", "Name");
            return View();
        }

        // POST: ToDo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,IsCompleted,DueDate,CategoryId")] ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toDoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categorites, "Id", "Name", toDoItem.CategoryId);
            return View(toDoItem);
        }

        // GET: ToDo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.ToDoItems.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categorites, "Id", "Name", toDoItem.CategoryId);
            return View(toDoItem);
        }

        // POST: ToDo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,IsCompleted,DueDate,CategoryId")] ToDoItem toDoItem)
        {
            if (id != toDoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoItemExists(toDoItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categorites, "Id", "Name", toDoItem.CategoryId);
            return View(toDoItem);
        }

        // GET: ToDo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.ToDoItems
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }

        // POST: ToDo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            _context.ToDoItems.Remove(toDoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


       

        private async Task<ActionResult> ChangeStatus(int id, bool status, bool showall)
        {
            var todoItemItem = _context.ToDoItems.FirstOrDefault(t => t.Id == id);
            if (todoItemItem == null)
            {
                return NotFound();
            }
            todoItemItem.IsCompleted = status;
            todoItemItem.CompletedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { showall= showall});
        }
    public async Task<ActionResult> MakeComplete (int id, bool showall)
        {
            return await ChangeStatus(id,true, showall);

        }
        public async Task<ActionResult> MakeInComplete(int id, bool showall)
        {

            return await ChangeStatus(id, false, showall);
        }

        private bool ToDoItemExists(int id)
        {
            return _context.ToDoItems.Any(e => e.Id == id);
        }
    }
}
