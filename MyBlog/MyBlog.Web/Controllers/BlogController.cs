using Microsoft.AspNetCore.Mvc;
using MyBlog.Web.Models;

namespace MyBlog.Web.Controllers
{
	public class BlogController : Controller
	{
		BlogContext db = new BlogContext();
		public IActionResult Index()
		{
			var model = db.Articles.Where(c => c.Status).ToList();
			return View(model);
		}
	}
}
