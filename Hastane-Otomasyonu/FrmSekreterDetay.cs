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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

		DatabaseConnection db = new DatabaseConnection();
		public string TcNo;

		private void FrmSekreterDetay_Load(object sender, EventArgs e)
		{
			LblTc.Text = TcNo;

			// Sekreter Detay Sayfasına Ad Soyad Getiren Kod bölümü
			using (var conn = db.GetConnection()) // PostgreSQL bağlantısı
			{
				try
				{
					conn.Open();
					using (var komut = new NpgsqlCommand("SELECT SekreterAdSoyad FROM tbl_sekreter WHERE SekreterTc=@p1", conn))
					{
						komut.Parameters.AddWithValue("@p1", LblTc.Text);
						using (var dr1 = komut.ExecuteReader())
						{
							while (dr1.Read())
							{
								LblAdSoyad.Text = dr1[0].ToString();
							}
						}
					}

					// Branş Listesini Data View'de Gösteren Kod bölümü
					DataTable dt1 = new DataTable();
					using (var da = new NpgsqlDataAdapter("SELECT * FROM tbl_branslar", conn))
					{
						da.Fill(dt1);
					}
					dataGridView1.DataSource = dt1;

					// Doktorları Data View'de Gösteren Kod Bölümü
					DataTable dt2 = new DataTable();
					using (var da1 = new NpgsqlDataAdapter("SELECT (DoktorAd || ' ' || DoktorSoyad) AS \"Doktorlar\", DoktorBrans FROM tbl_doktorlar", conn))
					{
						da1.Fill(dt2);
					}
					dataGridView2.DataSource = dt2;

					// Doktorlara Ait Branşı CheckBox'a Getiren Kod Bölümü
					using (var komut2 = new NpgsqlCommand("SELECT BransAd FROM tbl_branslar", conn))
					using (var dr2 = komut2.ExecuteReader())
					{
						while (dr2.Read())
						{
							CmbBrans.Items.Add(dr2[0]);
						}
					}
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

		private void BtnKaydet_Click(object sender, EventArgs e)
		{
			// Randevu Oluşturma Kod Bölümü
			using (var conn = db.GetConnection()) // PostgreSQL bağlantısı
			{
				try
				{
					conn.Open();
					using (var komutkaydet = new NpgsqlCommand("INSERT INTO tbl_randevular (RandevuTarih, RandevuSaat, RandevuBrans, RandevuDoktor, HastaTc) VALUES (@r1, @r2, @r3, @r4, @r5)", conn))
					{
						komutkaydet.Parameters.AddWithValue("@r1", MskTarih.Text);
						komutkaydet.Parameters.AddWithValue("@r2", MskSaat.Text);
						komutkaydet.Parameters.AddWithValue("@r3", CmbBrans.Text);
						komutkaydet.Parameters.AddWithValue("@r4", CmbDoktor.Text);
						komutkaydet.Parameters.AddWithValue("@r5", MskTc.Text);
						komutkaydet.ExecuteNonQuery();
					}
					MessageBox.Show("Randevu Oluşturuldu");
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Branşa Tıklandığında ilgili doktorun gelmesini gösterecek Kod Bölümü
			CmbDoktor.Items.Clear();
			using (var conn = db.GetConnection()) // PostgreSQL bağlantısı
			{
				try
				{
					conn.Open();
					using (var komut = new NpgsqlCommand("SELECT DoktorAd, DoktorSoyad FROM tbl_doktorlar WHERE DoktorBrans=@p1", conn))
					{
						komut.Parameters.AddWithValue("@p1", CmbBrans.Text);
						using (var dr = komut.ExecuteReader())
						{
							while (dr.Read())
							{
								CmbDoktor.Items.Add(dr[0] + " " + dr[1]);
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
					conn.Close();
				}
			}
		}

		private void BtnDuyuOluştur_Click(object sender, EventArgs e)
		{
			// Duyuruları Oluşturan Kod Bölümü
			using (var conn = db.GetConnection()) // PostgreSQL bağlantısı
			{
				try
				{
					conn.Open();
					using (var komut = new NpgsqlCommand("INSERT INTO tbl_duyurular (duyuru) VALUES (@d1)", conn))
					{
						komut.Parameters.AddWithValue("@d1", richTextBox1.Text);
						komut.ExecuteNonQuery();
					}
					MessageBox.Show("Duyuru Oluşturuldu");
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


		private void BtnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli drp = new FrmDoktorPaneli();
            drp.Show();
        }

        private void BtnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBrans frmBrans = new FrmBrans();
            frmBrans.Show();
        }

        private void BtnRandevuListele_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frl = new FrmRandevuListesi();
            frl.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular frmd = new FrmDuyurular();
            frmd.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmGirisler frm = new FrmGirisler();
            frm.Show();
            this.Hide();
        }
    }
}
