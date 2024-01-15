using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBlog.Web.Models;

namespace MyBlog.Web.Areas.Management.Controllers {
	[Area("Management")]
	public class AssignmentController : Controller {
		BlogContext db = new BlogContext();
		public IActionResult Index() {
			var model = db.Assignments
				.OrderBy(x => x.Deadline).ToList();
			ViewBag.AssignmentCount = model.Count();
			return View(model);
		}

		public IActionResult Details(int id) {
			var model = db.Assignments.Include("Project")
				.FirstOrDefault(x => x.Id == id);
			if (model == null) {
				return Redirect("/Management/Assignment/Index");
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult Create() {
			ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", null);
			return View();
		}

		[HttpPost]
		public IActionResult Create(Assignment model) {
			if (ModelState.IsValid) {
				model.Status = true;
				model.CreatedDate = DateTime.Now;
				db.Assignments.Add(model);
				db.SaveChanges();
				return Redirect("/Management/Assignment/Index");
			}
			ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name"
				, model.ProjectId);
			return View(model);
		}
	}
}
