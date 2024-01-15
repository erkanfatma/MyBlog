using FE.Helper.Utils;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Web.Models; 

namespace MyBlog.Web.Areas.Management.Controllers;

[Area("Management")]
public class SkillController : Controller
{
	BlogContext db = new BlogContext();
	public IActionResult Index()
	{
		IEnumerable<Skill> model = db.Skills.Where(c => c.Status == true).ToList();
		return View(model);
	}

	public IActionResult Details(int id)
	{
		//Skill model = db.Skills.FirstOrDefault(c => c.Id == id);
		Skill? model = db.Skills.Find(id);
		if (model == null)
		{
			return Redirect("/Management/Skill/Index");
		}
		return View(model);
	}

	public IActionResult Create()
	{
		return View();
	}

	[HttpPost]
	public IActionResult Create(Skill model)
	{
		if (ModelState.IsValid)
		{
			model.Status = true;
			db.Skills.Add(model);
			db.SaveChanges();

			////Mail gönder
			//var mailList = new List<string>() {
			//	"erkann.fatma@gmail.com" 
			//};
			//MailSender.Send(mailList, "Yetenek Eklendi",
			//	$"Blog sitenize yeni bir yetenek eklendi </br> {model.Title}"); 
			return Redirect("/Management/Skill/Index");
		}
		return View(model);
	}

	[HttpGet]
	public IActionResult Edit(int id)
	{
		Skill? model = db.Skills.Find(id);
		if (model == null)
		{
			return Redirect("/Management/Skill/Index");
		}
		return View(model);
	}

	[HttpPost]
	public IActionResult Edit(Skill model) {
		if (ModelState.IsValid) {
			Skill? editModel = db.Skills.Find(model.Id);
			if (editModel == null)
			{
				return Redirect("/Management/Skill/Index");
			}

			editModel.Title = model.Title;
			editModel.Score = model.Score;
			editModel.Description = model.Description;
			db.SaveChanges();
			return Redirect("/Management/Skill/Index");

		}
		return View(model);
	}

	public IActionResult Delete(int id)
	{
		Skill? model = db.Skills.Find(id);
		if (model == null)
		{
			return Redirect("/Management/Skill/Index");
		}
		return View(model);
	}

	[HttpPost, ActionName("Delete")]
	public IActionResult DeleteConfirmed(int id)
	{
		Skill? model = db.Skills.Find(id);
		if (model == null)
		{
			return Redirect("/Management/Skill/Index");
		}
		//soft delete (yazılımda silindi olarak göstermek) için
		model.Status = false;
		db.SaveChanges();

		//dbden tamamen silmek için
		//db.Skills.Remove(model);
		//db.SaveChanges();

		return Redirect("/Management/Skill/Index");
	}

	[Route("/Management/Skill/DeleteWithJs/{id}")]
	[HttpPost]
	public JsonResult DeleteWithJs(int id) {
        Skill? model = db.Skills.Find(id);
        if (model == null) {
			return Json("Böyle bir şey bulunamadı");
        }
        model.Status = false;
        db.SaveChanges();
        return Json("Silindi");
	}
}
