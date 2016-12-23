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
    public class HoaDonNhapsController : Controller
    {
        private MuaBanSachEntities1 db = new MuaBanSachEntities1();

        // GET: HoaDonBans
        public ActionResult Index()
        {
            List<HoaDonNhap> list = db.HoaDonNhaps.ToList();
            ViewBag.list = list;
            return View();
        }

        // GET: HoaDonBans/Details/5
        public ActionResult Details(int? id)
        {
            HoaDonNhap hoadon = db.HoaDonNhaps.Where(x => x.idHDN == id).FirstOrDefault<HoaDonNhap>();
            ViewBag.hoadon = hoadon;
         
            return View();
        }


    }
}
