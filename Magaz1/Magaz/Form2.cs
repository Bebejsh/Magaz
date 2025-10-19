using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Magaz
{
    public partial class Form2 : Form
    {
        private SqlConnection connection;
        private string connections = "server = 192.168.0.89; port = 3306; username =  _dpr2214; password =  _dpr2214;database= _dpr2214_magazzz";

        public int idUser { get; set; }
        public Form2()
        {
            InitializeComponent();
             
        }
        internal void SetUserData(string lastName, string firstName, string patron)
        {
            label1.Text = $"{lastName} {firstName} {patron} ";
            
        }
        private DataTable GetDataFromMySQL()
        {
            DataTable Magaz = new DataTable();

            try
            {
                MySqlConnection connection = new MySqlConnection(connections);
                connection.Open();

                string query = "SELECT * FROM Magaz";
                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(Magaz);

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

            return Magaz;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetDataFromMySQL();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetDataFromMySQL();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int idMagaz = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["idMagaz"].Value);
                int idUserid = GetUserId();

                using (MySqlConnection connection = new MySqlConnection("server = 192.168.0.89; port = 3306; username =  _dpr2214; password =  _dpr2214;database= _dpr2214_magazzz"))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("INSERT INTO used (User_idUser,Magaz_idMagaz) VALUES (@idUserid,@idMagaz)", connection))
                    {
                        command.Parameters.AddWithValue("@idUserid", idUserid);
                        command.Parameters.AddWithValue("@idMagaz", idMagaz);
                        command.ExecuteNonQuery();
                        string updateq = "UPDATE magaz SET Kol = Kol - 1 Where idMagaz=@idMagaz";
                        using (MySqlCommand update = new MySqlCommand(updateq, connection))
                        {
                            update.Parameters.AddWithValue("@idMagaz", idMagaz);
                            update.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                   
                }

                MessageBox.Show("Запись успешно добавлена в таблицу used!");
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку в таблице Magaz где есть книги в наличии.");
            }


            dataGridView1.DataSource = GetDataFromMySQL();

        }
        private int GetUserId()
        {

            int idUserid = idUser;
            
            return idUserid;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        
        
    }
}
