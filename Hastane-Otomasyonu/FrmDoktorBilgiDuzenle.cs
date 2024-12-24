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
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }
		DatabaseConnection db = new DatabaseConnection();
		public string TCNO;

		private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
		{
			MskTC.Text = TCNO;
			// Doktor Bilgilerini Güncelleyen Kod Bölümü
			using (var conn = db.GetConnection()) // PostgreSQL bağlantısı
			{
				try
				{
					conn.Open();
					using (var cmd = new NpgsqlCommand("SELECT * FROM tbl_doktorlar WHERE DoktorTc = @p1", conn))
					{
						cmd.Parameters.AddWithValue("@p1", MskTC.Text);
						using (var dr = cmd.ExecuteReader())
						{
							while (dr.Read())
							{
								txtAd.Text = dr["DoktorAd"].ToString();
								TxtSoyad.Text = dr["DoktorSoyad"].ToString();
								CmbBrans.Text = dr["DoktorBrans"].ToString();
								TxtSifre.Text = dr["DoktorSifre"].ToString();
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
			// Doktor Bilgilerini Güncelleyen Kod Bölümü
			using (var conn = db.GetConnection()) // PostgreSQL bağlantısı
			{
				try
				{
					conn.Open();
					using (var cmd = new NpgsqlCommand("UPDATE tbl_doktorlar SET DoktorAd = @p1, DoktorSoyad = @p2, DoktorBrans = @p3, DoktorSifre = @p4 WHERE DoktorTC = @p5", conn))
					{
						cmd.Parameters.AddWithValue("@p1", txtAd.Text);
						cmd.Parameters.AddWithValue("@p2", TxtSoyad.Text);
						cmd.Parameters.AddWithValue("@p3", CmbBrans.Text);
						cmd.Parameters.AddWithValue("@p4", TxtSifre.Text);
						cmd.Parameters.AddWithValue("@p5", MskTC.Text);
						cmd.ExecuteNonQuery();
					}

					MessageBox.Show("Güncelleme İşlemi Başarıyla Tamamlandı");
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Güncelleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
