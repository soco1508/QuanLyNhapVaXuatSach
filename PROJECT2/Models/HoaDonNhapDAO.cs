using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF;

namespace PROJECT2.Models
{
    public class HoaDonNhapDAO
    {
        MuaBanSachEntities1 db = null;
        public HoaDonNhapDAO()
        {
            db = new MuaBanSachEntities1();
        }
        public bool Insert(HoaDonNhap hoadonnhap)
        {
            try
            {
                db.HoaDonNhaps.Add(hoadonnhap);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}