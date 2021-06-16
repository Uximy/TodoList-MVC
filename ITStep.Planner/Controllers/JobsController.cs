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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITStep.Planner.Controllers
{
    public class JobsController : Controller
    {
        private readonly PlannerContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public JobsController(PlannerContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var job = new Job();
            var users = await _userManager.Users.ToListAsync();
            ViewBag.Users = users;
            return View(job);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Job job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
}
}
