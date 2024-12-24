using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using Npgsql;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hastane_Otomasyonu
{
    public partial class FrmHastaKayit : Form
    {
        public FrmHastaKayit()
        {
            InitializeComponent();
        }

       
        private void FrmHastaKayit_Load(object sender, EventArgs e)
        {
           
            
        }
		DatabaseConnection db = new DatabaseConnection();
		private void button1_Click(object sender, EventArgs e)
		{
			// PostgreSQL bağlantısı
			using (var conn = db.GetConnection())
			{
				try
				{
					conn.Open();

					// Veri ekleme komutunu yazma
					using (var komut = new NpgsqlCommand("INSERT INTO tbl_hastalar (HastaAd, HastaSoyad, HastaTc, HastaTelefon, HastaSifre, HastaCinsiyet) VALUES (@p1, @p2, @p3, @p4, @p5, @p6)", conn))
					{
						komut.Parameters.AddWithValue("@p1", txtAd.Text);
						komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
						komut.Parameters.AddWithValue("@p3", MskTC.Text);
						komut.Parameters.AddWithValue("@p4", MskTelefon.Text);
						komut.Parameters.AddWithValue("@p5", TxtSifre.Text);
						komut.Parameters.AddWithValue("@p6", CmbCinsiyet.Text);

						// Sorguyu çalıştırma
						komut.ExecuteNonQuery();
					}

					// Başarı mesajı
					MessageBox.Show("Kayıt İşlemi Başarıyla Tamamlandı");
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Veri ekleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
