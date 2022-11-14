using FordWare.Data;
using FordWare.Models;
using FordWare.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FordWare.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _hostEnv;

        public ProjectController(ApplicationDbContext dbContext, IWebHostEnvironment hostEnv)
        {
            _dbContext = dbContext;
            _hostEnv = hostEnv;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Project> projects = await _dbContext.Projects.Include(p => p.Company).Include(p => p.Category).ToListAsync();
            return View(projects);
        }

        public async Task<IActionResult> Create()
        {
            ProjectViewModel projectVM = new ProjectViewModel();
            projectVM.CategoryList = _dbContext.Categories.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            projectVM.CompanyList = _dbContext.Companies.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            return View(projectVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectViewModel projectVM, IFormFile projectImage)
        {
            if (ModelState.IsValid)
            {
                string wwwPath = _hostEnv.WebRootPath;
                if(projectImage != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwPath, @"images\projects");
                    var extension = Path.GetExtension(projectImage.FileName);

                    using(var fileStreams = new FileStream(Path.Combine(uploads, fileName+extension), FileMode.Create))
                    {
                        projectImage.CopyTo(fileStreams);
                    }
                    projectVM.Project.ImageURL = @"\images\projects\" + fileName + extension;
                }

                await _dbContext.Projects.AddAsync(projectVM.Project);
                await _dbContext.SaveChangesAsync();
                TempData["success"] = "Project created successfully.";
                return RedirectToAction("Index");
            }
            return View(projectVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            ProjectViewModel projectVM = new ProjectViewModel();
            projectVM.CategoryList = _dbContext.Categories.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            projectVM.CompanyList = _dbContext.Companies.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            projectVM.Project = await _dbContext.Projects.FirstOrDefaultAsync(c => c.Id == id);
            if (projectVM.Project == null)
            {
                return NotFound();
            }
            return View(projectVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProjectViewModel projectVM, IFormFile? projectImage)
        {
            if (ModelState.IsValid)
            {
                string wwwPath = _hostEnv.WebRootPath;
                if(projectImage != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwPath, @"images\projects");
                    var extension = Path.GetExtension(projectImage.FileName);

                    if(projectVM.Project.ImageURL != null)
                    {
                        var oldImagePath = Path.Combine(wwwPath, projectVM.Project.ImageURL.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        projectImage.CopyTo(fileStreams);
                    }
                    projectVM.Project.ImageURL = @"\images\projects\" + fileName + extension;
                }
                _dbContext.Projects.Update(projectVM.Project);
                await _dbContext.SaveChangesAsync();
                TempData["success"] = "Project updated successfully.";
                return RedirectToAction("Index");
            }
            return View(projectVM);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            Project? dbProject = await _dbContext.Projects.FirstOrDefaultAsync(c => c.Id == id);
            if (dbProject == null)
            {
                TempData["error"] = "Error deleting project";
                return Json(new { success = false, message = "Error while deleting" });
            }
            var oldImagePath = Path.Combine(_hostEnv.WebRootPath, dbProject.ImageURL.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _dbContext.Projects.Remove(dbProject);
            await _dbContext.SaveChangesAsync();
            TempData["success"] = "Project deleted successfully.";
            return Json(new { success = true, message = "Delete successful" });
        }
    }
}
