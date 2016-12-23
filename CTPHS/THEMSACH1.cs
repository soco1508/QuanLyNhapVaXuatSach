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

namespace CTPHS
{
    public partial class THEMSACH1 : Form
    {
        public THEMSACH1()
        {
            InitializeComponent();
        }
        BUSSach bussach = new BUSSach();
        BUSCTSach busctsach = new BUSCTSach();
        private void THEMSACH1_Load(object sender, EventArgs e)
        {
            cbNXB.DataSource = bussach.DSNXB().ToList();
            cbNXB.DisplayMember = "tenNXB";
            cbNXB.ValueMember = "idNXB";
            cbLinhVuc.DataSource = bussach.DSLV().ToList();
            cbLinhVuc.DisplayMember = "linhVuc1";
            cbLinhVuc.ValueMember = "idLV";
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtTenSach.Text = "";
            txtTenTG.Text = "";
            dtpkNgayXB.Value = DateTime.Now;
            cbLinhVuc.Text = "";
            txtGiaNhap.Value = 0;
            txtGiaBan.Value = 0;
            cbNXB.Text = "";
            txtST.Value = 1;
            txtKG.Text = "";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTenSach.Text)) lbTenSach.Text = "Bạn chưa nhập tên sách.";
                else if (txtGiaNhap.Value == 0) lbDGN.Text = "Giá nhập phải lớn hơn 0.";
                else if (txtGiaBan.Value == 0) lbDGB.Text = "Giá bán phải lớn hơn 0.";
                else if (string.IsNullOrEmpty(txtTenTG.Text)) lbTenTG.Text = "Bạn chưa nhập tên tác giả.";
                else if (dtpkNgayXB.Value.Date >= DateTime.Now.Date) lbNgayXB.Text = "Ngày xuất bản phải trước ngày hiện tại.";
                else if (txtST.Value <= 1) lbSoTrang.Text = "Số trang phải lớn hơn 1.";
                else if (string.IsNullOrEmpty(txtKG.Text)) lbKhoGiay.Text = "Bạn chưa nhập khổ giấy.";
                else
                {
                    string tensach = txtTenSach.Text;
                    int idlv = int.Parse(cbLinhVuc.SelectedValue.ToString());
                    string dgn = txtGiaNhap.Value.ToString();
                    string dgb = txtGiaBan.Value.ToString();
                    string tacgia = txtTenTG.Text;
                    int idnxb = int.Parse(cbNXB.SelectedValue.ToString());
                    DateTime ngayxb = dtpkNgayXB.Value;
                    int st = int.Parse(txtST.Value.ToString());
                    int sl = 0;
                    string kg = txtKG.Text;
                    busctsach.InsertCTSach(tacgia, idnxb, ngayxb, st, kg);
                    bussach.InsertSach(idlv, tensach, sl, dgb, dgn);
                    MessageBox.Show("Đã lưu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTenSach.Text = "";
                    txtTenTG.Text = "";
                    dtpkNgayXB.Value = DateTime.Now;
                    cbLinhVuc.Text = "";
                    txtGiaNhap.Value = 0;
                    txtGiaBan.Value = 0;
                    cbNXB.Text = "";
                    txtST.Value = 1;
                    txtKG.Text = "";
                    lbTenSach.Text = "";
                    lbDGN.Text = "";
                    lbDGB.Text = "";
                    lbTenTG.Text = "";
                    lbNgayXB.Text = "";
                    lbSoTrang.Text = "";
                    lbKhoGiay.Text = "";
                }                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }
    }
}
