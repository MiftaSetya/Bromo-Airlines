using LearnBromoAirlines1.MainClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;

namespace LearnBromoAirlines1
{
    internal class Helper
    {
        public static void OpenContent(Panel p, UserControl us)
        {
            us.Dock = DockStyle.Fill;
            p.Controls.Clear();
            p.Controls.Add(us);
            us.BringToFront();
        }

        public static void checkData(string table, string where, string msg)
        {
            SqlConnection con = Connection.getCon();
            con.Open();

            SqlCommand isNamaTaken = new SqlCommand("Select Count(*) from " + table + " where " + where + " ", con);
            if ((int)isNamaTaken.ExecuteScalar() > 0) throw new Exception(msg);

            con.Close();
        }

        public static void getData(string table, string kondisi, DataGridView dataGridView)
        {
            SqlConnection con = Connection.getCon();
            con.Open();

            string query = "SELECT * FROM " + table + " " + kondisi;
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView.DataSource = dt;

            con.Close();
        }

        public static void insertData(string table, string values)
        {
            SqlConnection con = Connection.getCon();
            con.Open();

            SqlCommand cmd = new SqlCommand("insert into " + table + " values " + values, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Insert Data Success");

            con.Close();
        }

        public static void updateData(string table, string set, string id)
        {
            SqlConnection con = Connection.getCon();
            con.Open();

            SqlCommand cmd = new SqlCommand("update " + table + " set " + set + " where id = " + id, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Update Success");

            con.Close();
        }

        public static void deleteData(string table, string id) 
        {
            SqlConnection con = Connection.getCon();
            con.Open();

            SqlCommand cmd = new SqlCommand("delete from " + table + " where id = " + id, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Delete Success");

            con.Close();
        }

        public static void comboBox(ComboBox comboBox, string select, string table)
        {
            SqlConnection con = Connection.getCon();
            con.Open();

            SqlCommand cmd = new SqlCommand("select ID, " + select + " from " + table + "", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            comboBox.DataSource = dt;
            comboBox.DisplayMember = select;
            comboBox.ValueMember = "ID";
            con.Close();
        }

        public static void btnGridView(DataGridView dataGridView, string name, string header, string text)
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = name;
            btn.HeaderText = header;
            btn.Text = text;
            btn.UseColumnTextForButtonValue = true;

            dataGridView.Columns.Add(btn);
        }
    }
}
