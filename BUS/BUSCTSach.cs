using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;

namespace BUS
{
    public class BUSCTSach
    {
        MuaBanSachEntities1 db = new MuaBanSachEntities1();
        public IEnumerable<ChiTietSach> DSCTS()
        {
            IEnumerable<ChiTietSach> ctsach = from c in db.ChiTietSaches                                           
                                              select c;
            return ctsach;
        }
        public void InsertCTSach(string tentg, int idnxb, DateTime nxb, int st, string kg)
        {
            ChiTietSach cts = new ChiTietSach();
            cts.tenTacGia = tentg;
            cts.idNXB = idnxb;
            cts.ngayXuatBan = nxb;
            cts.soTrang = st;
            cts.khoGiay = kg;
            db.ChiTietSaches.Add(cts);
            db.SaveChanges();           
        }
        public void Update(int id, string tentg, DateTime nxb, int st, string kg)
        {
            var query = db.ChiTietSaches.Where(x => x.idCTSach == id);
            foreach(ChiTietSach sach in query)
            {
                sach.tenTacGia = tentg;
                sach.ngayXuatBan = nxb;
                sach.soTrang = st;
                sach.khoGiay = kg;
            }
            db.SaveChanges();
            return;
        }
        public void Delete(int id)
        {
            ChiTietSach ctsach = (from c in db.ChiTietSaches select c).Single(t => t.idCTSach == id);
            db.ChiTietSaches.Remove(ctsach);
            db.SaveChanges();
            return;
        }
        public IEnumerable<ChiTietSach> Search(string s)
        {
            IEnumerable<ChiTietSach> ctsach = from c1 in db.ChiTietSaches
                                              where c1.tenTacGia.Contains(s) || c1.NhaXuatBan.tenNXB.Contains(s)
                                              select c1;
            return ctsach;
        }
    }
}
