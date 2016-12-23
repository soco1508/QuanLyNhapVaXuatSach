using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;

namespace BUS
{
    public class BUSSach
    {
        MuaBanSachEntities1 db = new MuaBanSachEntities1();
        public IEnumerable<DaiLy> DSDL()
        {
            IEnumerable<DaiLy> dl = from s in db.DaiLies select s;
            return dl;
        }

        public IEnumerable<NhaXuatBan> DSNXB()
        {
            IEnumerable<NhaXuatBan> nxb = from s in db.NhaXuatBans select s;
            return nxb;
        }

        public IEnumerable<LinhVuc> DSLV()
        {
            IEnumerable<LinhVuc> lv = from s in db.LinhVucs select s;
            return lv;
        }

        public IEnumerable<Sach> DSS()
        {
            IEnumerable<Sach> sach = from s in db.Saches select s;
            return sach;
        }
        
        public bool CheckExist(string tensach)
        {
            int kt = (from s in db.Saches where s.tenSach == tensach select s).Count();
            if (kt == 1)
            {
                return true;
            }
            else return false;
        }        
        public void InsertSach(int idlv,string tensach,int sl,string dgb,string dgn)
        {
            Sach s = new Sach();
            BUSCTSach ct = new BUSCTSach();
            int idCTSach = db.ChiTietSaches.Max(m => m.idCTSach);
            s.idLV = idlv;
            s.tenSach = tensach;
            s.soLuong = sl;
            s.donGiaBan = dgb;
            s.donGiaNhap = dgn;
            s.idCTSach = idCTSach;
            db.Saches.Add(s);
            db.SaveChanges();
        }
        public void Update(int id, string tensach, int sl, string dgb, string dgn)
        {
            var query = db.Saches.Where(x => x.idSach == id);
            foreach(Sach sach in query)
            {
                sach.tenSach = tensach;
                sach.soLuong = sl;
                sach.donGiaBan = dgb;
                sach.donGiaNhap = dgn;
            }
            db.SaveChanges();
            return;
        }
        public void Delete(int id, int id1)
        {
            Sach sach = (from s in db.Saches select s).Single(t => t.idSach == id);
            ChiTietSach ctsach = (from ct in db.ChiTietSaches select ct).Single(x => x.idCTSach == id1);
            db.Saches.Remove(sach);
            db.ChiTietSaches.Remove(ctsach);
            db.SaveChanges();
            return;
        }
        public IEnumerable<Sach> Search(string s)
        {
            IEnumerable<Sach> sach = from s1 in db.Saches
                                     where s1.tenSach.Contains(s) || s1.LinhVuc.linhVuc1.Contains(s)
                                     select s1;
            return sach;
        }
    }
}
