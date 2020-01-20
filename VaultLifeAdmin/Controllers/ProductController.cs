using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VaultLifeAdmin.Helpers;
using VaultLifeAdmin.Models;
using VaultLifeAdmin.ViewModels;
using VaultLifeAdmin.Service;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using ImageResizer.Resizing;
using ImageResizer;
namespace VaultLifeAdmin.Controllers
{
    
    public class ProductController : Controller
    {


        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();

        // GET: Product
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Contract).Include(p => p.Owner);
            return View(products.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            var model = new Product();
            model = (Product)Helpers.TableTracker.TrackCreate(model, "USR");

            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractCode");
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerCode");
            return View(model);
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductPrice, ProductSKUCode,OwnerID,SOH, AvailableSOH, IsExclusive, ContractID,ProductName,ProductDescription,DateInserted,DateUpdated,USR,Terms,link,URL")] Product product)
        {

            ProductValidator validator = new ProductValidator();

            if (ModelState.IsValid && validator.Validate(product).IsValid)
            {

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = product.ProductID });
            }

            ValidationResult results = validator.Validate(product);
            IList<ValidationFailure> failures = results.Errors;
            StringBuilder sb = new StringBuilder();
            foreach (var e in results.Errors)
            {
                ModelState.AddModelError(e.PropertyName, e.ErrorMessage);

            }

            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractCode", product.ContractID);
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerCode", product.OwnerID);
            return View(product);
        }

        [HttpPost]

        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files, int productId)
        {
            foreach (HttpPostedFileBase file in files)
            {
                saveFile(file, productId);
            }
            return Json("All files have been uccessfully stored.");
        }
