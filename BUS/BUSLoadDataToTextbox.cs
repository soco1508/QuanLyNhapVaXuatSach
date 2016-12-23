using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;

namespace BUS
{
    public class BUSLoadDataToTextbox
    {
        MuaBanSachEntities1 db = new MuaBanSachEntities1();
        public IEnumerable<string> DSS()
        {
            IEnumerable<string> sach = from s in db.Saches
                                     select s.tenSach;
            return sach;
        }
    }
}
