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
    public partial class FrmDoktorGiris : Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }

		DatabaseConnection db = new DatabaseConnection();
		
        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
			string doktorTc = MskTC.Text.Trim();
			string doktorSifre = txtSifre.Text.Trim();

			if (string.IsNullOrEmpty(doktorTc) || string.IsNullOrEmpty(doktorSifre))
			{
				MessageBox.Show("Lütfen TC ve şifreyi giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			using (var conn = db.GetConnection())
			{
				try
				{
					conn.Open();
					string query = "SELECT * FROM tbl_doktorlar WHERE DoktorTc = @p1 AND DoktorSifre = @p2";
					using (var cmd = new NpgsqlCommand(query, conn))
					{
						cmd.Parameters.AddWithValue("@p1", doktorTc);
						cmd.Parameters.AddWithValue("@p2", doktorSifre);
						using (var dr = cmd.ExecuteReader())
						{
							if (dr.Read())
							{
								// Başarılı giriş durumunda doktor detay ekranına yönlendir
								FrmDoktorDetay fr = new FrmDoktorDetay();
								fr.TC = doktorTc;
								fr.Show();
								this.Hide();
							}
							else
							{
								MessageBox.Show("Hatalı TC veya Şifre", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
