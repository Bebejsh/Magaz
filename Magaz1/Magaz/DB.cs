using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Magaz
{
    internal class DB
    {
        MySqlConnection con = new MySqlConnection("server=192.168.0.89;port=3306;username= _npr2214;password= _npr2214;database= _npr2214_511");
        public void openConnection()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
        }


        public void closeConnection()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();

            }

        }
        public MySqlConnection GetConnection()
        {
            return con;
        }
    }
}
