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

namespace LearnBromoAirlines1.Dashboard
{
    public partial class StatusPenerbangan : UserControl
    {
        SqlConnection con = Connection.getCon();
        bool isUbah = false;
        public static string Id;

        public StatusPenerbangan()
        {
            InitializeComponent();
        }

        private void StatusPenerbangan_Load(object sender, EventArgs e)
        {
            GetStatusPenerbangan();

            Helper.btnGridView(dataGridView1, "Ubah", "", "Ubah");
            dataGridView1.Columns["ID"].Visible = false;

            Helper.comboBox(comboBox1, "Nama", "StatusPenerbangan");
            Disable();
        }

        void GetStatusPenerbangan()
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT JP.ID, KodePenerbangan, BK.Nama as 'BandaraKeberangkatan', BT.Nama as 'BandaraTujuan', M.Nama as 'Maskapai', TanggalWaktuKeberangkatan, DurasiPenerbangan, HargaPerTiket " +
              "FROM JadwalPenerbangan JP " +
              "JOIN Bandara BK on JP.BandaraKeberangkatanID = BK.ID " +
              "JOIN Bandara BT on JP.BandaraTujuanID = BT.ID " +
              "JOIN Maskapai M on JP.MaskapaiID = M.ID " +
              "ORDER BY TanggalWaktuKeberangkatan DESC", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
        }

        void Disable()
        {
            label1.Visible = false;
            label4.Visible = false;
            comboBox1.Visible = false;
            maskedTextBox1.Visible = false;
            Batal.Visible = false;
            Simpan.Visible = false;
        }

        void Enable()
        {
            label1.Visible = true;
            comboBox1.Visible = true;
            Batal.Visible = true;
            Simpan.Visible = true;
        }

        void EnableMore()
        {
            label1.Visible = true;
            label4.Visible = true;
            comboBox1.Visible = true;
            maskedTextBox1.Visible = true;
            Batal.Visible = true;
            Simpan.Visible = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                Id = row.Cells["ID"].Value.ToString();

                if (e.ColumnIndex == dataGridView1.Columns["Ubah"].Index)
                {
                    isUbah = true;
                    Enable();
                }
            }
        }

        private void Batal_Click(object sender, EventArgs e)
        {
            isUbah = false;
            Disable();
        }

        private void Simpan_Click(object sender, EventArgs e)
        {
            MessageBox.Show(comboBox1.SelectedValue.GetType().ToString());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue.ToString() == "3")
            {
                EnableMore();
            }
        }
    }
}
