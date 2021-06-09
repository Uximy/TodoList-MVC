using System;
using System.Linq;
using System.Threading.Tasks;
using ITStep.Planner.Contexts;
using ITStep.Planner.Models.Entities;
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var project = new Project();
            var users = await _userManager.Users.ToListAsync();
            ViewBag.Users = users;
            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}