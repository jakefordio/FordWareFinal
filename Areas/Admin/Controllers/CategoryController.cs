using FordWare.Data;
using FordWare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FordWare.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categories = await _dbContext.Categories.ToListAsync();
            return View(categories);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The display order cannot match the name.");
            }
            if (ModelState.IsValid)
            {
                await _dbContext.Categories.AddAsync(category);
                await _dbContext.SaveChangesAsync();
                TempData["success"] = "Category created successfully.";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? dbCategory = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (dbCategory == null)
            {
                return NotFound();
            }
            return View(dbCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The display order cannot match the name.");
            }
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(category);
                await _dbContext.SaveChangesAsync();
                TempData["success"] = "Category updated successfully.";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? dbCategory = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (dbCategory == null)
            {
                return NotFound();
            }
            return View(dbCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            Category? dbCategory = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (dbCategory == null)
            {
                return NotFound();
            }

            _dbContext.Categories.Remove(dbCategory);
            await _dbContext.SaveChangesAsync();
            TempData["success"] = "Category deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
