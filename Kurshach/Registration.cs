using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions; // Для регулярных выражений
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kurshach
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
            maskedTextBox1.Mask = "+7 (000) 000-00-00";

        }

        private void label7_Click(object sender, EventArgs e)
        {
            Autho Auth = new Autho();
            Auth.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Проверка на пустые поля
            if (textBox1.Text == "")
            {
                MessageBox.Show("Вы не ввели логин");
                return;
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Вы не ввели пароль");
                return;
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Вы не ввели фамилию");
                return;
            }
            else if (textBox4.Text == "")
            {
                MessageBox.Show("Вы не ввели имя");
                return;
            }
            else if (textBox5.Text == "")
            {
                MessageBox.Show("Вы не ввели email");
                return;
            }
            else if (maskedTextBox1.Text == "")
            {
                MessageBox.Show("Вы не ввели телефон");
                return;
            }

            // Проверка email
            if (!IsValidEmail(textBox5.Text))
            {
                MessageBox.Show("Некорректный email");
                return;
            }

            // Проверка даты рождения
            if (dateTimePicker1.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Дата рождения не может быть в будущем");
                return;
            }

            try
            {
                // Подключение к базе данных и добавление пользователя
                using (SqlConnection sqlConnection = new SqlConnection(Dannie.connectionString))
                {
                    sqlConnection.Open();

                    // Роль для участника
                    int role = 3; // Предполагаем, что 1 — это ID роли участника

                    // Запрос на добавление пользователя
                    string queryInsertUser = "INSERT INTO K_User (login, password, surname, name, email, phone, Birthday, role) " +
                                             "VALUES (@login, @password, @surname, @name, @email, @phone, @Birthday, @role);" +
                                             "SELECT SCOPE_IDENTITY();"; // Возвращаем ID нового пользователя

                    using (SqlCommand sqlCommand = new SqlCommand(queryInsertUser, sqlConnection))
                    {
                        // Привязка значений из текстовых полей
                        sqlCommand.Parameters.AddWithValue("@login", textBox1.Text);
                        sqlCommand.Parameters.AddWithValue("@password", textBox2.Text);
                        sqlCommand.Parameters.AddWithValue("@surname", textBox3.Text);
                        sqlCommand.Parameters.AddWithValue("@name", textBox4.Text);
                        sqlCommand.Parameters.AddWithValue("@email", textBox5.Text);
                        sqlCommand.Parameters.AddWithValue("@phone", maskedTextBox1.Text);
                        sqlCommand.Parameters.AddWithValue("@Birthday", dateTimePicker1.Value);
                        sqlCommand.Parameters.AddWithValue("@role", role);

                        // Выполнение запроса и получение ID нового пользователя
                        int newUserId = Convert.ToInt32(sqlCommand.ExecuteScalar());

                        // Сохранение данных пользователя в классе Dannie
                        Dannie.Id = newUserId;
                        Dannie.FirstName = textBox4.Text;
                        Dannie.LastName = textBox3.Text;
                        Dannie.login = textBox1.Text;
                        Dannie.email = textBox5.Text;
                        Dannie.role = role;
                    }

                    MessageBox.Show("Регистрация прошла успешно!");
                    User Auth = new User();
                    Auth.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка регистрации: " + ex.Message);
            }
        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }


        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
    }

