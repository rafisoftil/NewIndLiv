using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace IndiaLivings_Web_UI.Controllers
{
    public class BlogController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var helper = new AuthenticationHelper();
            var list = helper.GetAllBlogs(1, 50, 0, false); // adjust paging as needed
            return View(list);
        }

        public async Task<IActionResult> Details(int blogId)
        {
            var helper = new AuthenticationHelper();
            var blog = helper.GetBlogById(blogId);
            return View(blog);
        }

        // Optional: Create action placeholder
        public IActionResult Create()
        {
            return View();
        }
    }
}