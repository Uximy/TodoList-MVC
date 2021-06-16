using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITStep.Planner.Contexts;
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
            var projects = new List<Project>();
            var project = new Project();
            project.Description = "Тут будет описание";
            project.Title = "NightSpace";
            project.Jobs = new List<Job>();
            project.Jobs.Add(new Job{JobTypeId = Guid.NewGuid(), ProjectId = Guid.NewGuid(),Title = "NightSpace", 
            Description = "Text432fjkddjkgjhjk", JobStatusId = Guid.NewGuid(), AuthorId = Guid.NewGuid(), JobStatus = new JobStatus{ Title = "В процессе"}});
            projects.Add(project);
            return View(projects);
        }
        
        [Authorize(Roles="admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var project = new Project();
            var users = await _userManager.Users.ToListAsync();
            ViewBag.Users = users;
            return View(project);
        }

        [Authorize(Roles="admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}