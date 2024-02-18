using LearnBromoAirlines1.MainClass;
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

namespace LearnBromoAirlines1.Cust
{
    public partial class CustomerMainForm : Form
    {
        SqlConnection con = Connection.getCon();
        public CustomerMainForm()
        {
            InitializeComponent();
        }

        public void SetNamaCust(string nama)
        {
            labelNama.Text = nama + "?";
        }

        private void CustomerMainForm_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Nama, Kota, KodeIATA from Bandara", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();

            while (dr.Read())
            {
                coll.Add(dr["Nama"].ToString() + ", " + dr["Kota"].ToString() + " ("+ dr["KodeIATA"].ToString() +")" );
            }

            textBox1.AutoCompleteCustomSource = coll;
            textBox2.AutoCompleteCustomSource = coll;
            dr.Close();
            con.Close();
        }
    }
}
