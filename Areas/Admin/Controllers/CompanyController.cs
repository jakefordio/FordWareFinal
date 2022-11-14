using FordWare.Data;
using FordWare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FordWare.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CompanyController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Company> companies = await _dbContext.Companies.ToListAsync();
            return View(companies);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Company company)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.Companies.AddAsync(company);
                await _dbContext.SaveChangesAsync();
                TempData["success"] = "Company created successfully.";
                return RedirectToAction("Index");
            }
            return View(company);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Company? dbCompany = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id);
            if (dbCompany == null)
            {
                return NotFound();
            }
            return View(dbCompany);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Companies.Update(company);
                await _dbContext.SaveChangesAsync();
                TempData["success"] = "Company updated successfully.";
                return RedirectToAction("Index");
            }
            return View(company);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Company? dbCompany = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id);
            if (dbCompany == null)
            {
                return NotFound();
            }
            return View(dbCompany);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            Company? dbCompany = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id);
            if (dbCompany == null)
            {
                return NotFound();
            }

            _dbContext.Companies.Remove(dbCompany);
            await _dbContext.SaveChangesAsync();
            TempData["success"] = "Company deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
