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
    public partial class teknik : Form
    {
        public teknik()
        {
            InitializeComponent();
        }

        //baglanti
        sqlbaglantisi bgl = new sqlbaglantisi();

        //listeleme sorgusu
        void listetask()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT t.taskId,t.taskName,t.aciklama,t.note,t.taskDate,t.preDate,t.realDate,t.durumId,t.projeId, t.userId,d.durumName,u.userName,p.projeName FROM tblTasks t inner join tblProje p on t.projeId=p.projeId inner join tblDurum d on t.durumId=d.durumId inner join tblUsers u on t.userId=u.userId", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            bgl.baglanti().Close();
        }

        //teknik formdan ana ekrana dönüş butonu
        private void simpleButton3_Click(object sender, EventArgs e)
        {

            
            this.Hide();
        }
        //güncelleme için bilgilerin hücrelerden çekilmesi
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblno.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtKartKonu.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            rtxtAciklama.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            rtxtNot.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            dateEdit1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            dateEdit2.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            dateEdit3.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            cbDurum.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtProjeId.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            cbUzman.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
        }
        //teknik forun açılış işlemleri
        private void teknik_Load(object sender, EventArgs e)
        {
            cbUzman.Enabled = false;
            
            SqlCommand komut = new SqlCommand("SELECT durumName from tbldurum", bgl.baglanti()) ;
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cbDurum.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
            SqlCommand komut2 = new SqlCommand("SELECT userName from tblUsers", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cbUzman.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
            // TODO: Bu kod satırı 'dbscrumDataSet.tblTasks' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.tblTasksTableAdapter.Fill(this.dbscrumDataSet.tblTasks);
            // TODO: Bu kod satırı 'dbscrumDataSet.tblUsers' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.tblUsersTableAdapter.Fill(this.dbscrumDataSet.tblUsers);
            listetask();
        }
        //ekle işlemleri
        private void btnEkle_Click(object sender, EventArgs e)
        {
            
            SqlCommand komut = new SqlCommand("exec taskEkle '" + txtKartKonu.Text + "','" + rtxtAciklama.Text + "','" + rtxtNot.Text + "','" + dateEdit1.Text + "','" + dateEdit2.Text + "','" + dateEdit3.Text + "'," + (cbDurum.SelectedIndex + 1) + "," + Convert.ToInt32(txtProjeId.Text) + "," + (cbUzman.SelectedIndex + 1) + "", bgl.baglanti());
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            listetask();
        }
        // silme işlemi
        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete from tblTasks where taskId=@p1", bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", Convert.ToInt32(lblno.Text));
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            listetask();
        }
        //Güncelleme için bilgilerin alınması
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE tblTasks SET taskName=@P2 , aciklama=@P3 , note=@P4, " +
                "taskDate=@P5, preDate=@P6, realDate=@P7 , durumId=@P8, projeId=@P9, userId=@P10 WHERE taskId=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", Convert.ToInt32(lblno.Text));
            komut.Parameters.AddWithValue("@P2", txtKartKonu.Text);
            komut.Parameters.AddWithValue("@P3", rtxtAciklama.Text);
            komut.Parameters.AddWithValue("@P4", rtxtNot.Text);
            komut.Parameters.AddWithValue("@P5", dateEdit1.Text);
            komut.Parameters.AddWithValue("@P6", dateEdit2.Text);
            komut.Parameters.AddWithValue("@P7", dateEdit3.Text);
            komut.Parameters.AddWithValue("@P8", (cbDurum.SelectedIndex + 1));
            komut.Parameters.AddWithValue("@P9", txtProjeId.Text);
            komut.Parameters.AddWithValue("@P10", (cbUzman.SelectedIndex + 1));
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("TABLO GÜNCELLEME İŞLEMİ GERÇEKLEŞTİ", "BİLGİLENDİRME", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listetask();
        }

       private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            
            cbUzman.Enabled = true;
            
        }

        private void fillBy1ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.tblUsersTableAdapter.FillBy1(this.dbscrumDataSet.tblUsers);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
        // otomatik olarak gerçekleşme süre tahmini
        private void cbUzman_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((cbUzman.SelectedIndex + 1) == 1)
                {
                    dateEdit2.EditValue = ((DateTime)dateEdit1.EditValue).AddDays(4);
                    dateEdit2.Text = Convert.ToString(dateEdit2.EditValue);

                }
                else if ((cbUzman.SelectedIndex + 1) == 2)
                {
                    dateEdit2.EditValue = ((DateTime)dateEdit1.EditValue).AddDays(6);
                    dateEdit2.Text = Convert.ToString(dateEdit2.EditValue);
                }
                else if ((cbUzman.SelectedIndex + 1) == 3)
                {
                    dateEdit2.EditValue = ((DateTime)dateEdit1.EditValue).AddDays(8);
                    dateEdit2.Text = Convert.ToString(dateEdit2.EditValue);
                }
                else
                {
                    dateEdit2.EditValue = ((DateTime)dateEdit1.EditValue).AddDays(10);
                    dateEdit2.Text = Convert.ToString(dateEdit2.EditValue);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception Message: " + ex.Message);
            }

        }

        private void txtProjeId_EditValueChanged(object sender, EventArgs e)
        {
            

        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT projeName from TBLPROJE where projeId=" + Convert.ToInt32(txtProjeId.Text) + "", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                txtProjeAd.Text = Convert.ToString(dr[0]);
            }
            bgl.baglanti().Close();

        }
    }
}
