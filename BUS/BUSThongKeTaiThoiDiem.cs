using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;
using System.Globalization;

namespace BUS
{
    public class BUSThongKeTaiThoiDiem
    {
        MuaBanSachEntities1 db = new MuaBanSachEntities1();
        public int Thongke(int id, DateTime ngay)
        {
            int xuat = 0;
            int nhap = 0;                    
            int? soluongxuat = (from hdb in db.HoaDonBans
                                from ct in db.CTHoaDonBans
                                where hdb.idHDB == ct.idHDB && ct.idSach == id && hdb.ngayBan < ngay
                                select ct.soLuong).Sum();
            int? soluongnhap = (from hdn in db.HoaDonNhaps
                                from ct in db.CTHoaDonNhaps
                                where hdn.idHDN == ct.idHDN && ct.idSach == id && hdn.ngayNhap < ngay
                                select ct.soLuong).Sum();
            xuat += Convert.ToInt32(soluongxuat);
            nhap += Convert.ToInt32(soluongnhap);
            return nhap - xuat;
        }
    }
}
