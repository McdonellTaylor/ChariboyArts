using CHARIBOY_ARTS.Data;
using CHARIBOY_ARTS.Models;
using CHARIBOY_ARTS.Areas.Admin.Models;
using CHARIBOY_ARTS.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CHARIBOY_ARTS.ViewModels;

namespace CHARIBOY_ARTS.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnityOfWork _unityOfWork;

        public HomeController(ILogger<HomeController> logger, IUnityOfWork unityOfWork,
            ApplicationDbContext dbContext, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _unityOfWork = unityOfWork;
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
          
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productsList = _unityOfWork.product.GetAll();
            return View(productsList);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var product = _dbContext.Products.Include(p => p.Comments).FirstOrDefault(p => p.Id == id);
            var user = await _userManager.GetUserAsync(User);
            if (product == null)
            {
                return NotFound();
            }

            ProductVM productVM = new()
            {
                Product = product,
                Comment = product.Comments.ToList(),
                UserName = user?.UserName,
            };
        
            return View(productVM);
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(int productId, string commentText)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Error"] = "You must be logged in to post a comment";

                RedirectToPage("/Identity/Account/Login");
            }

            var user = await _userManager.GetUserAsync(User);

          

            if (!string.IsNullOrEmpty(commentText))
            {
                var newComment = new Comment
                {
                    Text = commentText,
                    CreatedAt = DateTime.Now,
                    ProductId = productId,
                  
                };

                _dbContext.comments.Add(newComment);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Details), new {id = productId});
        }

        [HttpPost]
        public async Task<IActionResult> CommmentReplies(int commentId, int productId ,string text)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);

                var comment = await _dbContext.comments.Include(c => c.Product).FirstOrDefaultAsync(c => c.Id == commentId
                && c.ProductId == productId);

                if (comment != null) {
                    var reply = new Comment
                    {
                        Text = text,
                        CreatedAt = DateTime.Now,
                        ParentCommentId = commentId,
                        ProductId = productId
                    };
                    _dbContext.comments.Add(reply);
                }

              
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Details", new { id = productId });
            }
            TempData["Error"] = "You must be logged in to reply a comment";
            return RedirectToPage("/Identity/Account/Login");
        }

        public IActionResult ShortReels()
        {
            var videos = _dbContext.Videos.ToList();
            return View(videos);
        }

        public IActionResult About()
        {
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
