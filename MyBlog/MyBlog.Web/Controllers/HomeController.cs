using Microsoft.AspNetCore.Mvc;
using MyBlog.Web.Models;
using System.Diagnostics;

namespace MyBlog.Web.Controllers {
    public class HomeController : Controller {
        BlogContext db = new BlogContext();

        [HttpGet]
        public IActionResult Index() {
            MainPage model = db.MainPages.FirstOrDefault();
            return View(model);
        }
        [Route("iletisim")]
        public IActionResult Contact() {
            Contact model = db.Contacts.FirstOrDefault();
            return View(model);
        }

        [HttpGet]
        public IActionResult ContactUs() {
            return View();
        }

        [HttpPost]
        public IActionResult ContactUs(ContactMessage model) {
            if (ModelState.IsValid) {
                // baþarýlýysa kaydet
                model.Status = true;
                model.CreatedDate = DateTime.Now;
                model.FirstName = model.FirstName.ToUpper();
                model.LastName = model.LastName.ToUpper();

                db.ContactMessages.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // query string
        public IActionResult Skills(string? name, string? desc, byte? order = 0) {
            ViewBag.Search = "";
            ViewBag.Description = "";
            var model = db.Skills.ToList();
            if (name != null) {
                ViewBag.Search = name;
                model = model
                    .Where(c => c.Title.ToLower().Contains(name.ToLower())
                            || c.Description.ToLower().Contains(name.ToLower())
                            || c.Score.ToString().Contains(name.ToLower()))
                    .ToList();
            }
            if (desc != null) {
                ViewBag.Description = desc;
                model = model
                    .Where(c => c.Description.ToLower().Contains(desc.ToLower()))
                .ToList();
            }

            if (order == 0) {
                model = model.OrderBy(o => o.Title).ToList();
            }
            else if (order == 1) {
                model = model.OrderByDescending(o => o.Title).ToList();
            }
            else if (order == 2) {
                model = model.OrderBy(o => o.Score).ToList();
            }
            else {
                model = model.OrderByDescending(o => o.Score).ToList();
            }
            return View(model);
        }

        public IActionResult About() {
            About model = db.Abouts.FirstOrDefault();
            return View(model);
        }


       

        public IActionResult Privacy2() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
