using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LearnBromoAirlines1
{
    public partial class Login : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=MSI-GF63;Initial Catalog=BromoAirlines;Integrated Security=True;Encrypt=False");
        public Login()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            Visible = false;
        }

        private void buttonLogin_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Masukkan Username");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Masukkan Password");
            }
            else
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select * from Akun where Username = @Username and Password = @Password", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("Username", textBox1.Text);
                    cmd.Parameters.AddWithValue("Password", textBox2.Text);
                    SqlDataReader rd = cmd.ExecuteReader();

                    if (rd.HasRows)
                    {
                        MessageBox.Show("Login Berhasil");

                        rd.Read();
                        if ((bool)rd[6])
                        {
                            DashboardAdmin adminForm = new DashboardAdmin();
                            adminForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            UserForm userForm = new UserForm();
                            userForm.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username atau password anda salah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
