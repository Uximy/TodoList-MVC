using System;
using System.Collections.Generic;
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
            
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Ready(string projectId)
        {
          int coint = 0;
          var selectedStatus = (await _context.Jobs
                  .Include(x => x.JobStatus)
                  .Where(x => x.JobStatus.ToString() == "Готова")
                  .FirstOrDefaultAsync(x => x.Id == Guid.Parse(projectId))).ToString();
          foreach (int s in selectedStatus) coint++;
          return View(coint);
        }
       
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Back(string projectId)
        {
          int coint = 0;
          DateTime dt2 = DateTime.Now;
          var selectedBack = (await _context.Jobs
                    .Include(x => x.JobStatus)
                    .Where(x => x.Deadline <= dt2)
                    .FirstOrDefaultAsync(x => x.Id == Guid.Parse(projectId))).ToString();
          foreach (int s in selectedBack) coint++;
          return View(coint);
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