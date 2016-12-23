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
    public class NhaXuatBansController : Controller
    {
        private MuaBanSachEntities1 db = new MuaBanSachEntities1();

        // GET: NhaXuatBans
        public ActionResult Index()
        {
            ViewBag.nxb = db.NhaXuatBans.ToList();
            return View();
        }

        // GET: NhaXuatBans/Details/5
        public ActionResult Details(int? id)
        {
            NhaXuatBan nxb = db.NhaXuatBans.Where(x => x.idNXB == id).FirstOrDefault();
            ViewBag.NXB = nxb;
            return View();
        }

       
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
            NhaXuatBan a = new NhaXuatBan
            {
                tenNXB = ten,
                diaChi = diachi,
                sDT = sdt,
                tienNoNXB = tienno
            };
            db.NhaXuatBans.Add(a);
            db.SaveChanges();

            return Redirect("/NhaXuatBans");
        }

       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return Redirect("/NhaXuatBans");
            }
            NhaXuatBan nhaXuatBan = db.NhaXuatBans.Where(x => x.idNXB == id).FirstOrDefault<NhaXuatBan>();
            ViewBag.nxb = nhaXuatBan;
            if (nhaXuatBan == null)
            {
                return Redirect("/NhaXuatBans");
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
            NhaXuatBan a = db.NhaXuatBans.Where(x => x.idNXB == id).FirstOrDefault<NhaXuatBan>();
            if (a != null)
            {
                a.tenNXB = ten;
                a.diaChi = diachi;
                a.sDT = sdt;
                db.SaveChanges();
            }
            return Redirect("/NhaXuatBans");
        }

        // GET: NhaXuatBans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaXuatBan a = db.NhaXuatBans.Where(x => x.idNXB == id).FirstOrDefault<NhaXuatBan>();
            if (a == null)
            {
                return HttpNotFound();
            }
            db.NhaXuatBans.Remove(a);
            db.SaveChanges();
            return Redirect("/NhaXuatBans");
        }

    }
}
