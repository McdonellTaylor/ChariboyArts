using CHARIBOY_ARTS.Data;
using CHARIBOY_ARTS.Models;
using CHARIBOY_ARTS.Repositories.IRepository;
using CHARIBOY_ARTS.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CHARIBOY_ARTS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_User_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _dbContext;
        public ProductController(IUnityOfWork unityOfWork, IWebHostEnvironment webHostEnvironment, ApplicationDbContext dbContext)
        {
            _unityOfWork = unityOfWork;
            _webHostEnvironment = webHostEnvironment;
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            List<Product> products = _unityOfWork.product.GetAll().ToList();
            return View(products);
        }

        // GET: Upsert (for displaying the form)
        public IActionResult Upsert(int? id)
        {
            Product model = new Product();
            if (id == null)
            {
                // This is for create
                return View(model);
            }
            else
            {
                // This is for edit
                model = _unityOfWork.product.Get(u => u.Id == id);
                return View(model);
            }
        }

        // POST: Upsert (for handling form submission)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product model, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"Images\Product");
                    string VideoProductPath = Path.Combine(wwwRootPath, @"Images\Videos");

                    if (!string.IsNullOrEmpty(model.ImageUrl))
                    {
                        //delete old image
                        var oldImagePath = Path.Combine(wwwRootPath, model.ImageUrl.TrimStart('\\'));
                        //var oldVideoPath = Path.Combine(wwwRootPath, model.VideoUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                       // if (System.IO.File.Exists(oldVideoPath))
                        {
                        //    System.IO.File.Delete(oldVideoPath);
                        }
                    }
                    
                    using(var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    using (var fileStream = new FileStream(Path.Combine(VideoProductPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    model.ImageUrl = @"Images\Product\" + fileName;
                   // model.VideoUrl = @"Images\Videos\" + fileName;
                }
                if (model.Id == 0)
                {
                    // Create
                    _unityOfWork.product.Add(model);
                    TempData["success"] = "Category created successiful";
                }
                else
                {
                    // Update
                    _unityOfWork.product.Update(model);
                    TempData["success"] = "Category updated successiful";
                }
                _unityOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id== null)
            {
                return NotFound();
            }
            Product productToBeDeleted = _unityOfWork.product.Get(x => x.Id == id);
            if (productToBeDeleted == null)
            {
                return NotFound();
            }
            return View(productToBeDeleted);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product obj = _unityOfWork.product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unityOfWork.product.Remove(obj);
            _unityOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Product? productFromDb = _dbContext.Products.Find(id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
    }
}

