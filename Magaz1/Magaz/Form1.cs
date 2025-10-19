using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Magaz
{
    public partial class Form1 : Form
    {
        private SqlConnection connection;
        private string connections = "server = 192.168.0.89; port = 3306; username =  _dpr2214; password =  _dpr2214;database= _dpr2214_magazzz";
        public Form1()
        {
            InitializeComponent();

            //Form2 form2 = new Form2();
            //form2.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(connections);
            connection.Open();
            string cmd = "SELECT COUNT(*) FROM user WHERE Login = @Login AND Password = @Password";
            MySqlCommand Command = new MySqlCommand(cmd, connection);
            using (Command)
            {
                Command.Parameters.AddWithValue("@Login", textBox1.Text);
                Command.Parameters.AddWithValue("@Password", textBox2.Text);

                int count = Convert.ToInt32(Command.ExecuteScalar());

                

                if (count > 0)
                {
                    Form2 form2 = new Form2();
                    Form3 form3 = new Form3();


                    string query = "SELECT idUser, Name, Surname, Otchestvo FROM user WHERE Login = @Login";
                    MySqlCommand getUserCommand = new MySqlCommand(query, connection);
                    getUserCommand.Parameters.AddWithValue("@Login", textBox1.Text);

                    using (MySqlDataReader reader = getUserCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Получаем значения из базы данных
                            string lastName = reader.GetString("Name");
                            string firstName = reader.GetString("Surname");
                            string patron = reader.GetString("Otchestvo");
                            

                            // Передаем значения на Form2
                            form2.SetUserData(lastName, firstName, patron);
                            form2.idUser = Convert.ToInt32(reader["idUser"]);
                            int id = reader.GetInt32("idUser");
                            Form3.PPP.idUserK = id;
                        }
                    }
                    this.Hide();
                    form2.Show();
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль");
                }
            }
            connection.Close();
        }

    }
}