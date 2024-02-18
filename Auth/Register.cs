using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace LearnBromoAirlines1
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=MSI-GF63;Initial Catalog=BromoAirlines;Integrated Security=True;Encrypt=False");
        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string Username = textBox1.Text, Password = textBox4.Text, Nama = textBox2.Text, NomorTelepon = textBox3.Text;
                DateTime TanggalLahir = DateTime.Parse(dateTimePicker1.Text);
                bool role = false;

                if (Username == "") throw new Exception("Masukkan username");
                if (Nama == "") throw new Exception("Masukkan nama");
                if (NomorTelepon == "") throw new Exception("Masukkan nomor telepon");
                if (Password == "") throw new Exception("Masukkan Pasword");

                // pengondisian nomor telpon 10-15 digit
                String noTelponPattern = @"([0-9]){10,15}";
                if (!checkFormValidity(noTelponPattern, NomorTelepon)) throw new Exception("Nomor telepon tidak valid");


                // pengondisian password minimal 8 karakter
                if (Password.Length < 8) { throw new Exception("Password tidak valid"); }


                /*String passwordPattern = @"([a-zA-Z0-9]){8,}";
                if (!checkFormValidity(passwordPattern, Password)) throw new Exception("Password tidak valid");*/


                con.Open();
                // pengondisian username unique
                SqlCommand isUsernameTaken = new SqlCommand("SELECT COUNT(*) FROM Akun WHERE Username = '" + Username + "'", con);
                if ((int)isUsernameTaken.ExecuteScalar() > 0)
                {
                    throw new Exception("Username is taken");
                }

                // all done
                SqlCommand comm = new SqlCommand("exec Register '" + Username + "' '" + Password + "' '" + Nama + "' '" + NomorTelepon + "' '" + TanggalLahir + "' '" + role + "'", con);
                MessageBox.Show("Registrasi berhasil");

                UserForm userform = new UserForm();
                userform.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private Boolean checkFormValidity(String pattern, string input)
        {
            try
            {
                Regex regex = new Regex(pattern);
                Match match = regex.Match(input);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
