using System;
using System.Linq;
using System.Threading.Tasks;
using ITStep.Planner.Contexts;
using ITStep.Planner.Models;
using ITStep.Planner.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITStep.Planner.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly PlannerContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectsController(PlannerContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Jobs(string projectId)
        {
            var project = await _context.Projects
                .Include(x => x.Jobs)
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(projectId));
            var users = await _userManager.Users.ToListAsync();
            ViewBag.Users = users;
            return View(project);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var project = new Project();
            var users = await _userManager.Users.ToListAsync();
            ViewBag.Users = users;
            return View(project);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var project = await _context.Projects.FindAsync(id);
            //var project = await _context.FindByIdAsync(id);
            if (project is null) return NotFound();
            var model = new EditProjectRequest { Id = project.Id, Title = project.Title, Description = project.Description };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProjectRequest model)
        {
            if (ModelState.IsValid)
            {
                var project = await _context.FindAsync<Project>(model.Id.ToString());
                if (project is not null)
                {
                    project.Title = model.Title;
                    project.Description = model.Description;
                    _context.Projects.Update(project);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project is not null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }


    }
}