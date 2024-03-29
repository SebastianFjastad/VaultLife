﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vaultlife.Models;
using Vaultlife.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Data.Entity.Validation;
using FluentValidation.Results;
using System.Text;

namespace Vaultlife.Controllers
{
    public class AccountViewController : Controller
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();

        // GET: Members
        public ActionResult Index()
        {

            var members = db.Members.Include(m => m.MemberSubscriptionType).Include(m => m.Country).Include(m => m.CountryState);

            
           

            return View(members.ToList());
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //AccountViewModel adbAccountViewModel = new AccountViewModel();
           
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName");
            ViewBag.StateID = new SelectList(db.CountryStates, "StateID", "StateName");
            /*  ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "AddressType");*/
            ViewBag.MemberSubscriptionTypeID = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeCode");

            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ASPUserId,MemberID,MemberSubscriptionTypeID,FirstName, LastName,IdentityType,EmailAddress,TelephoneHome,TelephoneOffice,TelephoneMobile,CountryID,StateID,Gender,Ethnicity,DateOfBirth,ActiveIndicator,RenewalDate,AddressID,DateInserted,DateUpdated,USR")] Member member)
        {

            MemberValidator validator = new MemberValidator();

            if (ModelState.IsValid && validator.Validate(member).IsValid)
            {

                member.AddressID = -1;
                member.DateInserted = DateTime.Today;
                member.DateUpdated = DateTime.Today;
                member.ASPUserId = User.Identity.GetUserId();
                member.USR = User.Identity.GetUserName();
                member.ActiveIndicator = true;
                db.Members.Add(member);
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }

                }

                return RedirectToAction("Index", "Home");
            }
            ValidationResult results = validator.Validate(member);
            IList<ValidationFailure> failures = results.Errors;
            StringBuilder sb = new StringBuilder();
            foreach (var e in results.Errors)
            {
                ModelState.AddModelError(e.PropertyName, e.ErrorMessage);
                ModelState.AddModelError(e.PropertyName + "Error", e.ErrorMessage);
                sb.AppendLine(e.ErrorMessage);

            }
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName", member.CountryID);
            ViewBag.StateID = new SelectList(db.CountryStates, "StateID", "StateName", member.StateID);
            ViewBag.MemberSubscriptionTypeID = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeCode", member.MemberSubscriptionTypeID);
            return View(member);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }

            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName", member.CountryID);
            ViewBag.StateID = new SelectList(db.CountryStates, "StateID", "StateName", member.StateID);
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "AddressType", member.AddressID);
            ViewBag.MemberSubscriptionTypeID = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeCode", member.MemberSubscriptionTypeID);
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberID,MemberSubscriptionTypeID,FirstName,LastName,IdentityType,EmailAddress,TelephoneHome,TelephoneOffice,TelephoneMobile,CountryID,StateID,Gender,Ethnicity,DateOfBirth,ActiveIndicator,RenewalDate,AddressID,DateInserted,DateUpdated,USR")] Member member)
        {
            MemberValidator validator = new MemberValidator();
            if (ModelState.IsValid && validator.Validate(member).IsValid)
            {
                db.Entry(member).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ValidationResult results = validator.Validate(member);
            IList<ValidationFailure> failures = results.Errors;
            StringBuilder sb = new StringBuilder();
            foreach (var e in results.Errors)
            {

                ModelState.AddModelError(e.PropertyName + "Error", e.ErrorMessage);
                sb.AppendLine(e.ErrorMessage);

            }

            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName", member.CountryID);
            ViewBag.StateID = new SelectList(db.CountryStates, "StateID", "StateName", member.StateID);
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "AddressType", member.AddressID);
            ViewBag.MemberSubscriptionTypeID = new SelectList(db.MemberSubscriptionTypes, "MemberSubscriptionTypeID", "MemberSubscriptionTypeCode", member.MemberSubscriptionTypeID);
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult CancelCreate()
        {

            AspNetUser user = db.AspNetUsers.Where(x => x.UserName == User.Identity.Name).First();
            db.AspNetUsers.Remove(user);
            db.SaveChanges();
            AuthenticationManager.SignOut();

            return RedirectToAction("Index", "Home");
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