using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;

namespace DAO
{
    public class DAOCTSach
    {
        MuaBanSachEntities db = null;
        public DAOCTSach()
        {
            db = new MuaBanSachEntities();
        }
        public int Insert(ChiTietSach cts)
        {
            db.ChiTietSaches.Add(cts);
            db.SaveChanges();
            return cts.idCTSach;
        }
    }
}
