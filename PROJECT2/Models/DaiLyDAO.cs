using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF;

namespace PROJECT2.Models
{
    
    public class DaiLyDAO
    {
        MuaBanSachEntities1 db = new MuaBanSachEntities1();
        public DaiLyDAO()
        {
            
        }
        public int Insert(DaiLy daily)
        {
            db.DaiLies.Add(daily);
            db.SaveChanges();
            return daily.idDL;
        }
        public void Update(int id, string tienno)
        {
            DaiLy daily = db.DaiLies.Single(p => p.idDL == id);
            daily.tienDLNo = tienno;
            db.SaveChanges();
            return;
        }
    }
}