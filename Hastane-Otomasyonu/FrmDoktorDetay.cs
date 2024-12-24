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
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }

		DatabaseConnection db = new DatabaseConnection();
		public string TC;
        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
			LblTc.Text = TC;

			// PostgreSQL sorgusu için NpgsqlCommand kullanıyoruz
			using (var conn = db.GetConnection()) // PostgreSQL bağlantısı
			{
				try
				{
					conn.Open();
					string query = "SELECT DoktorAd, DoktorSoyad FROM tbl_doktorlar WHERE DoktorTc = @p1";
					using (var cmd = new NpgsqlCommand(query, conn))
					{
						cmd.Parameters.AddWithValue("@p1", LblTc.Text);

						// Veriyi okumak için NpgsqlDataReader kullanıyoruz
						using (var dr = cmd.ExecuteReader())
						{
							if (dr.Read())
							{
								// Doktor adı ve soyadını formda gösteriyoruz
								LblAdSoyad.Text = dr["DoktorAd"] + " " + dr["DoktorSoyad"];
							}
						}
					}

					// Doktora ait randevuları getiren sorgu
					DataTable dt = new DataTable();
					string randevuQuery = "SELECT * FROM tbl_randevular WHERE RandevuDoktor = @DoktorAdSoyad";
					using (var cmd2 = new NpgsqlCommand(randevuQuery, conn))
					{
						cmd2.Parameters.AddWithValue("@DoktorAdSoyad", LblAdSoyad.Text);
						using (var da = new NpgsqlDataAdapter(cmd2))
						{
							da.Fill(dt);
							dataGridView1.DataSource = dt;
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDuzenle frm = new FrmDoktorBilgiDuzenle();
            frm.TCNO = LblTc.Text;
            frm.Show();
        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr = new FrmDuyurular();
            fr.Show();
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Randevu Listesne Sol Tıklayınca O Hücredeki Hastanın Şikayetini Görebilecek
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            RchSikayet.Text=dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmGirisler frm = new FrmGirisler();
            frm.Show();
            this.Hide();
        }
    }
}
