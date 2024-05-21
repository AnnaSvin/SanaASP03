using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SanaSDB3;
using SanaSDB3.ViewModels;

namespace SanaSDB3.Controllers
{
    public class HomeController : Controller
    {
        private readonly TODOlistContext _context; 

        public HomeController(TODOlistContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var taskList = await _context.Tasks
                .Include(t => t.Category)
                .ToListAsync();

            // Assuming you have a list of categories in your database, retrieve them here
            var categories = await _context.Categories.ToListAsync();

            // Populate ViewBag.CategoryId with SelectList of categories
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            Tasks newTask = new Tasks();

            var viewModel = new TaskViewModel
            {
                TaskList = taskList,
                NewTask = new Tasks()


            };
   
            return View(viewModel); // Pass the ViewModel to the view

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTask(TaskViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(viewModel.NewTask.Name))
                {
                    ModelState.AddModelError("NewTask.Name", "The Name field is required.");
                    return RedirectToAction(nameof(Index)); // Redirect to the task list view
                }

                _context.Add(viewModel.NewTask);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index)); // Redirect to the task list view
            }

            // If model state is not valid, return back to the Index view with validation errors
            return View(nameof(Index), viewModel);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskComplete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            if (task.Completed == true)
            {
                task.Completed = false;
            }
            else
            {
                task.Completed = true;
            }

            try
            {
                _context.Update(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirect to the task list
            }
            catch (Exception ex)
            {
                // Handle the exception, log or display an error message
                ModelState.AddModelError("", "An error occurred while completing the task.");
                return View(task); // Return to the view with an error message
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name");
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Completed,Name,Priority,DueDate,CategoryId")] Tasks task)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(task.Name))
                {
                    ModelState.AddModelError("Name", "The Name field is required.");
                    ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", task.CategoryId);
                    return View(task);
                }

                _context.Add(task);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index)); // Redirect to the task list after successful creation
            }

            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", task.CategoryId);
            return View(task);
        }


        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", tasks.CategoryId);
            return View(tasks);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Completed,Name,DueDate,CategoryId")] Tasks tasks)
        {
            if (id != tasks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tasks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasksExists(tasks.Id))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", tasks.CategoryId);
            return View(tasks);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            try
            {
                _context.Remove(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirect to the task list
            }
            catch (Exception ex)
            {
                // Handle the exception, log or display an error message
                ModelState.AddModelError("", "An error occurred while completing the task.");
                return View(task); // Return to the view with an error message
            }
        }


        private bool TasksExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
