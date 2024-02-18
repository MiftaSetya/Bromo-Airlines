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
    public partial class MasterBandara : UserControl
    {
        SqlConnection con = Connection.getCon();
        bool isUbah = false;
        public static string Id;

        public MasterBandara()
        {
            InitializeComponent();
        }

        private void MasterBandara_Load(object sender, EventArgs e)
        {
            isUbah = false;
            GetMasterBandara();

            Helper.btnGridView(dataGridView1, "Ubah", "", "Ubah");
            Helper.btnGridView(dataGridView1, "Hapus", "", "Hapus");

            Helper.comboBox(comboBox1, "Nama", "Negara");
        }

        void GetMasterBandara()
        {
            con.Open();

            string query = "select B.ID, B.Nama, KodeIATA, Kota, N.Nama as 'Negara', JumlahTerminal, Alamat from Bandara B left join Negara N on NegaraID = N.ID";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

            dataGridView1.Columns["ID"].Visible = false;

            con.Close();
        }

        public void isValid(string Nama, string Kode, string Kota, string Alamat, int JumlahTerminal)
        {
            if (Nama == "") throw new Exception("Masukkan nama");
            if (Kode == "") throw new Exception("Masukkan kode IATA");
            if (Kota == "") throw new Exception("Masukkan kota");
            if (Alamat == "") throw new Exception("Masukkan alamat");
            if (JumlahTerminal < 1) throw new Exception("Jumlah terminal tidak boleh kurang dari 1");
            if (Kode.Length < 3) throw new Exception("Kode IATA harus berupa 3 karakter");
        }

        public void InputData(string Nama, string Kode)
        {
            con.Open();

            // Check Data
            Helper.checkData("Bandara", where: "Nama = '"+ Nama +"'", msg: "Nama bandara telah digunakan");
            Helper.checkData("Bandara", where: "KodeIATA = '"+ Kode +"'", msg: "Kode bandara telah digunakan");

            // Input data
            string queryInput = "INSERT INTO Bandara (Nama, KodeIATA, Kota, NegaraID, JumlahTerminal, Alamat) VALUES ('"+ textBox1.Text +"', '"+ textBox2.Text +"', '"+ textBox3.Text +"', '"+ comboBox1.SelectedValue + "', '"+ numericUpDown1.Value +"', '"+ textBox5.Text +"')";
            SqlCommand input = new SqlCommand(queryInput, con);

            input.ExecuteNonQuery();
            if (input.ExecuteNonQuery() > 0) throw new Exception("Data berhasil ditambahkan");

            GetMasterBandara();

            con.Close();
        }

        public void UpdateData(string Nama, string Kode)
        {
            con.Open();

            // Check Data
            Helper.checkData("Bandara", where: "Nama = '" + Nama + "'", msg: "Nama bandara telah digunakan");
            Helper.checkData("Bandara", where: "KodeIATA = '" + Kode + "'", msg: "Kode bandara telah digunakan");

            //Update Data
            string setQuery = "Nama = '" + textBox1.Text + "', KodeIATA = '" + textBox2.Text + "', Kota = '" + textBox3.Text + "', NegaraID = '" + comboBox1.SelectedValue + "', JumlahTerminal = '" + numericUpDown1.Value + "', Alamat = '" + textBox5.Text + "'";
            Helper.updateData("Bandara", setQuery, Id);

            GetMasterBandara();

            con.Close();
        }

        void Delete()
        {
            Helper.deleteData("Bandara", Id);
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
                    textBox2.Text = row.Cells["KodeIATA"].Value.ToString();
                    textBox3.Text = row.Cells["Nama"].Value.ToString();
                    textBox5.Text = row.Cells["Alamat"].Value.ToString();
                    comboBox1.Text = row.Cells["Negara"].Value.ToString();
                    numericUpDown1.Value = (int)row.Cells["JumlahTerminal"].Value;
                }
                else if (e.ColumnIndex == dataGridView1.Columns["Hapus"].Index)
                {
                    DialogResult result = MessageBox.Show("Apakah anda yakin untuk menghapus data ini ?", "Konfirmasi hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        Delete();

                        GetMasterBandara();
                    }
                }
            }
        }

        private void simpan_Click(object sender, EventArgs e)
        {
            try
            {
                string Nama = textBox1.Text, Kode = textBox2.Text, Kota = textBox3.Text, Negara = comboBox1.Text, Alamat = textBox5.Text;
                int JumlahTerminal = (int)numericUpDown1.Value;
                isValid(Nama, Kode, Kota, Alamat, JumlahTerminal);

                if(isUbah)
                {
                    UpdateData(Nama, Kode);
                    return;
                }

                InputData(Nama, Kode);

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

        private void Batal_Click(object sender, EventArgs e)
        {
            isUbah = false;

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox5.Clear();
            comboBox1.Text = string.Empty;
            numericUpDown1.Value = 1;
        }
    }
}