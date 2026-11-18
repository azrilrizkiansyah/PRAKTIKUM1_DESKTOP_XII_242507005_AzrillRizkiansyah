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
    public partial class FormRegister : Form
    {
        public FormRegister()
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
                string konfirmasi_password = txtKonfimasi_Password.Text;
                string role = comboBox1.Text;
                koneksi.Open();
                string query = "insert into tb_apoteker(username,password,role)values (@username,@password,role)";
                MySqlCommand cmd = new MySqlCommand(query, koneksi);
                cmd.Parameters.AddWithValue("@username",username);
                cmd.Parameters.AddWithValue("@password",password);
                cmd.Parameters.AddWithValue("@role",role);
                if (string.IsNullOrEmpty(username) | string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("username dan password tidak boleh kosonng");
                }
                else
                {
                    if (username != "username")
                    {
                        FormLogin kembali = new FormLogin();
                        kembali.FormClosed += Kembali_FormClosed;
                        kembali.Show();
                        this.Hide();
                    }
                    cmd.ExecuteNonQuery();
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("terjadi kesalahan" + ex.Message);
            }
        }

        private void Kembali_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            txtUsername.Focus();
        }
    }
}
