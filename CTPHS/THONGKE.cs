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
    public partial class THONGKE : Form
    {
        public THONGKE()
        {
            InitializeComponent();
        }
        BUSSach bussach = new BUSSach();
        BUSThongKeTaiThoiDiem bus = new BUSThongKeTaiThoiDiem();
        private void THONGKE_Load(object sender, EventArgs e)
        {
            cbTenSach.DataSource = bussach.DSS().ToList();
            cbTenSach.DisplayMember = "tenSach";
            cbTenSach.ValueMember = "idSach";          
        }

        private void cbTenSach_SelectedIndexChanged(object sender, EventArgs e)
        {                                    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(cbTenSach.SelectedValue.ToString()); //Chỉ lấy giá trị dc trong button
                DateTime ngay = dtpkNgay.Value;
                lbSL.Text = (bus.Thongke(id, ngay)).ToString();
                lbNgay.Text = dtpkNgay.Value.ToString("dd/MM/yyyy");
                lbTenSach.Text = cbTenSach.Text;
            }
            catch(Exception)
            {

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
    }
}
