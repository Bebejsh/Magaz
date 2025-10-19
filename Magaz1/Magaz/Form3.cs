using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magaz
{

    public partial class Form3 : Form
    {
        private SqlConnection connection;
        private string connections = "server = 192.168.0.89; port = 3306; username =  _dpr2214; password =  _dpr2214;database= _dpr2214_magazzz";

        public static class PPP
        {
            public static int idUserK { get; set; }
        }
        public Form3()
        {
            InitializeComponent();


        }

        private void Form3_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetDataFromMySQL();
        }
        private DataTable GetDataFromMySQL()
        {

            DataTable used = new DataTable();

            try
            {
                int idUserK = PPP.idUserK;

                MySqlConnection connection = new MySqlConnection(connections);
                connection.Open();

                string query = $"SELECT used.idUsed, used.Magaz_idMagaz, magaz.Nazv, magaz.Cena FROM used JOIN magaz ON used.Magaz_idMagaz = magaz.idMagaz WHERE User_idUser = '{idUserK}'";
                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(used);

                // Расчет общей суммы товаров
                decimal totalAmount = 0;
                foreach (DataRow row in used.Rows)
                {
                    totalAmount += Convert.ToDecimal(row["Cena"]); 
                }
                
                textBox1.Text = $"Общая сумма: {totalAmount:C}"; 

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении данных из MySQL: " + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            return used;
        }


        private void button1_Click(object sender, EventArgs e)
        {


            if (dataGridView1.SelectedRows.Count > 0)
            {
              
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                
                int id = Convert.ToInt32(selectedRow.Cells["idUsed"].Value); 

                // Удаление 
                string deleteQuery = "DELETE FROM used WHERE idUsed = @idUsed"; 
                using (MySqlConnection connection = new MySqlConnection("server = 192.168.0.89; port = 3306; username =  _dpr2214; password =  _dpr2214;database= _dpr2214_magazzz"))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@idUsed", id);
                        command.ExecuteNonQuery();
                    }
                }

                // Удаление строки из DataGridView
                dataGridView1.Rows.Remove(selectedRow);
            }
            GetDataFromMySQL();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide(); 
        }
    }
}
