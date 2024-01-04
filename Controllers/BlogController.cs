using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly BlogDataContext _db;

        public BlogController(BlogDataContext db)
        {
            _db = db;
        }

        [Route("")]
        public IActionResult Index(int page = 1)
        {
            const int pageSize = 2;

            var totalPosts = _db.Posts.Count();
            var totalPages = (int)Math.Ceiling((double)totalPosts / pageSize);

            var previousPage = page - 1;
            var nextPage = page + 1;

            ViewBag.CurrentPage = page;
            ViewBag.PreviousPage = previousPage;
            ViewBag.HasPreviousPage = previousPage >= 1;
            ViewBag.NextPage = nextPage;
            ViewBag.HasNextPage = nextPage <= totalPages;

            var posts = _db.Posts
                .OrderByDescending(x => x.Posted)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToArray();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView(posts);

            return View(posts);
        }



        [Route("{year:int}/{month:int}/{key}")]
        public IActionResult Post(int year, int month,  string key) {

            var post = _db.Posts.FirstOrDefault(x => x.Key == key);

           
            return View(post);
        }

        [Authorize]
        [Route("create")]
        [HttpGet]
        public IActionResult Create() 
        {
        
            return View();
        }

        [Authorize]
        [HttpPost]
        [Route("create")]

        public IActionResult Create(Post post)
        {

            if(ModelState.IsValid) 
            {
                return View(post);
            }

            post.Author = "";
            post.Posted = DateTime.Now;

            _db.Posts.Add(post);
            _db.SaveChanges();

            return RedirectToAction("Post", "Blog", new
            {
                year = post.Posted.Year, month = post.Posted.Month, key = post.Key
            });
        }



    }
}
