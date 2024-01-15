using Microsoft.AspNetCore.Mvc;
using MyBlog.Web.Models;

namespace MyBlog.Web.Areas.Management.Controllers;

[Area("Management")]
public class ContactMessageController : Controller
{
    BlogContext db = new BlogContext();
    public IActionResult Index()
    {
        IEnumerable<ContactMessage> model = db.ContactMessages
            .Where(c => c.Status == true)
            .OrderByDescending(o => o.CreatedDate)
            .ToList();

        return View(model);
    }
 

}
