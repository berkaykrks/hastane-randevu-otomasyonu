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
    public partial class FrmBrans : Form
    {
        public FrmBrans()
        {
            InitializeComponent();
        }

		DatabaseConnection db = new DatabaseConnection();

		private void FrmBrans_Load(object sender, EventArgs e)
        {
			// Branşların DataGridView'e yüklenmesi sağlanıyor
			DataTable dt = new DataTable();
			using (var conn = db.GetConnection())
			{
				try
				{
					conn.Open();
					string query = "SELECT * FROM tbl_branslar";
					using (var da = new NpgsqlDataAdapter(query, conn))
					{
						da.Fill(dt);
					}

					// DataGridView'e verilerin yüklenmesi
					dataGridView1.DataSource = dt;
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Veri yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

        private void BtnEkle_Click(object sender, EventArgs e)
        {
			string bransAd = TxtBrans.Text.Trim();

			if (string.IsNullOrEmpty(bransAd))
			{
				MessageBox.Show("Lütfen branş adını giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			using (var conn = db.GetConnection())
			{
				try
				{
					conn.Open();
					string query = "INSERT INTO tbl_branslar (BransAd) VALUES (@BransAd)";
					using (var cmd = new NpgsqlCommand(query, conn))
					{
						cmd.Parameters.AddWithValue("@BransAd", bransAd);
						cmd.ExecuteNonQuery();
					}

					MessageBox.Show("Branş başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Data Grid View'e Bir Kez Tıklanınca Verilerin Text'Boxa Gelmesini Sağlayan Kod Bölümü
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtBrans.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
			string bransId = TxtId.Text.Trim();

			if (string.IsNullOrEmpty(bransId))
			{
				MessageBox.Show("Lütfen silmek istediğiniz branş ID'sini giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			using (var conn = db.GetConnection())
			{
				try
				{
					conn.Open();
					string query = "DELETE FROM tbl_branslar WHERE BransId = @BransId";
					using (var cmd = new NpgsqlCommand(query, conn))
					{
						cmd.Parameters.AddWithValue("@BransId", bransId);
						cmd.ExecuteNonQuery();
					}

					MessageBox.Show("Branş başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void BtnGuncelle_Click(object sender, EventArgs e)
		{
			string bransAd = TxtBrans.Text.Trim();
			if (string.IsNullOrEmpty(TxtId.Text) || string.IsNullOrEmpty(bransAd))
			{
				MessageBox.Show("Lütfen branş ID'sini ve branş adını giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// BransId'nin integer olduğunu varsayıyoruz.
			if (!int.TryParse(TxtId.Text, out int bransId))
			{
				MessageBox.Show("Geçerli bir branş ID'si giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			using (var conn = db.GetConnection())
			{
				try
				{
					conn.Open();
					string query = "UPDATE tbl_branslar SET BransAd = @BransAd WHERE BransId = @BransId";
					using (var cmd = new NpgsqlCommand(query, conn))
					{
						cmd.Parameters.AddWithValue("@BransAd", bransAd);
						cmd.Parameters.AddWithValue("@BransId", bransId); // Doğru türde (int) gönderiliyor.
						cmd.ExecuteNonQuery();
					}

					MessageBox.Show("Branş başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
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
