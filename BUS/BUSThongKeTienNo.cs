using EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BUS
{
    public class BUSThongKeTienNo
    {
        MuaBanSachEntities1 db = new MuaBanSachEntities1();
        //Thống kê cho nhà xuất bản
        public IEnumerable<HoaDonNhap> ThongKe(int manxb, DateTime dt)
        {
            int year = dt.Year;
            int month = dt.Month;
            DateTime end = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            var a = from n in db.NhaXuatBans
                    from h in db.HoaDonNhaps
                    where n.idNXB == h.idNXB && n.idNXB == manxb && h.ngayNhap <= end
                    select h;
            return a;
        }

        public double TongTien(int manxb,DateTime dt,IEnumerable<HoaDonNhap> list)
        {
            DateTime end = new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));
            double tongtien = 0;
            if(list.Count() != 0)
            {
                foreach (var item in list)
                {
                    tongtien = tongtien + Convert.ToDouble(item.tongTien);
                }
            }
            
            double tienno = 0;
            var query = from b in db.LuuTienNoNXBs
                        where b.idnxb == manxb && b.ngay <= end
                        select b;
            //MessageBox.Show(query.Count().ToString());

            if(query.Count() != 0)
            {
                foreach (var item in query)
                {
                    tienno = tienno + Convert.ToDouble(item.tienNo);
                }
            }           
            return tongtien - tienno;
        }
       
        public IEnumerable<CTHoaDonNhap> CTSNXBN(int idhdn)
        {
            var result = db.CTHoaDonNhaps.Where(s => s.idHDN == idhdn);
            return result;
        }     
        public string GetGiaNhap(int id)
        {
            return (from s in db.Saches where s.idSach == id select s.donGiaNhap).FirstOrDefault();
        }
        public string GetTenNXB(int id)
        {
            return (from s in db.NhaXuatBans where s.idNXB == id select s.tenNXB).FirstOrDefault();
        }        
        public List<HoaDonBan> ThongKe1(int madl, DateTime dt)
        {
            int year = dt.Year;
            int month = dt.Month;
            DateTime end = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            var a = from n in db.DaiLies
                    from h in db.HoaDonBans
                    where n.idDL == h.idDL && n.idDL == madl && h.ngayBan <= end
                    select h;
            return a.ToList();
        }

        public double TongTien1(List<HoaDonBan> list)
        {
            double tongtien = 0;
            for (int i = 0; i < list.Count; i++)
            {
                tongtien = tongtien + Convert.ToDouble(list[i].tongTien);
            }
            return tongtien;
        }

        public void thanhtoan(int madaili, DateTime dt)
        {
            int year = dt.Year;
            int month = dt.Month;
            DateTime start = new DateTime(year, month,1);
            DateTime end = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            //

            var query0 = from b in db.LuuTienNoNXBs
                         where b.iddl == madaili && b.ngay == dt
                         select b;
            if(query0.Count() != 0)
            {
                MessageBox.Show("Ban da thanh toan roi.","Thong bao",MessageBoxButtons.OK);
                return;
            }

            var query = from n in db.DaiLies
                        from h in db.HoaDonBans
                        where n.idDL == madaili && n.idDL == h.idDL && h.ngayBan >= start && h.ngayBan <= end
                        select h;
            if(query.Count() != 0)
            {
                foreach (var item in query.ToList())
                {
                    foreach (var item1 in item.CTHoaDonBans.ToList())
                    {
                        LuuTienNoNXB t = new LuuTienNoNXB
                        {
                            ngay = end,
                            iddl = madaili,
                            idnxb = item1.Sach.ChiTietSach.idNXB,
                            tienNo = (item1.soLuong * Convert.ToInt32(item1.Sach.donGiaNhap)).ToString(),
                        };
                        db.LuuTienNoNXBs.Add(t);
                        db.SaveChanges();
                    }
                }
            }                      
        }

        public IEnumerable<CTHoaDonBan> CTSDLL(int idhdb)
        {
            var result = db.CTHoaDonBans.Where(s => s.idHDB == idhdb);            
            return result.ToList();
        }
        public string GetTenSach(int id)
        {
            return (from s in db.Saches where s.idSach == id select s.tenSach).FirstOrDefault();                            
        }
        public string GetLinhVuc(int id)
        {
            return (from l in db.LinhVucs where l.idLV == id select l.linhVuc1).FirstOrDefault();
        }
        public string GetGiaBan(int id)
        {
            return (from s in db.Saches where s.idSach == id select s.donGiaBan).FirstOrDefault();
        }
        public string GetTenDL(int id)
        {
            return (from s in db.DaiLies where s.idDL == id select s.tenDaiLy).FirstOrDefault();
        }
        //Giai đoạn tính tiền trả nợ nhà xuất bản
        public int GetIDNXB(int idsach, int idct)
        {
            
            return (from s in db.Saches
                    from ct in db.ChiTietSaches
                    where s.idCTSach == ct.idCTSach && s.idSach == idsach && ct.idCTSach == idct
                    select ct.idNXB).FirstOrDefault();
        }
        public int GetIDCTS(int id)
        {
            return (from s in db.Saches where s.idSach == id select s.idCTSach).FirstOrDefault();
        }
        public string GetTienNoNXB(int id)
        {
            return (from n in db.NhaXuatBans where n.idNXB == id select n.tienNoNXB).FirstOrDefault();
        }
        public void UpdateTienNoNXB(int id, string tienno)
        {
            NhaXuatBan nxb = db.NhaXuatBans.Single(d => d.idNXB == id);
            nxb.tienNoNXB = tienno;
            db.SaveChanges();
            return;
        }
    }
}
