using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF;

namespace PROJECT2.Models
{
    public class CTHoaDonNhapDAO
    {
        MuaBanSachEntities1 db = null;
        public CTHoaDonNhapDAO()
        {
            db = new MuaBanSachEntities1();
        }
        public int Insert(CTHoaDonNhap cthdnhap)
        {
            db.CTHoaDonNhaps.Add(cthdnhap);
            db.SaveChanges();
            return cthdnhap.idCTHDN;
        }
    }
}