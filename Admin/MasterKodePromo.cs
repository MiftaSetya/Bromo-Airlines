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
    public partial class MasterKodePromo : UserControl
    {
        SqlConnection con = Connection.getCon();
        bool isUbah = false;
        private static string Id;

        public MasterKodePromo()
        {
            InitializeComponent();
        }

        private void MasterKodePromo_Load(object sender, EventArgs e)
        {
            GetKodePromo();

            Helper.btnGridView(dataGridView1, "Ubah", "", "Ubah");
            Helper.btnGridView(dataGridView1, "Hapus", "", "Hapus");
        }

        private void GetKodePromo()
        {
            Helper.getData("KodePromo", "", dataGridView1);
            dataGridView1.Columns["ID"].Visible = false;
        }

        void inputData()
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("insert into KodePromo (Kode, PersentaseDiskon, MaksimumDiskon, BerlakuSampai, Deskripsi) values ('" + textBox1.Text + "', '" + numericUpDown1.Value + "', '" + numericUpDown2.Value + "', '" + dateTimePicker1.Text + "', '" + textBox2.Text + "')", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Input Success");

            GetKodePromo();

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

                    textBox1.Text = row.Cells["Kode"].Value.ToString();
                    textBox2.Text = row.Cells["Deskripsi"].Value.ToString();
                    numericUpDown1.Value = (decimal)((double)row.Cells["PersentaseDiskon"].Value);
                    numericUpDown2.Value = (decimal)((double)row.Cells["MaksimumDiskon"].Value);
                    dateTimePicker1.Value = (DateTime)row.Cells["BerlakuSampai"].Value;
                }
                else if (e.ColumnIndex == dataGridView1.Columns["Hapus"].Index)
                {
                    DialogResult result = MessageBox.Show("Apakah anda yakin untuk menghapus data ini ?", "Konfirmasi hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        Helper.deleteData("KodePromo", Id);

                        GetKodePromo();
                    }
                }
            }
        }

        private void Simpan_Click(object sender, EventArgs e)
        {
            try
            {
                string Kode = textBox1.Text, Deskripsi = textBox2.Text;
                float PersenDiskon = (float)numericUpDown1.Value, MaksDiskon = (float)numericUpDown2.Value;
                DateTime BerlakuSampai = DateTime.Parse(dateTimePicker1.Text);

                if (isUbah)
                {
                    Helper.updateData("KodePromo", "Kode = '" + textBox1.Text + "', PersentaseDiskon = '" + numericUpDown1.Value + "', MaksimumDiskon = '" + numericUpDown2.Value + "', BerlakuSampai = '" + dateTimePicker1.Text + "', Deskripsi = '" + textBox2.Text + "'", Id);
                    GetKodePromo();
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
            numericUpDown1.Value = 1;
            numericUpDown2.Value = 1;
            dateTimePicker1.Value = DateTime.Now;
        }
    }
}
