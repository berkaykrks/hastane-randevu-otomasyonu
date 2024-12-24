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
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }

        
        private void LnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayit fr = new FrmHastaKayit();
            fr.Show();
        }
		DatabaseConnection db = new DatabaseConnection();

		private void BtnGirisYap_Click(object sender, EventArgs e)
		{
			using (var conn = db.GetConnection()) // PostgreSQL bağlantısı
			{
				try
				{
					conn.Open();
					using (var komut = new NpgsqlCommand("SELECT * FROM tbl_hastalar WHERE HastaTc=@p1 AND HastaSifre=@p2", conn))
					{
						komut.Parameters.AddWithValue("@p1", MskTC.Text);
						komut.Parameters.AddWithValue("@p2", txtSifre.Text);

						using (var dr = komut.ExecuteReader())
						{
							if (dr.Read())
							{
								FrmHastaDetay fr = new FrmHastaDetay();
								fr.tc = MskTC.Text;
								fr.Show();
								this.Hide();
							}
							else
							{
								MessageBox.Show("Hatalı Kullanıcı Adı ve Şifre.");
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


		private void button1_Click(object sender, EventArgs e)
        {
            FrmGirisler frm = new FrmGirisler();
            frm.Show();
            this.Hide();
        }
    }
}
