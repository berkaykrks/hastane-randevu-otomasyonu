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
using Npgsql;

namespace Hastane_Otomasyonu
{
    public partial class FrmBilgiDuzenle : Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }
        
        
        private void label1_Click(object sender, EventArgs e)
        {

        }
		DatabaseConnection db = new DatabaseConnection();
		public string Tcno;


		private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
		{
			// Hastaya Ait Bilgilerin Otomatik Ekrana Gelmesini Sağlayan Kod Bölümü
			MskTC.Text = Tcno;

			// PostgreSQL bağlantısı için kullanılacak using bloğu
			using (var conn = db.GetConnection())
			{
				try
				{
					conn.Open();
					// PostgreSQL için SqlCommand yerine NpgsqlCommand kullanılıyor
					using (var cmd = new NpgsqlCommand("SELECT * FROM tbl_hastalar WHERE HastaTc = @p1", conn))
					{
						cmd.Parameters.AddWithValue("@p1", MskTC.Text);
						using (var dr = cmd.ExecuteReader())
						{
							if (dr.Read())
							{
								txtAd.Text = dr["HastaAd"].ToString();
								txtSoyad.Text = dr["HastaSoyad"].ToString();
								MskTel.Text = dr["HastaTelefon"].ToString();
								txtSifre.Text = dr["HastaSifre"].ToString();
								CmbCinsiyet.Text = dr["HastaCinsiyet"].ToString();
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Veri çekme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void BtnBilgiGuncelle_Click(object sender, EventArgs e)
		{
			// Hastaya Ait Bilgilerin Güncellendiği Kod Bölümü
			using (var conn = db.GetConnection())
			{
				try
				{
					conn.Open();
					// PostgreSQL için SqlCommand yerine NpgsqlCommand kullanılıyor
					using (var cmd = new NpgsqlCommand("UPDATE tbl_hastalar SET HastaAd = @p1, HastaSoyad = @p2, HastaTelefon = @p3, HastaSifre = @p4, HastaCinsiyet = @p5 WHERE HastaTc = @p6", conn))
					{
						cmd.Parameters.AddWithValue("@p1", txtAd.Text);
						cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
						cmd.Parameters.AddWithValue("@p3", MskTel.Text);
						cmd.Parameters.AddWithValue("@p4", txtSifre.Text);
						cmd.Parameters.AddWithValue("@p5", CmbCinsiyet.Text);
						cmd.Parameters.AddWithValue("@p6", MskTC.Text);

						cmd.ExecuteNonQuery();
					}

					MessageBox.Show("Bilgileriniz Güncellendi");
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Güncelleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}


		private void button2_Click(object sender, EventArgs e)
        {
            FrmGirisler frm = new FrmGirisler();
            frm.Show();
            this.Hide();
        }
    }
}
