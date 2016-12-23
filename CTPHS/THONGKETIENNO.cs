using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EF;

namespace CTPHS
{
    public partial class THONGKETIENNO : Form
    {
        public THONGKETIENNO()
        {
            InitializeComponent();
        }
        BUSSach bussach = new BUSSach();
        BUSThongKeTienNo bus = new BUSThongKeTienNo();
                
        private void THONGKETIENNO_Load(object sender, EventArgs e)
        {
            cbDaiLy.DataSource = bussach.DSDL().ToList();
            cbDaiLy.DisplayMember = "tenDaiLy";
            cbDaiLy.ValueMember = "idDL";
            cbNXB.DataSource = bussach.DSNXB().ToList();
            cbNXB.DisplayMember = "tenNXB";
            cbNXB.ValueMember = "idNXB";            
            btnThanhToan.Enabled = false;
        }                

        private void btnThongKe_Click_1(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(cbDaiLy.SelectedValue.ToString());
                DateTime dt = dtpkThang.Value;
                dataGridView1.Rows.Clear();
                List<HoaDonBan> list = bus.ThongKe1(id, dt);
                double tongtien = bus.TongTien1(list);
                for (int i = 0; i < list.Count; i++)
                {
                    double temp = Convert.ToDouble(list[i].tongTien);
                    string tt = temp.ToString("N0");
                    DateTime temp1 = Convert.ToDateTime(list[i].ngayBan);
                    string datetime = temp1.ToString("dd/MM/yyyy"); 
                    dataGridView1.Rows.Add(list[i].idHDB, datetime, tt);
                }
                lbTongTienNo.Text = (tongtien.ToString("N0")) + " (VNĐ)";
            }       
            catch(Exception)
            {
                MessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            dataGridView2.Rows.Clear();
            int i = dataGridView1.CurrentCell.RowIndex;
            int idhdb = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString());              
            List<CTHoaDonBan> list = bus.CTSDLL(idhdb).ToList();           
            for (int a = 0; a < list.Count; a++)
            {                
                double temp = Convert.ToDouble(list[a].Sach.donGiaBan);
                int sl = Convert.ToInt32(list[a].soLuong);
                string dongia = temp.ToString("N0");
                double thanhtien = temp * sl;
                string thanhtien1 = thanhtien.ToString("N0");
                dataGridView2.Rows.Add(list[a].idCTHDB, list[a].idHDB, list[a].Sach.tenSach, list[a].LinhVuc.linhVuc1, list[a].soLuong, dongia, thanhtien1);
            }                
            btnThanhToan.Enabled = true;                                  
        }
        
        private void btnThongKe1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(cbNXB.SelectedValue.ToString());
                DateTime dt = dtpkThang1.Value;
                //
                dataGridView4.Rows.Clear();
                List<HoaDonNhap> list = bus.ThongKe(id, dt).ToList();
                double tongtien = bus.TongTien(id, dt, list);
                for (int i = 0; i < list.Count; i++)
                {
                    double temp = Convert.ToDouble(list[i].tongTien);
                    string tt = temp.ToString("N0");
                    DateTime temp1 = Convert.ToDateTime(list[i].ngayNhap);
                    string datetime = temp1.ToString("dd/MM/yyyy");
                    dataGridView4.Rows.Add(list[i].idHDN, datetime, tt);
                }
                lbTienNoNXB.Text = (tongtien.ToString("N0")) + " (VNĐ)";
            }
            catch(Exception)
            {
                MessageBox.Show("Không có dữ liệu.", "Thông báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }        
        }              

        private void dataGridView4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            dataGridView3.Rows.Clear();
            int i = dataGridView4.CurrentCell.RowIndex;
            int idhdn = Convert.ToInt32(dataGridView4.Rows[i].Cells["maphieunhap"].Value.ToString());
            List<CTHoaDonNhap> list = bus.CTSNXBN(idhdn).ToList();
            for (int a = 0; a < list.Count; a++)
            {                
                double temp = Convert.ToDouble(list[a].Sach.donGiaNhap);
                int sl = Convert.ToInt32(list[a].soLuong);
                string dongia = temp.ToString("N0");
                double thanhtien = temp * sl;
                string thanhtien1 = thanhtien.ToString("N0");
                dataGridView3.Rows.Add(list[a].idCTHDN, list[a].idHDN, list[a].Sach.tenSach, list[a].LinhVuc.linhVuc1, list[a].soLuong, dongia, thanhtien1);
            }
        }        
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(cbDaiLy.SelectedValue.ToString());
                DateTime dt = dtpkThang.Value;
                bus.thanhtoan(id, dt);
                MessageBox.Show("Thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception)
            {
                MessageBox.Show("Không có dữ liệu.", "Thông báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            FIRSTFORM f = new FIRSTFORM();
            f.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            FIRSTFORM f = new FIRSTFORM();
            f.ShowDialog();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }       
    }
}
