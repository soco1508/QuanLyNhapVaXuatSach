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
    public class HoaDonBansController : Controller
    {
        private MuaBanSachEntities1 db = new MuaBanSachEntities1();

        // GET: HoaDonBans
        public ActionResult Index()
        {
            List<HoaDonBan> list = db.HoaDonBans.ToList();
            ViewBag.list = list;
            return View();
        }

        // GET: HoaDonBans/Details/5
        public ActionResult Details(int? id)
        {
            HoaDonBan hoadon = db.HoaDonBans.Where(x => x.idHDB == id).FirstOrDefault<HoaDonBan>();
            ViewBag.hoadon = hoadon;
            return View();
        }

      
    }
}
