using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.IO;
using Vaultlife.Models;

namespace Vaultlife.Controllers
{
    public class LanguageItemsController : Controller
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();

        // GET: /LanguateItems/
        public ActionResult Index()
        {
            var languageitems = db.LanguageItems.Include(l => l.Language);
            ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "LanguageName");
            return View(languageitems.ToList());
        }

        // GET: /LanguateItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LanguageItem languageitem = db.LanguageItems.Find(id);
            if (languageitem == null)
            {
                return HttpNotFound();
            }
            return View(languageitem);
        }

        // GET: /LanguateItems/Create
        public ActionResult Create()
        {
            ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "LanguageName");
            LanguageItem model = new LanguageItem();
            model = (LanguageItem)Helpers.TableTracker.TrackCreate(model, User.Identity.Name);
            return View(model);
        }

        // POST: /LanguateItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="LanguageItemID,LanguageID,LanguageItemKey,LanguageItemValue")] LanguageItem languageitem)
        {
            if (ModelState.IsValid)
            {
                db.LanguageItems.Add(languageitem); 
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "LanguageName", languageitem.LanguageID);
            return View(languageitem);
        }

        // GET: /LanguateItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LanguageItem languageitem = db.LanguageItems.Find(id);
            if (languageitem == null)
            {
                return HttpNotFound();
            }
            ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "LanguageName", languageitem.LanguageID);
            return View(languageitem);
        }

        // POST: /LanguateItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit([Bind(Include="LanguageItemID,LanguageID,LanguageItemKey,LanguageItemValue")] LanguageItem languageitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(languageitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "LanguageName", languageitem.LanguageID);
            return View(languageitem);
        }

        // GET: /LanguateItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LanguageItem languageitem = db.LanguageItems.Find(id);
            if (languageitem == null)
            {
                return HttpNotFound();
            }
            return View(languageitem);
        }

        // POST: /LanguateItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LanguageItem languageitem = db.LanguageItems.Find(id);
            db.LanguageItems.Remove(languageitem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public FileStreamResult CreateResource(int? id)
        {
           // VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();
            XmlDocument doc = new XmlDocument();
             
            
            doc.Load(Server.MapPath("~/Content/ResourceTemplate/Resources.txt"));
            XmlElement root = doc.DocumentElement;

            XmlElement datum = null;
            XmlElement value = null;
            XmlAttribute datumName = null;
            XmlAttribute datumSpace = doc.CreateAttribute("xml:space");
            datumSpace.Value = "preserve";

            Dictionary<string, string> parsedData = new Dictionary<string, string>();

       
            

           
            var entries = from e in db.LanguageItems where e.LanguageID == id select e;

           


            foreach (var Item in entries)
            {
                string key = Convert.ToString(Item.LanguageItemKey);
                string ivalue = Convert.ToString(Item.LanguageItemValue);
                parsedData.Add(key, ivalue);
              
            };



         
           

            foreach (KeyValuePair<string, string> pair in parsedData)
            {
                datum = doc.CreateElement("data");
                datumName = doc.CreateAttribute("name");
                datumName.Value = pair.Key;
                value = doc.CreateElement("value");
                value.InnerText = pair.Value;

                datum.Attributes.Append(datumName);
                datum.Attributes.Append(datumSpace);
                datum.AppendChild(value);
                root.AppendChild(datum);
            }

                MemoryStream ms = new MemoryStream();
                using (XmlWriter writer = XmlWriter.Create(ms))
            {
                  doc.WriteTo(writer); // Write to memorystream
            }
                byte[] bytes = ms.ToArray();


                var stream = new MemoryStream(bytes);
                
                
                
                return File(stream, "text/XML", "your_file_nam.txt");

              
              
        }

      




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
