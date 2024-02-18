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

namespace LearnBromoAirlines1.Admin
{
    public partial class JadwalPenerbangan : UserControl
    {
        SqlConnection con = Connection.getCon();

        public JadwalPenerbangan()
        {
            InitializeComponent();
        }

        private void JadwalPenerbangan_Load(object sender, EventArgs e)
        {
            GetJadwalPenerbangan();

            DataGridViewButtonColumn btnUbah = new DataGridViewButtonColumn();
            btnUbah.Name = "Ubah";
            btnUbah.HeaderText = "";
            btnUbah.Text = "Ubah";
            btnUbah.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn btnHapus = new DataGridViewButtonColumn();
            btnHapus.Name = "Ubah";
            btnHapus.HeaderText = "";
            btnHapus.Text = "Ubah";
            btnHapus.UseColumnTextForButtonValue = true;

            dataGridView1.Columns.Add(btnUbah);
            dataGridView1.Columns.Add(btnHapus);
        }

        void GetJadwalPenerbangan()
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT KodePenerbangan, BK.Nama as 'BandaraKeberangkatan', BT.Nama as 'BandaraTujuan', M.Nama as 'Maskapai', TanggalWaktuKeberangkatan, DurasiPenerbangan, HargaPerTiket " +
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

        void isValid( int BandaraKeberangkatanID, int BandaraTujuanID, DateTime TanggalWaktuKeberangkatan, int DurasiPenerbangan, double HargaPerTiket)
        {
            if (BandaraKeberangkatanID == BandaraTujuanID) throw new Exception("Bandara keberangkatan dan bandara tujuan tidak boleh sama");

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
