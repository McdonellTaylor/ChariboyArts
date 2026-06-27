using System;
using System.Linq;
using System.Web;
using System.IO;
using CHARIBOY_ARTS.Areas.Admin.Models;
using CHARIBOY_ARTS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CHARIBOY_ARTS.Utilities;


namespace CHARIBOY_ARTS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_User_Admin)]
    public class VideoController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public VideoController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult CreateVideo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateVideo(IFormFile videoFile, string title, string description)
        {
            if (videoFile != null && videoFile.Length > 0)
            {
                var fileName = Path.GetFileName(videoFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Videos", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await videoFile.CopyToAsync(stream);
                }

                var video = new Videos
                {
                    Title = title,
                    Description = description,
                    FileName = fileName,
                    FilePath = "/Videos/" + fileName
                };

                _applicationDbContext.Videos.Add(video);
                await _applicationDbContext.SaveChangesAsync();

                return RedirectToAction("Videos");
            }

            return View();
        }

        public IActionResult Videos()
        {
            var videos = _applicationDbContext.Videos.ToList();
            return View(videos);
        }
        
        //public IActionResult EditVideo(int? id)
        //{
        //    var video = _applicationDbContext.Videos.FirstOrDefault(x => x.Id == id);
        //    if (video == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(video);
        //}

        //[HttpPost]
        //public async Task<IActionResult> EditVideo(int id, IFormFile videoFile, string title, string description)
        //{
        //    var video = _applicationDbContext.Videos.FirstOrDefault(X => X.Id == id);
        //    if (video == null)
        //    {
        //        return NotFound();
        //    }

        //    if(videoFile != null && videoFile.Length > 0)
        //    {
        //        var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", video.FilePath.TrimStart('/'));
        //        if (System.IO.File.Exists(oldFilePath)) 
        //        {
        //            System.IO.File.Delete(oldFilePath);
        //        }

        //        var newFileName = Path.GetFileName(video.FilePath);
        //        var newFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Videos", newFileName);

        //        using(var stream = new FileStream(newFilePath, FileMode.Create))
        //        {
        //            await videoFile.CopyToAsync(stream);
        //        }

        //        video.FilePath = "/Videos/" + newFileName;
        //        video.FileName = newFileName;
        //    }

        //    video.Title = title;
        //    video.Description = description;

        //    _applicationDbContext.Videos.Update(video);

        //    await _applicationDbContext.SaveChangesAsync();

        //    return RedirectToAction("Videos");
        //}
        
        public IActionResult DeleteVideo(int? id)
        {
            var video = _applicationDbContext.Videos.FirstOrDefault(x=> x.Id == id);
            if(video == null)
            {
                return NotFound();
            }
            return View(video);
        }

        [HttpPost, ActionName("DeleteVideo")]
        public IActionResult DeletePost(int? id)
        {
            var video = _applicationDbContext.Videos.FirstOrDefault(x => x.Id == id);
            if (video == null)
            {
                return NotFound();
            }
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", video.FilePath.TrimStart('/'));

            if (System.IO.File.Exists(filePath)) 
            {
                System.IO.File.Delete(filePath);
            }

            _applicationDbContext.Videos.Remove(video);
            _applicationDbContext.SaveChanges();

            return RedirectToAction("Videos");
            
        }
    }
}
