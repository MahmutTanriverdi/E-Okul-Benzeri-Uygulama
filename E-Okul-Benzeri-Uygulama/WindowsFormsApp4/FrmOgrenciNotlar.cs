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

namespace WindowsFormsApp4
{
    public partial class FrmOgrenciNotlar : Form
    {
        public FrmOgrenciNotlar()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public string numara;
        public string adSoyad;
        private void FrmOgrenciNotlar_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select dersAd, sinav1, sinav2, sinav3, proje, " +
                "ortalama, durum from Tbl_Notlar inner join Tbl_Dersler on " +
                "Tbl_Notlar.dersId = Tbl_Dersler.dersId where ogrId=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",numara);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Öğrenci Ad Soyad Yazdırma
            SqlCommand komut1 = new SqlCommand("Select ogrAd,ogrSoyad from Tbl_Ogrenciler where ogrId=@p1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", numara);
            SqlDataReader dr = komut1.ExecuteReader();
            while (dr.Read())
            {
                this.Text= dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();
        }
    }
}
