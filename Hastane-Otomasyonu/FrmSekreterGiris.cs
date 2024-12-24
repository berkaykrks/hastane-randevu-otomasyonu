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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }


		DatabaseConnection db = new DatabaseConnection();
		private void BtnGirisYap_Click(object sender, EventArgs e)
        {

			string sekreterTc = MskTC.Text.Trim();
			string sekreterSifre = txtSifre.Text.Trim();

			// TC ve şifre boşsa kullanıcıyı uyarıyoruz
			if (string.IsNullOrEmpty(sekreterTc) || string.IsNullOrEmpty(sekreterSifre))
			{
				MessageBox.Show("Lütfen TC ve şifreyi giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// Veritabanı bağlantısı kuruyoruz
			using (var conn = db.GetConnection()) // Veritabanı bağlantısı PostgreSQL için
			{
				try
				{
					// Bağlantıyı açıyoruz
					conn.Open();

					// SQL sorgumuzu hazırlıyoruz
					string query = "SELECT * FROM tbl_Sekreter WHERE SekreterTc = @p1 AND SekreterSifre = @p2";

					using (var cmd = new NpgsqlCommand(query, conn))
					{
						// Parametreleri ekliyoruz
						cmd.Parameters.AddWithValue("@p1", sekreterTc);
						cmd.Parameters.AddWithValue("@p2", sekreterSifre);

						// Sorguyu çalıştırıp veriyi okuyabiliyoruz
						using (var dr = cmd.ExecuteReader())
						{
							// Eğer veri varsa giriş başarılı demektir
							if (dr.Read())
							{
								// Başarılı giriş durumunda sekreter detay ekranına yönlendiriyoruz
								FrmSekreterDetay frs = new FrmSekreterDetay();
								frs.TcNo = sekreterTc;  // Sekreterin TC bilgisini detay formuna gönderiyoruz
								frs.Show();  // Detay formunu gösteriyoruz
								this.Hide();  // Şu anki formu gizliyoruz
							}
							else
							{
								// Eğer giriş bilgileri yanlışsa kullanıcıya hata mesajı gösteriyoruz
								MessageBox.Show("Hatalı TC veya Şifre", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
					}
				}
				catch (Exception ex)
				{
					// Hata durumunda kullanıcıya hata mesajı gösteriyoruz
					MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
