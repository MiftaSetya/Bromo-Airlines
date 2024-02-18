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
using LearnBromoAirlines1.Admin;
using LearnBromoAirlines1.Dashboard;

namespace LearnBromoAirlines1
{
    public partial class DashboardAdmin : Form
    {
        public DashboardAdmin()
        {
            InitializeComponent();
        }

        private void AdminMainForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MasterBandara us = new MasterBandara();
            Helper.OpenContent(panelContent, us);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            MasterMaskapai us = new MasterMaskapai();
            Helper.OpenContent(panelContent, us);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            JadwalPenerbangan us = new JadwalPenerbangan();
            Helper.OpenContent(panelContent, us);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            MasterKodePromo us = new MasterKodePromo();
            Helper.OpenContent(panelContent, us);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            StatusPenerbangan us = new StatusPenerbangan();
            Helper.OpenContent(panelContent, us);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