private void saveFile(HttpPostedFileBase file, int productId)
        {Dictionary<Enums.ImageType, string> versions = new Dictionary<Enums.ImageType, string>();
            //Define the versions to generate
            versions.Add(Enums.ImageType.thumb, "width=75&height=75&crop=auto&format=jpg"); //Crop to square thumbnail
            versions.Add(Enums.ImageType.medium, "maxwidth=200&maxheight=200&format=jpg"); //Fit inside 400x400 area, jpeg
            versions.Add(Enums.ImageType.large, "maxwidth=540&maxheight=450&format=jpg"); //Fit inside 1900x1200 area
            foreach (string fileKey in System.Web.HttpContext.Current.Request.Files.Keys)
            {
                // HttpPostedFile file = HttpContext.Current.Request.Files[fileKey];
                if (file.ContentLength <= 0) continue; //Skip unused file controls.
                //To store the list of generated paths
                List<MemoryStream> generatedFiles = new List<MemoryStream>();
                //Generate each version
                foreach (Enums.ImageType suffix in versions.Keys)
                //Let the image builder add the correct extension based on the output file type
                {
                    Imagedetail newImage = new Imagedetail();
                    newImage.ImageName = file.FileName;
                    //Here's where the ContentType column comes in handy.  By saving
                    //  this to the database, it makes it infinitely easier to get it back
                    //  later when trying to show the image.
                    newImage.ContentType = file.ContentType;
                    if (suffix.Equals(Enums.ImageType.thumb))
                    {
                        newImage.ImageTypeID = 1;
                    }
                    else if (suffix.Equals(Enums.ImageType.medium))
                    {
                        newImage.ImageTypeID = 2;
                    }
                    else
                    {
                        newImage.ImageTypeID = 3;
                    }
                    var resultStream = new MemoryStream();
                    newImage.ImageName = suffix + newImage.ImageName;
                    ImageBuilder.Current.Build(file, resultStream,
                    new ResizeSettings(versions[suffix]), false, true);
                    MemoryStream imageInmemory = new MemoryStream();
                    Int64 length = resultStream.Length;
                    byte[] binaryData = resultStream.ToArray();
                    //string temp = Convert.ToBase64String(binaryData);
                    newImage.ImageContent = binaryData;
                    db.Imagedetails.Add(newImage);
                }
            }
            db.SaveChanges();

           
            return;
        }
        private byte[] ReadData(Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }
        
        public ActionResult SaveUploadedFile(int productid)//DropZoneTest
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                //Save file content goes here
                fName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {

                    var originalDirectory = new DirectoryInfo(string.Format("{0}Content", Server.MapPath(@"\")));

                    string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "ProductImages");

                    var fileName1 = Path.GetFileName(file.FileName);

                    bool isExists = System.IO.Directory.Exists(pathString);

                    if (!isExists)
                        System.IO.Directory.CreateDirectory(pathString);

                    var path = string.Format("{0}\\pid_{2}_{1}", pathString, file.FileName, productid.ToString());
                    file.SaveAs(path);

                }

            }

            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            AccountService service = new AccountService(db);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Where(x => x.ProductID == id).First();
            IEnumerable<ImageViewModel> images = db.Imagedetails.Where(d => d.ProductId == product.ProductID).Select(x => new ImageViewModel { Image = x });


            if (product == null)
            {
                return HttpNotFound();
            }

            ProductViewModel productViewModel = new ProductViewModel() { Product = product, Images = images };

            ViewBag.ContractIDList = new SelectList(db.Contracts, "ContractID", "ContractCode", product.ContractID);
            ViewBag.OwnerIDList = new SelectList(db.Owners, "OwnerID", "OwnerCode", product.OwnerID);
            return View(productViewModel);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProductID,ProductSKUCode,OwnerID,ContractID,ProductName,ProductDescription,DateInserted,DateUpdated,USR")] Product product)
        public ActionResult Edit(ProductViewModel productViewModel)
        {

            var product = productViewModel.Product;
            product = (Product)Helpers.TableTracker.TrackEdit(product, "USR");
            ProductValidator validator = new ProductValidator();

            if (ModelState.IsValid && validator.Validate(product).IsValid)
            {

                if (productViewModel.Images != null)
                {
                    foreach (var d in productViewModel.Images)
                    {

                        if (d.DeleteOnSave)
                        {
                            Imagedetail image = db.Imagedetails.Where(x => x.ImageID == d.Image.ImageID).First();
                            db.Imagedetails.Remove(image);

                        }
                        
                    }

                }

                db.Entry(productViewModel.Product).State = EntityState.Modified;

                db.SaveChanges();


                int count = 0;
                foreach (string fileName in Request.Files)
                {

                    HttpPostedFileBase file = Request.Files[count++];
                    saveFile(file, productViewModel.Product.ProductID);           

                }


                return RedirectToAction("Index");
            }

            ValidationResult results = validator.Validate(product);
            IList<ValidationFailure> failures = results.Errors;
            StringBuilder sb = new StringBuilder();
            foreach (var e in results.Errors)
            {

                ModelState.AddModelError(e.PropertyName + "Error", e.ErrorMessage);
                sb.AppendLine(e.ErrorMessage);

            }

            //ViewBag.ValidationErrors = sb.ToString();
            ViewBag.ContractIDList = new SelectList(db.Contracts, "ContractID", "ContractCode", productViewModel.Product.ContractID);
            ViewBag.OwnerIDList = new SelectList(db.Owners, "OwnerID", "OwnerCode", productViewModel.Product.OwnerID);
            return View(productViewModel);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //TODO fix delete images
 /*           var DisplayItems = db.DisplayItems.Where(x => x.ProductID == id).ToList();

            foreach (var d in DisplayItems)
            {
                DisplayItem displayItem = db.DisplayItems.Where(s => s.DisplayItemID == d.DisplayItemID).First();
                db.DisplayItems.Remove(displayItem);
                db.SaveChanges();
            }
            */
            var ProductInWatchLists = db.ProductInWatchLists.Where(x => x.ProductID == id).ToList();

            foreach (var d in ProductInWatchLists)
            {
                ProductInWatchList ProductInWatchList = db.ProductInWatchLists.Where(s => s.ProductInWatchListID == d.ProductInWatchListID).First();
                db.ProductInWatchLists.Remove(ProductInWatchList);
                db.SaveChanges();
            }

            var ProductInGames = db.ProductInGames.Where(x => x.ProductID == id).ToList();

            foreach (var d in ProductInGames)
            {

                var ProductPlayeds = db.ProductPlayeds.Where(x => x.ProductInGameID == d.ProductInGameID).ToList();

                foreach (var d1 in ProductPlayeds)
                {
                    ProductPlayed ProductPlayed = db.ProductPlayeds.Where(s => s.ProductInGameID == d1.ProductInGameID).First();
                    db.ProductPlayeds.Remove(ProductPlayed);
                    db.SaveChanges();
                }

                var ProductLocations = db.ProductLocations.Where(x => x.ProductInGameID == d.ProductInGameID).ToList();

                foreach (var d1 in ProductLocations)
                {
                    var SerialNumbers = db.SerialNumbers.Where(x => x.ProductLocationID == d1.ProductLocationID).ToList();
                    foreach (var s1 in SerialNumbers)
                    {
                        SerialNumber SerialNumber = db.SerialNumbers.Where(s => s.ProductLocationID == d1.ProductLocationID).First();
                        db.SerialNumbers.Remove(SerialNumber);
                        db.SaveChanges();
                    
                    }

                    var Vouchers = db.Vouchers.Where(x => x.ProductLocationID == d1.ProductLocationID).ToList();
                    foreach (var s1 in Vouchers)
                    {
                        Voucher Voucher = db.Vouchers.Where(s => s.ProductLocationID == d1.ProductLocationID).First();
                        db.Vouchers.Remove(Voucher);
                        db.SaveChanges();

                    }

                    ProductLocation ProductLocation = db.ProductLocations.Where(s => s.ProductInGameID == d1.ProductInGameID).First();
                    db.ProductLocations.Remove(ProductLocation);
                    db.SaveChanges();
                }


                ProductInGame ProductInGame = db.ProductInGames.Where(s => s.ProductInGameID == d.ProductInGameID).First();
                db.ProductInGames.Remove(ProductInGame);
                db.SaveChanges();
            }

            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult IndexFiltered(string ProductDescription, string ProductSKUCode)
        {
            List<Expression<Func<Product, bool>>> whereClause = new List<Expression<Func<Product, bool>>>();

            if (!String.IsNullOrEmpty(ProductDescription))
            {
                whereClause.Add(t => t.ProductDescription.Contains(ProductDescription));
            }

            if (!String.IsNullOrEmpty(ProductSKUCode))
            {
                whereClause.Add(t => t.ProductSKUCode.Contains(ProductSKUCode));
            }

            IQueryable<Product> products = SelectAllProductsWhere<Product>(whereClause);

            return View(products.ToList());
        }

        public IQueryable<Product> SelectAllProductsWhere<TKey>(List<Expression<Func<Product, bool>>> whereClause)
        {
            var selectAllQry = from p in db.Products.Include(p => p.Owner)
                               select p;


            if (whereClause != null)
            {
                foreach (Expression<Func<Product, bool>> whereStmt in whereClause)
                {
                    selectAllQry = selectAllQry.Where(whereStmt);
                }
            }

            return selectAllQry;
        }

        public ActionResult ViewProduct()
        {
            return View();
        }

        public static void ResizeLogo(string originalFilename, string resizeFilename)
        {
            Image imgOriginal = Image.FromFile(originalFilename);

            //pass in whatever value you want for the width (180)
            Image imgActual = ScaleBySize(imgOriginal, 180);
            imgActual.Save(resizeFilename);
            imgActual.Dispose();
        }

        public static Image ScaleBySize(Image imgPhoto, int size)
        {
            int logoSize = size;

            float sourceWidth = imgPhoto.Width;
            float sourceHeight = imgPhoto.Height;
            float destHeight = 0;
            float destWidth = 0;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            // Resize Image to have the height = logoSize/2 or width = logoSize.
            // Height is greater than width, set Height = logoSize and resize width accordingly
            if (sourceWidth > (2 * sourceHeight))
            {
                destWidth = logoSize;
                destHeight = (float)(sourceHeight * logoSize / sourceWidth);
            }
            else
            {
                int h = logoSize / 2;
                destHeight = h;
                destWidth = (float)(sourceWidth * h / sourceHeight);
            }
            // Width is greater than height, set Width = logoSize and resize height accordingly

            Bitmap bmPhoto = new Bitmap((int)destWidth, (int)destHeight,
                                        PixelFormat.Format32bppPArgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, (int)destWidth, (int)destHeight),
                new Rectangle(sourceX, sourceY, (int)sourceWidth, (int)sourceHeight),
                    GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }
    }
}
