using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF;

namespace PROJECT2.Models
{
    public class CTHoaDonDAO
    {
        MuaBanSachEntities1 db = null;
        public CTHoaDonDAO()
        {
            db = new MuaBanSachEntities1();
        }
        public int Insert(CTHoaDonBan cthdban)
        {          
                db.CTHoaDonBans.Add(cthdban);
                db.SaveChanges();
                return cthdban.idCTHDB;                      
        }
    }
}