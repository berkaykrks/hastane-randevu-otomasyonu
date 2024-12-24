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
    public partial class FrmDuyurular : Form
    {
        public FrmDuyurular()
        {
            InitializeComponent();
        }

		DatabaseConnection db = new DatabaseConnection();
		private void Duyurular_Load(object sender, EventArgs e)
		{
			// Duyuruları DataGridView'e Getiren Kod Bölümü
			DataTable dt = new DataTable();

			// PostgreSQL bağlantısı için NpgsqlDataAdapter kullanımı
			using (var conn = db.GetConnection()) // PostgreSQL bağlantısı
			{
				try
				{
					conn.Open();
					using (var da = new NpgsqlDataAdapter("SELECT * FROM tbl_duyurular", conn))
					{
						da.Fill(dt);
					}

					dataGridView1.DataSource = dt;
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Veri çekme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}


		private void button1_Click(object sender, EventArgs e)
        {
            FrmGirisler frm = new FrmGirisler();
            frm.Show();
            this.Hide();
        }

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
	}
}
