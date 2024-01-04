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
        public IActionResult Index()
        {
            var posts = _db.Posts.OrderByDescending(x => x.Posted).Take(5).ToArray();
            return View(posts);
        }


        [Route("{year:int}/{month:int}/{key}")]
        public IActionResult Post(int year, int month,  string key) {

            var post = _db.Posts.FirstOrDefault(x => x.Key == key);

           
            return View(post);
        }

        [Route("create")]
        [HttpGet]
        public IActionResult Create() 
        {
        
            return View();
        }

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
