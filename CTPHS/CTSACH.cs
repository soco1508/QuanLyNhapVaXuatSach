using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using EF;

namespace CTPHS
{
    public partial class CTSACH : Form
    {
        public CTSACH()
        {
            InitializeComponent();
        }
        BUSCTSach bus = new BUSCTSach();
        private void CTSACH_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            List<ChiTietSach> list = bus.DSCTS().ToList();
            for(int i = 0; i < list.Count; i++)
            {
                DateTime temp = Convert.ToDateTime(list[i].ngayXuatBan);
                string ngayxuatban = temp.ToString("MM/dd/yyyy");
                dataGridView1.Rows.Add(list[i].idCTSach, list[i].tenTacGia, list[i].NhaXuatBan.tenNXB, ngayxuatban, list[i].soTrang, list[i].khoGiay);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int j = dataGridView1.CurrentCell.RowIndex;
                int id = Convert.ToInt32(dataGridView1.Rows[j].Cells[0].Value.ToString());
                bus.Delete(id);
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.Rows.Clear();
                List<ChiTietSach> list = bus.DSCTS().ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    DateTime temp = Convert.ToDateTime(list[i].ngayXuatBan);
                    string ngayxuatban = temp.ToString("MM/dd/yyyy");
                    dataGridView1.Rows.Add(list[i].idCTSach, list[i].tenTacGia, list[i].NhaXuatBan.tenNXB, ngayxuatban, list[i].soTrang, list[i].khoGiay);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                int i = dataGridView1.CurrentCell.RowIndex;
                int id = Convert.ToInt32(dataGridView1.Rows[i].Cells["idct"].Value.ToString());                
                int st = Convert.ToInt32(dataGridView1.Rows[i].Cells["st"].Value.ToString());
                string tentg = dataGridView1.Rows[i].Cells["tentg"].Value.ToString();
                string kg = dataGridView1.Rows[i].Cells["kg"].Value.ToString();
                DateTime ngayxb = Convert.ToDateTime(dataGridView1.Rows[i].Cells["ngayxb"].Value.ToString());
                bus.Update(id, tentg, ngayxb, st, kg);
                MessageBox.Show("Đã cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể xóa vì dữ liệu liên quan vẫn còn ở màn hình quản lý sách.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string s = txtTim.Text;
                dataGridView1.Rows.Clear();
                List<ChiTietSach> list = bus.Search(s).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    DateTime temp = Convert.ToDateTime(list[i].ngayXuatBan);
                    string ngayxuatban = temp.ToString("MM/dd/yyyy");
                    dataGridView1.Rows.Add(list[i].idCTSach, list[i].tenTacGia, list[i].NhaXuatBan.tenNXB, ngayxuatban, list[i].soTrang, list[i].khoGiay);
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            SACH f = new SACH();
            f.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
        
    }
}
