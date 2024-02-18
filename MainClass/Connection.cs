using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnBromoAirlines1.MainClass
{
    internal class Connection
    {
        public static SqlConnection getCon()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=MSI-GF63;Initial Catalog=BromoAirlines;Integrated Security=True;Encrypt=False";
            return con;
        }
    }
}
