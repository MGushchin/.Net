﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Manager_Alpha.Models;

namespace Test_Manager_Alpha.Controllers
{
    public class HomeController: Controller
    {
        ApplicationContext db;

        public HomeController(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Projects.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(Project project)
        //{
        //    db.Projects.Add(project);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public async Task<IActionResult> Create(string name, string description)
        {
            Project project = new Project() { Name = name, Description = description, TestSuites = new List<TestSuite>()};
            db.Projects.Add(project);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult OpenProject(string? projectId)
        {
            return RedirectToAction("Show", "Project", new { projectId = projectId });
        }
    }
}