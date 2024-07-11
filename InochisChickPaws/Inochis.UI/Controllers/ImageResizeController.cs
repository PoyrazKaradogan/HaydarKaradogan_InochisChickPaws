using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Inochis.UI.Controllers
{
    public class ImageResizeController : Controller
    {
       
        private readonly IHostingEnvironment _hostingEnvironment;

        public ImageResizeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult ImageResize()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ImageResize(IFormFile file)
        {
            int width = 400;
            int height = 400;
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "images", "resized.jpg");
            Image image = Image.FromStream(file.OpenReadStream(), true, true);
            var newImage = new Bitmap(width, height);
            using (var a = Graphics.FromImage(newImage))
            {
                a.DrawImage(image, 0, 0, width, height);
                newImage.Save(path);
            }


                return View();
        }
    }
}
