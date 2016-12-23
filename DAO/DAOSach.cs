using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;

namespace DAO
{
    public class DAOSach
    {
        MuaBanSachEntities db = null;
        public DAOSach()
        {
            db = new MuaBanSachEntities();
        }
        public bool Insert(Sach sach)
        {
            try
            {
                db.Saches.Add(sach);
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
