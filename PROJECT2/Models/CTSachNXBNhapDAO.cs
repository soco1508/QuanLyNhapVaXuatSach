using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF;

namespace PROJECT2.Models
{
    public class CTSachNXBNhapDAO
    {
        MuaBanSachEntities1 db = null;
        public CTSachNXBNhapDAO()
        {
            db = new MuaBanSachEntities1();
        }
        public int Insert(ChiTietSachNXBNhap ctsnxbn)
        {
            db.ChiTietSachNXBNhaps.Add(ctsnxbn);
            db.SaveChanges();
            return ctsnxbn.id;
        }
    }
}