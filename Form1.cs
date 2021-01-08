using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ungdunghoctumoi
{
    public partial class Form1 : Form
    {
        NhanVat nhanvat = new NhanVat();
        List<Label> lblTiengAnh = new List<Label>();//trên panel 1
        List<Label> lblTiengViet = new List<Label>();//trên panel 1
        Timer timer1 = new Timer();
        int phut = 0, giay = 0, tongthoigian;
        public Form1()
        {
            InitializeComponent();
            nhanvat.LoadNewWord();
            timer1.Tick += Timer1_Tick;

        }
        Label TaoLabel(int x, int y, string text, string name)// tạo 1 label
        {
            Label xxx = new Label();
            xxx.AutoSize = true;
            xxx.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xxx.Location = new System.Drawing.Point(x, y);
            xxx.Size = new System.Drawing.Size(218, 27);
            xxx.Name = name;
            xxx.Text = text;
            return xxx;
        }
        void TaoLabelTiengViet()//tạo label nghĩa tiếng việt
        {

            int x = 200, y = 160;
            for (int i = 0; i < nhanvat.SoLuongTuMoi; i++)
            {
                Label xxx = TaoLabel(x, y, nhanvat.taketv(i), "lbltv" + i.ToString());
                lblTiengViet.Add(xxx);
                y += 20;
            }
        }
        void TaoLabelTiengAnh()//tạo label nghĩa tiếng anh
        {
            int x = 100, y = 160;
            for (int i = 0; i < nhanvat.SoLuongTuMoi; i++)
            {
                Label xxx = TaoLabel(x, y, nhanvat.taketa(i), "lblta" + i.ToString());
                lblTiengAnh.Add(xxx);
                y += 20;
            }
        }
        void Add()// add các label lên 
        {
            for (int i = 0; i < nhanvat.SoLuongTuMoi; i++)
            {
                groupBox1.Controls.Add(lblTiengViet[i]);
                groupBox1.Controls.Add(lblTiengAnh[i]);
            }
        }    

        //control sử dụng
        private void btnStart_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtSoLuongTu.Text);
            if (x > 9 || x < 4)
            {
                MessageBox.Show("Nhập sai quy định!!!", "Cảnh cáo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuongTu.Focus();
            }
            else
            {
                nhanvat.SoLuongTuMoi = x;
                nhanvat.TakeRandomNewWord();
                TaoLabelTiengAnh();
                TaoLabelTiengViet();
                Add();
                gantumoi();
                tongthoigian = x * 35;
                timer1.Enabled = true;
            }
        }
        private void txtSoLuongTu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < '0' || e.KeyChar > '9' && (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }
        private void btnCong_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtSoLuongTu.Text);
            if (x > 8)
            {
                MessageBox.Show("Không khuyến khích học nhiều nhé!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                x++;
                txtSoLuongTu.Text = x.ToString();
            }
        }
        private void btnTru_Click(object sender, EventArgs e)
        {
            int x = int.Parse(txtSoLuongTu.Text);
            if (x < 5)
            {
                MessageBox.Show("Học ít thế, thêm đi!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                x--;
                txtSoLuongTu.Text = x.ToString();
            }
        }
        List<string> tumoita = new List<string>();
        List<string> tumoitv = new List<string>();
        void gantumoi()
        {
            for(int i=0;i<nhanvat.SoLuongTuMoi;i++)
            {
                tumoita.Add(nhanvat.taketa(i));
                tumoitv.Add(nhanvat.taketv(i));

            }
        }
        public delegate void NhanDuLieu(NhanVat x);

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtthemtuta.Text != "" && txtthemtutv.Text != "")
            {
                nhanvat.ThemTu(txtthemtuta.Text, txtthemtutv.Text);
                txtthemtutv.Clear();
                txtthemtuta.Clear();
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public NhanDuLieu Sender;
        private void Timer1_Tick(object sender, EventArgs e)
        {
            {
                if (tongthoigian == phut * 60 + giay) //mở sang form kiểm tra
                {
                    timer1.Enabled = false;
                    Form2 f2 = new Form2();
                    NhanDuLieu nhandulieu = new NhanDuLieu(f2.GetData);
                    nhandulieu(nhanvat);
                    this.Hide();
                    f2.ShowDialog();
       
                }
                else
                {
                    giay++;
                    if (giay == 60)
                    {
                        phut += 1;
                        giay = 0;
                        if (phut < 10)
                            lblphut.Text = "0" + phut.ToString();
                        else
                            lblphut.Text = phut.ToString();
                        lblgiay.Text = ": " + giay.ToString();
                    }
                    else
                    {
                        if (giay < 10)
                            lblgiay.Text = ": 0" + giay.ToString();
                        else
                            lblgiay.Text = ": " + giay.ToString();
                    }
                }
            }
        }
    }
}
    

