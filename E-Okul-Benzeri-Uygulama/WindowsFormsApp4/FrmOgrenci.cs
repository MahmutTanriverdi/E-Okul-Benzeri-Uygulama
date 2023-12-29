using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Policy;

namespace WindowsFormsApp4
{
    public partial class FrmOgrenci : Form
    {
        public FrmOgrenci()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        void liste()
        {
            SqlCommand komut = new SqlCommand("Select ogrId, ogrAd, ogrSoyad, kulupAd, ogrCinsiyet from Tbl_Ogrenciler " +
                "inner join Tbl_Kulupler on Tbl_Ogrenciler.ogrKulup = Tbl_Kulupler.kulupId",bgl.baglanti());
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.LightBlue;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.Transparent;
        }

        

        private void FrmOgrenci_Load(object sender, EventArgs e)
        {
            liste();
            SqlCommand komut = new SqlCommand("Select * from Tbl_Kulupler",bgl.baglanti());
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbKulup.DisplayMember = "kulupAd";
            CmbKulup.ValueMember = "kulupId";
            CmbKulup.DataSource = dt;
            bgl.baglanti().Close();
        }
        string cinsiyet = "";
        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Insert into Tbl_Ogrenciler(ogrAd, ogrSoyad, ogrKulup, ogrCinsiyet)" +
                "values(@p1,@p2,@p3,@p4)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtOgrenciAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtOgrenciSoyad.Text);
            komut.Parameters.AddWithValue("@p3", CmbKulup.SelectedValue.ToString());
            komut.Parameters.AddWithValue("@p4", cinsiyet);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Öğrenci Ekleme İşlemi Başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            liste();
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            liste();
        }

        private void CmbKulup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TxtOgrenciId.Text = CmbKulup.SelectedValue.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete from Tbl_Ogrenciler where ogrId=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtOgrenciId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Öğrenci Silme İşlemi Başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            liste();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtOgrenciId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtOgrenciAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtOgrenciSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            CmbKulup.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            if (dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString() == "Erkek")
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update Tbl_Ogrenciler set ogrAd=@p1, ogrSoyad=@p2, ogrKulup=@p3, ogrCinsiyet=@p4 where ogrId=@p5",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtOgrenciAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtOgrenciSoyad.Text);
            komut.Parameters.AddWithValue("@p3", CmbKulup.SelectedValue);
            if(radioButton1.Checked == true)
            {
                komut.Parameters.AddWithValue("@p4", cinsiyet);
            }
            if (radioButton2.Checked == true)
            {
                komut.Parameters.AddWithValue("@p4", cinsiyet);
            }
            komut.Parameters.AddWithValue("@p5", TxtOgrenciId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Öğrenci Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            liste();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true)
            {
                cinsiyet = "Kız";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                cinsiyet = "Erkek";
            }
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select ogrId, ogrAd, ogrSoyad, kulupAd, ogrCinsiyet from Tbl_Ogrenciler " +
                " inner join Tbl_Kulupler on Tbl_Ogrenciler.ogrKulup = Tbl_Kulupler.kulupId where ogrAd = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtOgrenciAra.Text);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void TxtOgrenciAra_TextChanged(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select ogrId, ogrAd, ogrSoyad, kulupAd, ogrCinsiyet from Tbl_Ogrenciler " +
                " inner join Tbl_Kulupler on Tbl_Ogrenciler.ogrKulup = Tbl_Kulupler.kulupId where ogrAd like '%" +TxtOgrenciAra.Text + "%' ", bgl.baglanti());
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox8_MouseHover(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.LightBlue;
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.Transparent;
        }
    }
}
