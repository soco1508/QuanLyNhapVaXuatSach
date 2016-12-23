using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EF;

namespace PROJECT2.Controllers
{
    public class ChiTietSachDaiLyLaysController : Controller
    {
        private MuaBanSachEntities1 db = new MuaBanSachEntities1();

        // GET: ChiTietSachDaiLyLays
        public ActionResult Index()
        {
            var chiTietSachDaiLyLays = db.ChiTietSachDaiLyLays.Include(c => c.Sach).Include(c => c.DaiLy);
            return View(chiTietSachDaiLyLays.ToList());
        }

        // GET: ChiTietSachDaiLyLays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietSachDaiLyLay chiTietSachDaiLyLay = db.ChiTietSachDaiLyLays.Find(id);
            if (chiTietSachDaiLyLay == null)
            {
                return HttpNotFound();
            }
            return View(chiTietSachDaiLyLay);
        }

        // GET: ChiTietSachDaiLyLays/Create
        public ActionResult Create()
        {
            ViewBag.idSach = new SelectList(db.Saches, "idSach", "tenSach");
            ViewBag.idDL = new SelectList(db.DaiLies, "idDL", "tenDaiLy");
            return View();
        }

        // POST: ChiTietSachDaiLyLays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idDL,idSach,ngayLay,soLuongXuat")] ChiTietSachDaiLyLay chiTietSachDaiLyLay)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietSachDaiLyLays.Add(chiTietSachDaiLyLay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idSach = new SelectList(db.Saches, "idSach", "tenSach", chiTietSachDaiLyLay.idSach);
            ViewBag.idDL = new SelectList(db.DaiLies, "idDL", "tenDaiLy", chiTietSachDaiLyLay.idDL);
            return View(chiTietSachDaiLyLay);
        }

        // GET: ChiTietSachDaiLyLays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietSachDaiLyLay chiTietSachDaiLyLay = db.ChiTietSachDaiLyLays.Find(id);
            if (chiTietSachDaiLyLay == null)
            {
                return HttpNotFound();
            }
            ViewBag.idSach = new SelectList(db.Saches, "idSach", "tenSach", chiTietSachDaiLyLay.idSach);
            ViewBag.idDL = new SelectList(db.DaiLies, "idDL", "tenDaiLy", chiTietSachDaiLyLay.idDL);
            return View(chiTietSachDaiLyLay);
        }

        // POST: ChiTietSachDaiLyLays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idDL,idSach,ngayLay,soLuongXuat")] ChiTietSachDaiLyLay chiTietSachDaiLyLay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietSachDaiLyLay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idSach = new SelectList(db.Saches, "idSach", "tenSach", chiTietSachDaiLyLay.idSach);
            ViewBag.idDL = new SelectList(db.DaiLies, "idDL", "tenDaiLy", chiTietSachDaiLyLay.idDL);
            return View(chiTietSachDaiLyLay);
        }

        // GET: ChiTietSachDaiLyLays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietSachDaiLyLay chiTietSachDaiLyLay = db.ChiTietSachDaiLyLays.Find(id);
            if (chiTietSachDaiLyLay == null)
            {
                return HttpNotFound();
            }
            return View(chiTietSachDaiLyLay);
        }

        // POST: ChiTietSachDaiLyLays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietSachDaiLyLay chiTietSachDaiLyLay = db.ChiTietSachDaiLyLays.Find(id);
            db.ChiTietSachDaiLyLays.Remove(chiTietSachDaiLyLay);
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
    }
}
