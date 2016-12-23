using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EF;
using PROJECT2.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Globalization;

namespace PROJECT2.Controllers
{
    public class NhapSachController : Controller
    {
        // GET: NhapSach
        private MuaBanSachEntities1 db = new MuaBanSachEntities1();

        public ActionResult Index()
        {
            ViewBag.linhvuc = db.LinhVucs.ToList();
            int cartcount = 0;
            if (Session["ShoppingCart"] != null)
            {
                List<CartItem> list = Session["ShoppingCart"] as List<CartItem>;
                
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
            
            return Json(result,JsonRequestBehavior.AllowGet);
        }

       
        public JsonResult AddToCart(string idsach)
        {
            int id = Int32.Parse(idsach);
            List<CartItem> list = new List<CartItem>();
            if (Session["ShoppingCart"] == null)
            {
                list.Add(new CartItem { Quantity = 1, productOrder = db.Saches.Find(id) });
            }
            else
            {
                list = Session["ShoppingCart"] as List<CartItem>;
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
            Session["ShoppingCart"] = list;
            int cartcount = 0;
            for (int i = 0; i < list.Count; i++)
            {
                cartcount = cartcount + list[i].Quantity;
            }

            return Json(new { ItemAmount = cartcount },JsonRequestBehavior.AllowGet);
        }


        public ActionResult Details(int id)
        {
            Sach sach = db.Saches.Where(x => x.idSach == id).FirstOrDefault<Sach>();

            ViewBag.sach = sach;

            return View();
        }

        public ActionResult ShoppingCart()
        {
            List<CartItem> cart = Session["ShoppingCart"] as List<CartItem>;
            ViewBag.Cart = cart;
            return View();
        }

        public ActionResult DeleteAll()
        {
            Session["ShoppingCart"] = null;
            return Redirect("/NhapSach/ShoppingCart");
        }

        public ActionResult Delete(int id)
        {
            if(Session["ShoppingCart"] == null)
            {
                return Redirect("/NhapSach/ShoppingCart");
            }else
            {
                List<CartItem> list = new List<CartItem>();
                list = Session["ShoppingCart"] as List<CartItem>;
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
                    Session["ShoppingCart"] = list;
                }
                return Redirect("/NhapSach/ShoppingCart");
            }
            
        }

        
        public ActionResult Update(string[] cartList)
        {
            if(Session["ShoppingCart"] != null)
            {
                List<CartItem> list = Session["ShoppingCart"] as List<CartItem>;
                for(int i = 0; i < list.Count; i++)
                {
                    int soluong = Int32.Parse(cartList[i]);
                    list[i].Quantity = soluong;
                }
                Session["ShoppingCart"] = list;
            }
            return Json(new { mess = "Thành công" },JsonRequestBehavior.AllowGet);
        }

        public ActionResult Payment()
        {
            ViewBag.nxb = db.NhaXuatBans.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Payment2()
        {
            var idnxb = Request.Form["idnxb"];
            var sdt = Request.Form["sdt"];
            var nguoigiao = Request.Form["nguoigiao"];
            //var ngaynhap = DateTime.ParseExact(Request.Form["ngaynhap"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var ngaynhap = Request.Form["ngaynhap"];
            double tongtien = 0;
            int id = Int32.Parse(idnxb);

            NhaXuatBan nxb = db.NhaXuatBans.Where(x => x.idNXB == id).FirstOrDefault<NhaXuatBan>();

            if(Session["ShoppingCart"] != null)
            {
                List<CartItem> list = Session["ShoppingCart"] as List<CartItem>;
                for(int i = 0; i < list.Count; i++)
                {
                    tongtien = tongtien + (list[i].Quantity * Convert.ToDouble(list[i].productOrder.donGiaNhap));
                }
            }
            List<CartItem> cart = Session["ShoppingCart"] as List<CartItem>;
            //
            var hoadonnhapDAO = new HoaDonNhapDAO();
            HoaDonNhap hoadon = new HoaDonNhap
            {
                idNXB = Int32.Parse(idnxb),
                sDT = sdt,
                nguoiGiaoSach = nguoigiao,
                ngayNhap = DateTime.Parse(ngaynhap),
                tongTien = tongtien.ToString()
            };
            hoadonnhapDAO.Insert(hoadon);
            //
            var nxbDAO = new NhaXuatBanDAO();

            double temp = Convert.ToDouble(nxb.tienNoNXB) + Convert.ToDouble(tongtien);
            string temp1 = Convert.ToString(temp);
            nxbDAO.Update(id, temp1);
            //
            var chitietDAO = new CTHoaDonNhapDAO();
            var chitietNXBDAO = new CTSachNXBNhapDAO();
            foreach (var item in cart)
            {
                CTHoaDonNhap cthoadonnhap = new CTHoaDonNhap
                {
                    idHDN = hoadon.idHDN,
                    idLV = item.productOrder.idLV,
                    idSach = item.productOrder.idSach,
                    soLuong = item.Quantity,
                };
                chitietDAO.Insert(cthoadonnhap);
                //
                int idsach4 = item.productOrder.idSach;
                Sach h = db.Saches.Where(x => x.idSach == idsach4).FirstOrDefault<Sach>();
                h.soLuong = h.soLuong + item.Quantity;
                db.SaveChanges();
                //

                ChiTietSachNXBNhap ctsnxbn = new ChiTietSachNXBNhap
                {
                    idNXB = nxb.idNXB,
                    idSach = item.productOrder.idSach,
                    ngayNhap = DateTime.Parse(ngaynhap),
                    soLuongNhap = item.Quantity,
                };
                chitietNXBDAO.Insert(ctsnxbn);
            }
            
            HoaDonNhap hoadon2 = db.HoaDonNhaps.Where(x => x.idHDN == hoadon.idHDN ).FirstOrDefault<HoaDonNhap>();
            ViewBag.hoadon = hoadon2;
            return View("success");        
        }
    }
}