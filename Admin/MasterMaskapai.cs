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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LearnBromoAirlines1.Admin
{
    public partial class MasterMaskapai : UserControl
    {
        SqlConnection con = Connection.getCon();
        bool isUbah = false;
        private static string Id;

        public MasterMaskapai()
        {
            InitializeComponent();
        }

        private void MasterMaskapai_Load(object sender, EventArgs e)
        {
            GetMasterMaskapai();

            // Button Ubah
            DataGridViewButtonColumn btnUbah = new DataGridViewButtonColumn();
            btnUbah.Name = "Ubah";
            btnUbah.HeaderText = "";
            btnUbah.Text = "Ubah";
            btnUbah.UseColumnTextForButtonValue = true;

            // Button Hapus
            DataGridViewButtonColumn btnHapus = new DataGridViewButtonColumn();
            btnHapus.Name = "Hapus";
            btnHapus.HeaderText = "";
            btnHapus.Text = "Hapus";
            btnHapus.UseColumnTextForButtonValue = true;

            // Add Button
            dataGridView1.Columns.Add(btnUbah);
            dataGridView1.Columns.Add(btnHapus);
        }

        void GetMasterMaskapai()
        {
            Helper.getData("Maskapai", "ORDER BY Nama", dataGridView1);
            dataGridView1.Columns["ID"].Visible = false;
        }

        void isValid(string Nama, string Perusahaan, string Deskripsi, int JumlahKru)
        {
            if (Nama == "") throw new Exception("Harap Masukkan Nama");
            if (Perusahaan == "") throw new Exception("Harap Masukkan Nama Perusahaan");
            if (Deskripsi == "") throw new Exception("Harap Masukkan Deskripsi");
            if (JumlahKru < 1) throw new Exception("Jumlah Kru Tidak Boleh Kurang Dari 1");
        }

        void inputData()
        {
            string nama = textBox1.Text;

            con.Open();

            Helper.checkData("Maskapai", where: "Nama = '" + nama + "' ", msg: "Nama maskapai telah digunakan");

            SqlCommand input = new SqlCommand("insert into Maskapai (Nama, Perusahaan, JumlahKru, Deskripsi) values ('"+ textBox1.Text +"', '"+ textBox2.Text +"', '"+ numericUpDown1.Value +"', '"+ textBox3.Text +"')", con);
            input.ExecuteNonQuery();
            MessageBox.Show("Maskapai berhasil ditambahkan");

            GetMasterMaskapai();

            con.Close();
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

                    textBox1.Text = row.Cells["Nama"].Value.ToString();
                    textBox2.Text = row.Cells["Perusahaan"].Value.ToString();
                    textBox3.Text = row.Cells["Deskripsi"].Value.ToString();
                    numericUpDown1.Value = (int)row.Cells["JumlahKru"].Value;
                }
                else if (e.ColumnIndex == dataGridView1.Columns["Hapus"].Index)
                {
                    DialogResult result = MessageBox.Show("Apakah anda yakin untuk menghapus data ini ?", "Konfirmasi hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        Helper.deleteData("Maskapai", Id);

                        GetMasterMaskapai();
                    }
                }
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                string Nama = textBox1.Text, Perusahaan = textBox2.Text, Deskripsi = textBox3.Text;
                int JumlahKru = (int)numericUpDown1.Value;
                isValid(Nama, Perusahaan, Deskripsi, JumlahKru);

                if (isUbah)
                {
                    Helper.updateData("Maskapai", "Nama = '" + textBox1.Text + "', Perusahaan = '" + textBox2.Text + "', JumlahKru = '" + numericUpDown1.Value + "', Deskripsi = '" + textBox3.Text + "'", Id);
                    GetMasterMaskapai();
                    return;
                }

                inputData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Batal_Click(object sender, EventArgs e)
        {
            isUbah = false;

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            numericUpDown1.Value = 1;
        }
    }
}
