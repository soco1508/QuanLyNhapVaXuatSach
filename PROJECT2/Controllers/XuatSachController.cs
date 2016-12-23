using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EF;
using PROJECT2.Models;
using System.Globalization;

namespace PROJECT2.Controllers
{
    public class XuatSachController : Controller
    {

        private MuaBanSachEntities1 db = new MuaBanSachEntities1();

        public ActionResult Index()
        {
            ViewBag.linhvuc = db.LinhVucs.ToList();
            int cartcount = 0;
            if (Session["ShoppingCart1"] != null)
            {
                List<CartItem> list = Session["ShoppingCart1"] as List<CartItem>;

                for (int i = 0; i < list.Count; i++)
                {
                    cartcount = cartcount + list[i].Quantity;
                }
            }
            ViewBag.soluong = cartcount;
            return View();
        }

        public JsonResult TakeData(string tenlinhvuc, string tensach, string page)
        {
            var query = (from b in db.Saches
                         join c in db.LinhVucs on b.idLV equals c.idLV
                         where c.linhVuc1.Contains(tenlinhvuc) && b.tenSach.Contains(tensach)
                          && b.soLuong > 0
                         select new
                         {
                             idsach = b.idSach,
                             tensach = b.tenSach,
                             soluong = b.soLuong,
                             gianhap = b.donGiaNhap,
                             giaban = b.donGiaBan,
                             tenlinhvuc = c.linhVuc1
                         }).OrderBy(x => x.tensach);
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

        public JsonResult AddToCart(string idsach)
        {
            int id = Int32.Parse(idsach);
            List<CartItem> list = new List<CartItem>();
            if (Session["ShoppingCart1"] == null)
            {
                list.Add(new CartItem { Quantity = 1, productOrder = db.Saches.Find(id) });
            }
            else
            {
                list = Session["ShoppingCart1"] as List<CartItem>;
                int k = -1;
                for (int i = 0; i < list.Count; i++)
                {
                    Sach a = list[i].productOrder;
                    if (a.idSach == id)
                    {
                        k = i;
                        break;
                    }
                }
                if (k == -1)
                {
                    CartItem b = new CartItem { Quantity = 1, productOrder = db.Saches.Find(id) };
                    list.Add(b);
                }
                else
                {
                    list[k].Quantity = list[k].Quantity + 1;
                }
            }
            Session["ShoppingCart1"] = list;
            int cartcount = 0;
            for (int i = 0; i < list.Count; i++)
            {
                cartcount = cartcount + list[i].Quantity;
            }
            return Json(new { ItemAmount = cartcount }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            Sach sach = db.Saches.Where(x => x.idSach == id).FirstOrDefault<Sach>();
            ViewBag.sach = sach;
            return View();
        }

        public ActionResult ShoppingCart()
        {
            List<CartItem> cart = Session["ShoppingCart1"] as List<CartItem>;
            ViewBag.Cart = cart;
            return View();
        }

        public ActionResult DeleteAll()
        {
            Session["ShoppingCart1"] = null;
            return Redirect("/XuatSach/ShoppingCart");
        }

        public ActionResult Delete(int id)
        {
            if (Session["ShoppingCart1"] == null)
            {
                return Redirect("/XuatSach/ShoppingCart");
            }
            else
            {
                List<CartItem> list = new List<CartItem>();
                list = Session["ShoppingCart1"] as List<CartItem>;
                int k = -1;
                for (int i = 0; i < list.Count; i++)
                {
                    Sach a = list[i].productOrder;
                    if (a.idSach == id)
                    {
                        k = i;
                        break;
                    }
                }
                if (k != -1)
                {
                    list.RemoveAt(k);
                    Session["ShoppingCart1"] = list;
                }
                return Redirect("/XuatSach/ShoppingCart");
            }
        }

        public ActionResult Update(string[] cartList)
        {
            if (Session["ShoppingCart1"] != null)
            {
                List<CartItem> list = Session["ShoppingCart1"] as List<CartItem>;
                for (int i = 0; i < list.Count; i++)
                {
                    int soluong = Int32.Parse(cartList[i]);
                    list[i].Quantity = soluong;
                }
                Session["ShoppingCart1"] = list;
            }
            return Json(new { mess = "Thành công" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Payment()
        {
            ViewBag.dl = db.DaiLies.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Payment2()
        {
            var iddl = Request.Form["iddl"];
            var diachi = Request.Form["diachi"];
            var sdt = Request.Form["sdt"];
            var nguoinhan = Request.Form["nguoinhan"];
            //var ngayxuat = DateTime.ParseExact(Request.Form["ngayxuat"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var ngayxuat = Request.Form["ngayxuat"];
            double tongtien = 0;
            int id = Int32.Parse(iddl);

            DaiLy dl = db.DaiLies.Where(x => x.idDL == id).FirstOrDefault<DaiLy>();

            if (Session["ShoppingCart1"] != null)
            {
                List<CartItem> list = Session["ShoppingCart1"] as List<CartItem>;
                for (int i = 0; i < list.Count; i++)
                {
                    tongtien = tongtien + (list[i].Quantity * Convert.ToDouble(list[i].productOrder.donGiaBan));
                }
            }
            List<CartItem> cart = Session["ShoppingCart1"] as List<CartItem>;
            //
            var hoadonbanDAO = new HoaDonDAO();
            HoaDonBan hoadon = new HoaDonBan
            {
                idDL = Int32.Parse(iddl),
                diaChi = diachi,
                sDT = sdt,
                nguoiNhanSach = nguoinhan,
                ngayBan = DateTime.Parse(ngayxuat),
                tongTien = tongtien.ToString()
            };
            hoadonbanDAO.Insert(hoadon);
            //
            var dlDAO = new DaiLyDAO();

            double temp = Convert.ToDouble(dl.tienDLNo) + Convert.ToDouble(tongtien);
            string temp1 = Convert.ToString(temp);
            dlDAO.Update(id, temp1);
            //
            var chitietDAO = new CTHoaDonDAO();
            var chitietDLDAO = new CTSachDaiLyLayDAO();
            foreach (var item in cart)
            {
                CTHoaDonBan cthoadonban = new CTHoaDonBan
                {
                    idHDB = hoadon.idHDB,
                    idLV = item.productOrder.idLV,
                    idSach = item.productOrder.idSach,
                    soLuong = item.Quantity,
                };
                chitietDAO.Insert(cthoadonban);

                int idsach4 = item.productOrder.idSach;
                Sach h = db.Saches.Where(x => x.idSach == idsach4).FirstOrDefault<Sach>();
                h.soLuong = h.soLuong - item.Quantity;
                db.SaveChanges();

                ChiTietSachDaiLyLay ctsdll = new ChiTietSachDaiLyLay
                {
                    idDL = dl.idDL,
                    idSach = item.productOrder.idSach,
                    ngayLay = DateTime.Parse(ngayxuat),
                    soLuongXuat = item.Quantity,
                };
                chitietDLDAO.Insert(ctsdll);
            }
            //
            HoaDonBan hoadon2 = db.HoaDonBans.Where(x => x.idHDB == hoadon.idHDB).FirstOrDefault<HoaDonBan>();
            ViewBag.hoadon = hoadon2;
            return View("success1");
        }
    }
}