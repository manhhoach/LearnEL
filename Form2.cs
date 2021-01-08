using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ungdunghoctumoi
{
    public partial class Form2 : Form
    {
       
        NhanVat nhanvatchinh = new NhanVat();
        List<TuMoi> cautraloi = new List<TuMoi>();
        public Form2()
        {
            InitializeComponent();
                               
        }    
        public void GetData(NhanVat x)
        {
            nhanvatchinh = x;
        }
        List<Label> lbltiengviet = new List<Label>();
        List<TextBoxThuaKe> txtcautraloi = new List<TextBoxThuaKe>();
        Label taolabel(int x, int y, string text, string name)
        {
            Label xx = new Label();
            xx.Location = new Point(x, y);
            xx.Font = new System.Drawing.Font("Microsoft sans serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xx.Text = text;
            xx.Name = name;
            return xx;
        }
        
    
        void taolistlbl()
        {
            int x = 260, y = 30;
            for (int i = 0; i < nhanvatchinh.SoLuongTuMoi; i++)
            {
                Label xx = taolabel(x, y, nhanvatchinh.taketv(i), "lbltv" + i.ToString());
                lbltiengviet.Add(xx);
                y += 30;
            }
        }
        TextBoxThuaKe taotxt(int x, int y)
        {
            TextBoxThuaKe km = new TextBoxThuaKe();
            km.Size = new Size(180, 25);
            km.Location = new Point(x, y);
            km.Font = new System.Drawing.Font("Microsoft sans serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            return km;
        }
        void taolisttxt()
        {
            int x = 40, y = 30;
            for (int i = 0; i < nhanvatchinh.SoLuongTuMoi; i++)
            {
                TextBoxThuaKe xx = taotxt(x, y);
                txtcautraloi.Add(xx);
                y += 30;
            }
        }
        void addcontrol()
        {
            for (int i = 0; i < nhanvatchinh.SoLuongTuMoi; i++)
            {
                this.Controls.Add(lbltiengviet[i]);
                this.Controls.Add(txtcautraloi[i]);
            }
        }
        void AddCauTraLoi()
        {
            for(int i=0;i<nhanvatchinh.SoLuongTuMoi;i++)
            {
                TuMoi x = new TuMoi("", nhanvatchinh.taketv(i));
                cautraloi.Add(x);
            }
        }
        void LayCauTraLoi()
        {
            for(int i=0;i<nhanvatchinh.SoLuongTuMoi;i++)
            {
                cautraloi[i].NghiaTiengAnh = txtcautraloi[i].Text;
            }
        }
        private void btnXuLy_Click_1(object sender, EventArgs e)
        {
            LayCauTraLoi();
            lbldiem.Text="Điểm của bạn là: "+nhanvatchinh.KiemTra(cautraloi).ToString()+"/"+nhanvatchinh.SoLuongTuMoi.ToString();
            btnXuLy.Enabled = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            taolistlbl();
            taolisttxt();
            addcontrol();
            AddCauTraLoi();
        }
        string s="";
        
        private void btnXemdapan_Click(object sender, EventArgs e)
        {
            for(int i=0;i<nhanvatchinh.SoLuongTuMoi;i++)
            {
                s += nhanvatchinh.taketa(i) + " : " + nhanvatchinh.taketv(i)+"\n";
            }
            MessageBox.Show(s);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Close();
            f1.Show();
            
        
        }
    }
}
