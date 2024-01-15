using Microsoft.AspNetCore.Mvc;
using MyBlog.Web.Models;

namespace MyBlog.Web.Controllers;

public class SkillController : Controller
{
	BlogContext db = new BlogContext();
	public IActionResult Index()
	{
		IEnumerable<Skill> model = 
			db.Skills
			.Where(ahmet => ahmet.Status == true 
						&& ahmet.Title.Contains("s")
						&& ahmet.Score > 50
						//&& ahmet.Description != null
						//&& !string.IsNullOrEmpty(ahmet.Description)
			)
			.ToList();
		return View(model);
	}
}
