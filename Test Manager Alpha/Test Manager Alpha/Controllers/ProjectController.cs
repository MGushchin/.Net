using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Manager_Alpha.Models;

namespace Test_Manager_Alpha.Controllers
{
    public class ProjectController : Controller
    {
        ApplicationContext db;

        public ProjectController(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IActionResult> ShowInfo(string projectName)
        {
            Project? project = await db.Projects.FirstOrDefaultAsync(p => p.Name == projectName);
            return View(project);
            //return RedirectToAction("ShowInfo", "Project", project);
        }

        //public async Task<IActionResult> ShowInfo(int projectId)
        //{
        //    Project? project = await db.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
        //    return View(project);
        //}

    }
}
