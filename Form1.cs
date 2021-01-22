using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using System.Data.SqlClient;

namespace yazilimsinama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        //to do çizelgesinin oluşturulması
        void listetodo()
        {
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT t.taskName,t.aciklama FROM tblTasks t inner join tblProje p on t.projeId=p.projeId inner join tblDurum d on t.durumId=d.durumId inner join tblUsers u on t.userId=u.userId WHERE t.durumId=1", bgl.baglanti());
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;

            bgl.baglanti().Close();
        }
        //in progress çizelgesinin oluşturulması
        void listeinprogress()
        {
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT t.taskName,t.aciklama FROM tblTasks t inner join tblProje p on t.projeId=p.projeId inner join tblDurum d on t.durumId=d.durumId inner join tblUsers u on t.userId=u.userId WHERE t.durumId=2", bgl.baglanti());
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            bgl.baglanti().Close();
        }

        //revision çizelgesinin oluşturulması
        void listerevision()
        {
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT t.taskName,t.aciklama FROM tblTasks t inner join tblProje p on t.projeId=p.projeId inner join tblDurum d on t.durumId=d.durumId inner join tblUsers u on t.userId=u.userId WHERE t.durumId=3", bgl.baglanti());
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            dataGridView3.DataSource = dt3;

            bgl.baglanti().Close();
        }

        //check çizelgesinin oluşturulması
        void listecheck()
        {
            SqlDataAdapter da4 = new SqlDataAdapter("SELECT t.taskName,t.aciklama FROM tblTasks t inner join tblProje p on t.projeId=p.projeId inner join tblDurum d on t.durumId=d.durumId inner join tblUsers u on t.userId=u.userId WHERE t.durumId=4", bgl.baglanti());
            DataTable dt4 = new DataTable();
            da4.Fill(dt4);
            dataGridView4.DataSource = dt4;

            bgl.baglanti().Close();
        }

        //done çizelgesinin oluşturulması
        void listedone()
        {
            SqlDataAdapter da5 = new SqlDataAdapter("SELECT t.taskName,t.aciklama FROM tblTasks t inner join tblProje p on t.projeId=p.projeId inner join tblDurum d on t.durumId=d.durumId inner join tblUsers u on t.userId=u.userId WHERE t.durumId=5", bgl.baglanti());
            DataTable dt5 = new DataTable();
            da5.Fill(dt5);
            dataGridView5.DataSource = dt5;

            bgl.baglanti().Close();
        }

        // teknik forma geçiş butonu
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            teknik teknik = new teknik();
            teknik.Show();
           
        }

        // form yükleme işlemleri
        private void Form1_Load(object sender, EventArgs e)
        {
            listetodo();
            listeinprogress();
            listerevision();
            listecheck();
            listedone();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            dashboarddesign dbd = new dashboarddesign();
            dbd.Show();
        }
    }
}
