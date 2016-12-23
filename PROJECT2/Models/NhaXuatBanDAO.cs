using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF;

namespace PROJECT2.Models
{
    public class NhaXuatBanDAO
    {
        MuaBanSachEntities1 db = new MuaBanSachEntities1();
        public NhaXuatBanDAO()
        {
           
        }
        public int Insert(NhaXuatBan nxb)
        {
            db.NhaXuatBans.Add(nxb);
            db.SaveChanges();
            return nxb.idNXB;
        }
        public void Update(int id, string tiennonxb)
        {
            NhaXuatBan nxb = db.NhaXuatBans.Single(n => n.idNXB == id);
            nxb.tienNoNXB = tiennonxb;
            db.SaveChanges();
            return;
        }
    }
}