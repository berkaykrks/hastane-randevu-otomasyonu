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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;
		DatabaseConnection db = new DatabaseConnection();
		private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
			LblTc.Text = tc;

			// Ad Soyad Verilerini Çeken Kod Bölümü
			using (var conn = db.GetConnection()) // PostgreSQL bağlantısı
			{
				try
				{
					conn.Open();
					string query = "SELECT hastaad, hastasoyad FROM tbl_hastalar WHERE hastatc = @p1";
					using (var cmd = new NpgsqlCommand(query, conn))
					{
						cmd.Parameters.AddWithValue("@p1", LblTc.Text);

						using (var dr = cmd.ExecuteReader())
						{
							if (dr.Read())
							{
								LblAdSoyad.Text = dr["hastaad"] + " " + dr["hastasoyad"];
							}
						}
					}

					// Randevu Geçmişini Çeken Kod Bölümü
					DataTable dt = new DataTable();
					string randevuQuery = "SELECT * FROM tbl_randevular WHERE HastaTc = @tc";
					using (var cmd2 = new NpgsqlCommand(randevuQuery, conn))
					{
						cmd2.Parameters.AddWithValue("@tc", tc);
						using (var da = new NpgsqlDataAdapter(cmd2))
						{
							da.Fill(dt);
							dataGridView1.DataSource = dt;
						}
					}

					// Branşların Verilerini Çeken Kod Bölümü
					string bransQuery = "SELECT BransAd FROM tbl_branslar";
					using (var cmd3 = new NpgsqlCommand(bransQuery, conn))
					{
						using (var dr2 = cmd3.ExecuteReader())
						{
							while (dr2.Read())
							{
								CmbBrans.Items.Add(dr2["BransAd"]);
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally
				{
					conn.Close(); // Bağlantıyı kapatma
				}
			}
		}

		private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Branş ile Eşleşen Doktorların Gelmesini Sağlayan Kod Bölümü
			CmbDoktor.Items.Clear();

			using (var conn = db.GetConnection()) // PostgreSQL bağlantısı
			{
				try
				{
					conn.Open();
					string query = "SELECT DoktorAd, DoktorSoyad FROM tbl_doktorlar WHERE doktorbrans = @p1";
					using (var cmd = new NpgsqlCommand(query, conn))
					{
						cmd.Parameters.AddWithValue("@p1", CmbBrans.Text);
						using (var dr = cmd.ExecuteReader())
						{
							while (dr.Read())
							{
								CmbDoktor.Items.Add(dr["DoktorAd"] + " " + dr["DoktorSoyad"]);
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally
				{
					conn.Close(); // Bağlantıyı kapat
				}
			}
		}


		private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Doktorun Randevularını Getiren Kod Bölümü
			DataTable dt = new DataTable();
			using (var conn = db.GetConnection()) // PostgreSQL bağlantısı
			{
				try
				{
					conn.Open();
					string query = "SELECT * FROM tbl_randevular WHERE RandevuBrans = @p1 AND randevudoktor = @p2 AND RandevuDurum = FALSE";
					using (var da = new NpgsqlDataAdapter(query, conn))
					{
						da.SelectCommand.Parameters.AddWithValue("@p1", CmbBrans.Text);
						da.SelectCommand.Parameters.AddWithValue("@p2", CmbDoktor.Text);
						da.Fill(dt);
					}
					dataGridView2.DataSource = dt;
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally
				{
					conn.Close();
				}
			}
		}

		private void LnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			// Bilgileri Düzenleme Kod Bölümü
			FrmBilgiDuzenle fr = new FrmBilgiDuzenle();
			fr.Tcno = LblTc.Text;
			fr.Show();
		}

		private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			int secilen = e.RowIndex; // Hangi satırın seçildiğini alıyoruz
			if (dataGridView2.Rows[secilen].Cells[0].Value != null)
			{
				Txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
			}
			else
			{
				Txtid.Text = string.Empty;
			}
		}

		private void BtnRandevuAl_Click(object sender, EventArgs e)
		{
			// Randevuyu Almaya Yarayan Kod Bölümü
			using (var conn = db.GetConnection()) // PostgreSQL bağlantısı
			{
				try
				{
					conn.Open();
					string query = "UPDATE tbl_randevular SET RandevuDurum = TRUE, HastaTc = @p1, HastaSikayet = @p2 WHERE randevuid = @p3";
					using (var cmd = new NpgsqlCommand(query, conn))
					{
						cmd.Parameters.AddWithValue("@p1", LblTc.Text);
						cmd.Parameters.AddWithValue("@p2", RchSikayet.Text);
						// Txtid.Text'i integer'a dönüştürüyoruz
						if (int.TryParse(Txtid.Text, out int randevuId))
						{
							cmd.Parameters.AddWithValue("@p3", randevuId);
						}
						else
						{
							throw new ArgumentException("Randevu ID geçerli bir sayı değil.");
						}
						cmd.ExecuteNonQuery();
					}
					MessageBox.Show("Randevu Alındı");
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally
				{
					conn.Close();
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
