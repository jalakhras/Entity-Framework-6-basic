using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using NijaDomain.Classes;
using NinjaDomain.DataModel;
using ActionNameAttribute = System.Web.Http.ActionNameAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace MVCAppRepository.Controllers
{
    public class NinjasController : Controller
    {
        private readonly DisconnectedRepository _repo = new DisconnectedRepository();

        public ActionResult Index()
        {
            var ninjas = _repo.GetNinjasWithClan();
            return View(ninjas);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ninja = _repo.GetNinjaWithEquipmentAndClan(id.Value);
            if (ninja == null)
            {
                return HttpNotFound();
            }
            return View(ninja);
        }

        public ActionResult Create()
        {
            ViewBag.ClanId = new SelectList(_repo.GetClanList(), "Id", "ClanName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
          [Bind(Include = "Id,Name,ServedInOniwaban,ClanId,DateOfBirth,DateCreated,DateModified")] Ninja ninja)
        {
            if (ModelState.IsValid)
            {
                _repo.SaveNewNinja(ninja);
                return RedirectToAction("Index");
            }

            ViewBag.ClanId = new SelectList(_repo.GetClanList(), "Id", "ClanName", ninja.ClanId);
            return View(ninja);
        }

        // GET: Ninjas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ninja = _repo.GetNinjaWithEquipment(id.Value);
            if (ninja == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClanId = new SelectList(_repo.GetClanList(), "Id", "ClanName", ninja.ClanId);

            return View(ninja);
        }

        // POST: Ninjas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
          [Bind(Include = "Id,Name,ServedInOniwaban,ClanId,DateOfBirth,DateCreated,DateModified")] Ninja ninja)
        {
            if (ModelState.IsValid)
            {
                _repo.SaveUpdatedNinja(ninja);
                return RedirectToAction("Index");
            }
            ViewBag.ClanId = new SelectList(_repo.GetClanList(), "Id", "ClanName", ninja.ClanId);
            return View(ninja);
        }

        // GET: Ninjas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ninja = _repo.GetNinjaById(id.Value);
            if (ninja == null)
            {
                return HttpNotFound();
            }
            return View(ninja);
        }

        // POST: Ninjas/Delete/5
        [System.Web.Http.HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.DeleteNinja(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    _repo.Dispose();
            //}
            //base.Dispose(disposing);
        }
    }
}