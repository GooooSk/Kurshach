using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kurshach
{
    public partial class Autho : Form
    {
        public Autho()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Registration Auth = new Registration();
            Auth.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String comm = "Select id_user, login, password, role, surname, name, email FROM K_User WHERE login='" + textBox1.Text + "'AND password='" + textBox2.Text + "';";

            SqlConnection sqlConnection = new SqlConnection(Dannie.connectionString);
            SqlCommand sqlCommand = new SqlCommand(comm, sqlConnection);
            int id = 0;
            string log = "";
            string pass = "";
            int role = 0;
            string surname = "";
            string name = "";
            string email = "";
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                id = sqlDataReader.GetInt32(0);
                log = sqlDataReader.GetString(1);
                pass = sqlDataReader.GetString(2);
                role = sqlDataReader.GetInt32(3);
                surname = sqlDataReader.GetString(4);
                name = sqlDataReader.GetString(5);
                email = sqlDataReader.GetString(6);
            }
            sqlConnection.Close();
            if ((log == textBox1.Text) && (pass == textBox2.Text) && (role == 1))
            {
                MessageBox.Show("Вы успешно вошли под Администратором");
                Dannie.Id = id;
                Dannie.FirstName = name;
                Dannie.LastName = surname;
                Dannie.login = log;
                Dannie.email = email;
                Dannie.role = role;
                Form Admin = new Admin();
                Admin.Show();
                this.Hide();
            }
            else if ((log == textBox1.Text) && (pass == textBox2.Text) && (role == 2))
            {
                MessageBox.Show("Вы успешно вошли под Тренером");
                Dannie.Id = id;
                Dannie.FirstName = name;
                Dannie.LastName = surname;
                Dannie.login = log;
                Dannie.email = email;
                Dannie.role = role;
                Form Coach = new Coach();
                Coach.Show();
                this.Hide();
            }
            else if ((log == textBox1.Text) && (pass == textBox2.Text) && (role == 3))
            {
                MessageBox.Show("Вы успешно вошли под Участником");
                Dannie.Id = id;
                Dannie.FirstName = name;
                Dannie.LastName = surname;
                Dannie.login = log;
                Dannie.email = email;
                Dannie.role = role;
                Form User = new User();
                User.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Данные неверные");
            }
        }
    }
}
