using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PRAKTIKUM1_DESKTOP_XII_242507005_AzrillRizkiansyah
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }
        string konfigurasi = "server=localhost;username=root;password=;database=db_apotek_petugas";
        private void btnLogin_Click(object sender, EventArgs e)
        {
            MySqlConnection koneksi = new MySqlConnection(konfigurasi);
            try
            {
                string username = txtUsername.Text;
                string password = txtPassword.Text;
               
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Username dan Password tidak boleh kosong!");
                    txtPassword.Focus();
                    return;
                }
                koneksi.Open();
                string query = "select username,password,status_aktif from tb_apoteker WHERE username = @username limit 1";

                MySqlCommand cmd = new MySqlCommand(query, koneksi);
                cmd.Parameters.AddWithValue("@username", username);
                MySqlDataReader hasil = cmd.ExecuteReader();
                if (hasil.Read())
                {       
                    if (password == hasil["password"].ToString())
                    {
                        FormDashboard formDashboard = new FormDashboard();
                        formDashboard.FormClosed += FormDashboard_FormClosed;
                        formDashboard.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Password salah!");
                    }
                }
                else
                {
                    MessageBox.Show("Username tidak ditemukan!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private void FormDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}
