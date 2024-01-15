using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBlog.Web.Models;

namespace MyBlog.Web.Areas.Management.Controllers;

[Area("Management")]
public class ArticleTagController : Controller {
	BlogContext db = new BlogContext();
	public IActionResult Index() {
		IEnumerable<ArticleTag> model = db.ArticleTags
			.Include("Article").Include("Tag").ToList();
		ViewBag.Counter = model.Count();
		return View(model);
	}

	public IActionResult Create() {
		ViewBag.ErrorMessage = "";
		ViewBag.ArticleId = new SelectList(db.Articles, "Id", "Title", null);
		ViewBag.TagId = new SelectList(db.Tags, "Id", "Name", null);
		return View();
	}

	[HttpPost]
	public IActionResult Create(ArticleTag model) {
		if (ModelState.IsValid) {
			var articleTag = db.ArticleTags.FirstOrDefault(t => t.ArticleId == model.ArticleId && t.TagId == model.TagId);
			if (articleTag != null) {
				ViewBag.ErrorMessage = "Böyle bir kayıt zaten var";
				return View(model);
			}
			db.ArticleTags.Add(model);
			db.SaveChanges();
			return Redirect("/Management/ArticleTag/Index");
		}
		ViewBag.ErrorMessage = "Lütfen değerleri kontrol edin.";
		ViewBag.ArticleId = new SelectList(db.Articles, "Id", "Title", model.ArticleId);
		ViewBag.TagId = new SelectList(db.Tags, "Id", "Name", model.TagId);
		return View(model);
	}

	[Route("/Management/ArticleTag/Delete/{articleId}/{tagId}")]
	public IActionResult Delete(int articleId, int tagId) {
		ArticleTag? model = db.ArticleTags
			.Include("Article").Include("Tag")
			.FirstOrDefault(x => x.ArticleId == articleId && x.TagId == tagId);
		if (model == null) {
			return Redirect("/Management/ArticleTag/Index");
		}
		return View(model);
	}

	[Route("/Management/ArticleTag/Delete/{articleId}/{tagId}")]
	[HttpPost, ActionName("Delete")]
	public IActionResult DeleteConfirmed(int articleId, int tagId) {
		ArticleTag? model = db.ArticleTags
			.Include("Article").Include("Tag")
		.FirstOrDefault(x => x.ArticleId == articleId && x.TagId == tagId);
		if (model == null) {
			return Redirect("/Management/ArticleTag/Index");
		}
		//hard delete
		db.ArticleTags.Remove(model);
		db.SaveChanges();

		return Redirect("/Management/ArticleTag/Index");
	}

    [Route("/Management/ArticleTag/DeleteByJs/{articleId}/{tagId}")]
    [HttpPost]
	public JsonResult DeleteByJs(int articleId, int tagId) {
        Console.WriteLine("Sil");
		return Json("Silindi");
    }
}
 