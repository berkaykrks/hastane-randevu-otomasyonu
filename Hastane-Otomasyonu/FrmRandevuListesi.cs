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
    public partial class FrmRandevuListesi : Form
    {
        public FrmRandevuListesi()
        {
            InitializeComponent();
        }

		DatabaseConnection db = new DatabaseConnection();
		private void FrmRandevuListesi_Load(object sender, EventArgs e)
		{
			// Formun Açılışında Randevuları Data Grid View'e Aktaran Kod Bölümü
			DataTable dt = new DataTable();
			using (var conn = db.GetConnection()) // PostgreSQL bağlantısı
			{
				try
				{
					conn.Open();
					string query = "SELECT * FROM tbl_randevular";
					using (var da = new NpgsqlDataAdapter(query, conn))
					{
						da.Fill(dt);
					}
					dataGridView1.DataSource = dt;
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

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
	}
}
