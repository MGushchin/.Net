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

        public async Task<IActionResult> Show(int projectId)
        {
            Project? project = await db.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
            string? projectName = project?.Name; 
            return RedirectToAction("ShowInfo", "Project", project);
        }

        public IActionResult ShowInfo(Project project)
        {
            //Project? project = await db.Projects.FirstOrDefaultAsync(p => p.Name == projectName);
            return View(project);
        }

    }
}
