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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }

		DatabaseConnection db = new DatabaseConnection();
		private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
			//Form Load'ında Doktorları DataTable'da Gösteren Kod Bölümü
			DataTable dt1 = new DataTable();
			using (var conn = db.GetConnection())
			{
				try
				{
					conn.Open();
					string query = "SELECT * FROM tbl_doktorlar";
					using (var da1 = new NpgsqlDataAdapter(query, conn))
					{
						da1.Fill(dt1);
						dataGridView1.DataSource = dt1;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			//Doktorlara Ait Branşı ComboBox'a Getiren Kod Bölümü
			using (var conn = db.GetConnection())
			{
				try
				{
					conn.Open();
					string query2 = "SELECT BransAd FROM tbl_branslar";
					using (var cmd2 = new NpgsqlCommand(query2, conn))
					{
						using (var dr2 = cmd2.ExecuteReader())
						{
							while (dr2.Read())
							{
								CmbBrans.Items.Add(dr2[0].ToString());
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void BtnEkle_Click(object sender, EventArgs e)
		{
			// Kullanıcının girdiği doktor bilgilerini alır ve boşlukları temizler
			string doktorAd = TxtAd.Text.Trim();
			string doktorSoyad = TxtSoyad.Text.Trim();
			string doktorBrans = CmbBrans.Text.Trim();
			string doktorTc = MskTc.Text.Trim();
			string doktorSifre = TxtSifre.Text.Trim();

			// Eğer herhangi bir alan boşsa kullanıcıya uyarı gösterilir
			if (string.IsNullOrEmpty(doktorAd) || string.IsNullOrEmpty(doktorSoyad) || string.IsNullOrEmpty(doktorBrans) || string.IsNullOrEmpty(doktorTc) || string.IsNullOrEmpty(doktorSifre))
			{
				MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;  // Tüm alanlar dolmadıkça işlem yapılmaz
			}

			// Veritabanı bağlantısını açar ve işlemi gerçekleştirecek sorguyu hazırlar
			using (var conn = db.GetConnection())
			{
				try
				{
					conn.Open();  // Veritabanı bağlantısını açar

					// Doktoru veritabanına ekleyen SQL sorgusu
					string query = "INSERT INTO tbl_doktorlar (DoktorAd, DoktorSoyad, DoktorBrans, DoktorTC, DoktorSifre) VALUES (@d1, @d2, @d3, @d4, @d5)";
					using (var cmd = new NpgsqlCommand(query, conn))
					{
						// Sorguya parametreler eklenir
						cmd.Parameters.AddWithValue("@d1", doktorAd);
						cmd.Parameters.AddWithValue("@d2", doktorSoyad);
						cmd.Parameters.AddWithValue("@d3", doktorBrans);
						cmd.Parameters.AddWithValue("@d4", doktorTc);
						cmd.Parameters.AddWithValue("@d5", doktorSifre);
						// Sorguyu çalıştırarak doktoru veritabanına ekler
						cmd.ExecuteNonQuery();
					}

					// Eklenme işlemi başarılıysa kullanıcıya bilgi mesajı gösterir
					MessageBox.Show("Doktor başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
					// Hata durumunda kullanıcıya hata mesajını gösterir
					MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}


		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
			int secilen = dataGridView1.SelectedCells[0].RowIndex;
			TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
			TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
			CmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
			MskTc.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
			TxtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
		}

		private void BtnSil_Click(object sender, EventArgs e)
		{
			// Kullanıcının girdiği doktor TC numarasını alır ve boşlukları temizler
			string doktorTc = MskTc.Text.Trim();

			// Eğer TC numarası boşsa kullanıcıya uyarı gösterilir
			if (string.IsNullOrEmpty(doktorTc))
			{
				MessageBox.Show("Lütfen TC'yi giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;  // TC girilmeden işlem yapılmaz
			}

			// Veritabanı bağlantısını açar ve işlemi gerçekleştirecek sorguyu hazırlar
			using (var conn = db.GetConnection())
			{
				try
				{
					conn.Open();  // Veritabanı bağlantısını açar

					// Doktoru silen SQL sorgusu
					string query = "DELETE FROM tbl_doktorlar WHERE DoktorTC = @p1";
					using (var cmd = new NpgsqlCommand(query, conn))
					{
						// Doktor TC numarasını parametre olarak ekler
						cmd.Parameters.AddWithValue("@p1", doktorTc);
						// Sorguyu çalıştırarak doktoru veritabanından siler
						cmd.ExecuteNonQuery();
					}

					// Silme işlemi başarılıysa kullanıcıya bilgi mesajı gösterir
					MessageBox.Show("Doktor başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
					// Hata durumunda kullanıcıya hata mesajını gösterir
					MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void BtnGuncelle_Click(object sender, EventArgs e)
        {
			// Kullanıcının girdiği doktor bilgilerini alır
			string doktorAd = TxtAd.Text.Trim();  // Doktor adı alınıp boşluklar temizlenir
			string doktorSoyad = TxtSoyad.Text.Trim();  // Doktor soyadı alınıp boşluklar temizlenir
			string doktorBrans = CmbBrans.Text.Trim();  // Doktor branşı alınıp boşluklar temizlenir
			string doktorTc = MskTc.Text.Trim();  // Doktor TC numarası alınıp boşluklar temizlenir
			string doktorSifre = TxtSifre.Text.Trim();  // Doktor şifresi alınıp boşluklar temizlenir

			// Alanların boş olup olmadığını kontrol eder
			if (string.IsNullOrEmpty(doktorAd) || string.IsNullOrEmpty(doktorSoyad) || string.IsNullOrEmpty(doktorBrans) || string.IsNullOrEmpty(doktorTc) || string.IsNullOrEmpty(doktorSifre))
			{
				// Eğer herhangi bir alan boşsa kullanıcıyı uyarır
				MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;  // İşlem yapılmaz, fonksiyon sonlandırılır
			}

			// Veritabanı bağlantısını açar ve işlemi gerçekleştirecek sorguyu hazırlar
			using (var conn = db.GetConnection())
			{
				try
				{
					conn.Open();  // Veritabanı bağlantısını açar

					// Doktor bilgilerini güncelleyen SQL sorgusu
					string query = "UPDATE tbl_doktorlar SET DoktorAd = @d1, DoktorSoyad = @d2, DoktorBrans = @d3, DoktorSifre = @d5 WHERE DoktorTC = @d4";
					using (var cmd = new NpgsqlCommand(query, conn))
					{
						// Parametreleri sorguya ekler
						cmd.Parameters.AddWithValue("@d1", doktorAd);
						cmd.Parameters.AddWithValue("@d2", doktorSoyad);
						cmd.Parameters.AddWithValue("@d3", doktorBrans);
						cmd.Parameters.AddWithValue("@d4", doktorTc);
						cmd.Parameters.AddWithValue("@d5", doktorSifre);

						// SQL sorgusunu çalıştırır ve veriyi günceller
						cmd.ExecuteNonQuery();
					}

					// Güncelleme başarılı olduğunda kullanıcıyı bilgilendirir
					MessageBox.Show("Doktor başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
					// Hata durumunda kullanıcıya hata mesajını gösterir
					MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

        private void button1_Click(object sender, EventArgs e)
        {
            FrmGirisler frm = new FrmGirisler();
            frm.Show();
            this.Hide();
        }
    }
}
