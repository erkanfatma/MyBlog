using Microsoft.AspNetCore.Mvc;
using MyBlog.Web.Models;

namespace MyBlog.Web.Areas.Management.Controllers;

[Area("Management")]
public class TagController : Controller {
    BlogContext db = new BlogContext();
    public IActionResult Index() {
        IEnumerable<Tag> model = db.Tags.Where(c => c.Status == true).ToList();
        return View(model);
    }

    public IActionResult Details(int id) {
        //Tag model = db.Tags.FirstOrDefault(c => c.Id == id);
        Tag? model = db.Tags.Find(id);
        if (model == null) {
            return Redirect("/Management/Tag/Index");
        }
        return View(model);
    }

    public IActionResult Create() {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Tag model) {
        if (ModelState.IsValid) {
            model.Status = true;
            model.CreatedDate = DateTime.Now;
            db.Tags.Add(model);
            db.SaveChanges();

          return Redirect("/Management/Tag/Index");
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Edit(int id) {
        Tag? model = db.Tags.Find(id);
        if (model == null) {
            return Redirect("/Management/Tag/Index");
        }
        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(Tag model) {
        if (ModelState.IsValid) {
            Tag? editModel = db.Tags.Find(model.Id);
            if (editModel == null) {
                return Redirect("/Management/Tag/Index");
            }

            editModel.Name = model.Name; 
            db.SaveChanges();
            return Redirect("/Management/Tag/Index");

        }
        return View(model);
    }

    public IActionResult Delete(int id) {
        Tag? model = db.Tags.Find(id);
        if (model == null) {
            return Redirect("/Management/Tag/Index");
        }
        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id) {
        Tag? model = db.Tags.Find(id);
        if (model == null) {
            return Redirect("/Management/Tag/Index");
        }
        //soft delete (yazılımda silindi olarak göstermek) için
        model.Status = false;
        db.SaveChanges();

        //dbden tamamen silmek için
        //db.Tags.Remove(model);
        //db.SaveChanges();

        return Redirect("/Management/Tag/Index");
    }
}
