using System;
using System.Linq;
using System.Windows.Forms;
using BUS;
using System.ComponentModel;
using EF;
using System.Collections.Generic;
using iTextSharp.text;

namespace CTPHS
{
    public partial class SACH : Form
    {
        public SACH()
        {
            InitializeComponent();           
        }
        BUSSach bussach = new BUSSach();                
        #region Mới
        private void SACH_Load_1(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();             
            List<Sach> list = bussach.DSS().ToList();
            for(int i = 0; i < list.Count; i++)
            {
                double a = Convert.ToDouble(list[i].donGiaBan);
                string b = a.ToString("N0");
                double a1 = Convert.ToDouble(list[i].donGiaNhap);
                string b1 = a1.ToString("N0");
                dataGridView1.Rows.Add(list[i].idSach, list[i].tenSach,list[i].LinhVuc.linhVuc1, list[i].soLuong, b, b1, list[i].idCTSach);                
            }                         
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            FIRSTFORM f = new FIRSTFORM();
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CTSACH f = new CTSACH();
            f.ShowDialog();
        }
        
        private void btnThem1_Click(object sender, EventArgs e)
        {
            THEMSACH1 f = new THEMSACH1();
            f.ShowDialog();
        }

        private void btnSua1_Click(object sender, EventArgs e)
        {
            try
            {
                int i = dataGridView1.CurrentCell.RowIndex;
                int id = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString());
                int sl = Convert.ToInt32(dataGridView1.Rows[i].Cells["soluong"].Value.ToString());
                string tensach = dataGridView1.Rows[i].Cells["tensach"].Value.ToString();
                string dgb = dataGridView1.Rows[i].Cells["dongiaban"].Value.ToString();
                string dgn = dataGridView1.Rows[i].Cells["dongianhap"].Value.ToString();
                bussach.Update(id, tensach, sl, dgb, dgn);
                MessageBox.Show("Đã cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa1_Click(object sender, EventArgs e)
        {
            try
            {
                int i = dataGridView1.CurrentCell.RowIndex;
                int id = Convert.ToInt32(dataGridView1.Rows[i].Cells["masach"].Value.ToString());
                int id1 = Convert.ToInt32(dataGridView1.Rows[i].Cells["machitiet"].Value.ToString());
                bussach.Delete(id,id1);
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.Rows.Clear();
                List<Sach> list = bussach.DSS().ToList();
                for (int j = 0; j < list.Count; j++)
                {
                    double a = Convert.ToDouble(list[j].donGiaBan);
                    string b = a.ToString("N0");
                    double a1 = Convert.ToDouble(list[j].donGiaNhap);
                    string b1 = a1.ToString("N0");
                    dataGridView1.Rows.Add(list[j].idSach, list[j].tenSach, list[j].LinhVuc.linhVuc1, list[j].soLuong, b, b1, list[j].idCTSach);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string s = txtTim1.Text;
                dataGridView1.Rows.Clear();
                List<Sach> list = bussach.Search(s).ToList();
                for(int i = 0; i < list.Count; i++)
                {
                    double a = Convert.ToDouble(list[i].donGiaBan);
                    string b = a.ToString("N0");
                    double a1 = Convert.ToDouble(list[i].donGiaNhap);
                    string b1 = a1.ToString("N0");
                    dataGridView1.Rows.Add(list[i].idSach, list[i].tenSach, list[i].LinhVuc.linhVuc1, list[i].soLuong, b, b1, list[i].idCTSach);
                }
            }
            catch (Exception)
            {
                
            }
        }      
        #endregion
    }
}
