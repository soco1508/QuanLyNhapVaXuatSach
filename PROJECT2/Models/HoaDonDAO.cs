using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF;

namespace PROJECT2.Models
{
    public class HoaDonDAO
    {
        MuaBanSachEntities1 db = null;
        public HoaDonDAO()
        {
            db = new MuaBanSachEntities1();
        }
        public bool Insert(HoaDonBan hoadonban)
        {
            try
            {
                db.HoaDonBans.Add(hoadonban);
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