using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaultlife.Models;
using System.IO;

namespace Vaultlife.Controllers
{
    public class ImageController : Controller
    {
        // GET: Imag
        [HttpGet]
        public ActionResult Index(int imageId)
        {
            VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();
            //This is my method for getting the image information
            // including the image byte array from the image column in
            // a database.
            Imagedetail image = db.Imagedetails.Where(x => x.ImageID == imageId).First();
            //As you can see the use is stupid simple.  Just get the image bytes and the
            //  saved content type.  See this is where the contentType comes in real handy.
            ImageResult result = new ImageResult(image.ImageContent, image.ContentType);

            return result;
        }
    }

    public class ImageResult : ActionResult
    {
        public String ContentType { get; set; }
        public byte[] ImageBytes { get; set; }
        public String SourceFilename { get; set; }

        //This is used for times where you have a physical location
        public ImageResult(String sourceFilename, String contentType)
        {
            SourceFilename = sourceFilename;
            ContentType = contentType;
        }

        //This is used for when you have the actual image in byte form
        //  which is more important for this post.
        public ImageResult(byte[] sourceStream, String contentType)
        {
            ImageBytes = sourceStream;
            ContentType = contentType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.Clear();
            response.Cache.SetCacheability(HttpCacheability.NoCache);
            response.ContentType = ContentType;

            //Check to see if this is done from bytes or physical location
            //  If you're really paranoid you could set a true/false flag in
            //  the constructor.
            if (ImageBytes != null)
            {
                var stream = new MemoryStream(ImageBytes);
                stream.WriteTo(response.OutputStream);
                stream.Dispose();
            }
            else
            {
                response.TransmitFile(SourceFilename);
            }
        }
    }
}