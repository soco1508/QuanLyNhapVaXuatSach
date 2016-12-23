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
    public class DaiLiesController : Controller
    {
        private MuaBanSachEntities1 db = new MuaBanSachEntities1();

        // GET: DaiLies
        public ActionResult Index()
        {
            ViewBag.daili = db.DaiLies.ToList();
            return View();
        }

        public JsonResult TakeData(string from, string to, string page)
        {
            DateTime tu = DateTime.Parse(from);
            DateTime den = DateTime.Parse(to);
            var query = (from c in db.ChiTietSachDaiLyLays
                         where c.ngayLay >= tu && c.ngayLay <= den 
                         select new
                         {
                             ts = c.Sach.tenSach,
                             ngaylay = c.ngayLay,
                             sl = c.soLuongXuat,
                         }).OrderBy(x => x.ngaylay);
            int rowperpage = 8;
            int offset = (Int16.Parse(page) - 1) * rowperpage;

            var data = query.Skip(offset).Take(rowperpage).ToList();
            var count = query.Count();
            var result = new
            {
                data = data,
                count = count
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: DaiLies/Details/5
        public ActionResult Details(int? id)
        {
            DaiLy dl = db.DaiLies.Where(x => x.idDL == id).FirstOrDefault();
            ViewBag.DaiLy = dl;
            return View();
        }

        // GET: DaiLies/Create
        public ActionResult Create()
        {            
            return View();
        }
      
        [HttpPost]
        public ActionResult Create2()
        {
            var ten = Request.Form["ten"];
            var diachi = Request.Form["diachi"];
            var sdt = Request.Form["sdt"];
            var tienno = "0";
            //
            DaiLy a = new DaiLy
            {
                tenDaiLy = ten,
                diaChi = diachi,
                sDT = sdt,
                tienDLNo = tienno
            };
            db.DaiLies.Add(a);
            db.SaveChanges();

            return Redirect("/DaiLies");
        }

        // GET: DaiLies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return Redirect("/DaiLies");
            }
            DaiLy daili = db.DaiLies.Where(x => x.idDL == id).FirstOrDefault<DaiLy>();
            ViewBag.daili = daili;
            if (daili == null)
            {
                return Redirect("/DaiLies");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit2()
        {
            var id = Int32.Parse(Request.Form["id"]);
            var ten = Request.Form["ten"];
            var diachi = Request.Form["diachi"];
            var sdt = Request.Form["sdt"];

            //
            DaiLy a = db.DaiLies.Where(x => x.idDL == id).FirstOrDefault<DaiLy>();
            if (a != null)
            {
                a.tenDaiLy = ten;
                a.diaChi = diachi;
                a.sDT = sdt;
                db.SaveChanges();
            }
            return Redirect("/DaiLies");
        }

        // GET: DaiLies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaiLy a = db.DaiLies.Where(x => x.idDL == id).FirstOrDefault<DaiLy>();
            if (a == null)
            {
                return HttpNotFound();
            }
            db.DaiLies.Remove(a);
            db.SaveChanges();
            return Redirect("/DaiLies");
        }
    }
}
