using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF;

namespace PROJECT2.Models
{
    public class CTSachDaiLyLayDAO
    {
        MuaBanSachEntities1 db = null;
        public CTSachDaiLyLayDAO()
        {
            db = new MuaBanSachEntities1();
        }
        public int Insert(ChiTietSachDaiLyLay ctsdll)
        {
            db.ChiTietSachDaiLyLays.Add(ctsdll);
            db.SaveChanges();
            return ctsdll.id;
        }
    }
}