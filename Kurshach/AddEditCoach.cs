using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kurshach
{
    public partial class AddEditCoach : Form
    {




        public int? UserId { get; set; } // Nullable int, чтобы отличить добавление от редактирования


        public AddEditCoach()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                string connectionString = Dannie.connectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (this.UserId == null) // Новый тренер
                    {
                        // Выполняем INSERT-запрос
                        string query = "INSERT INTO K_User (login, password, surname, name, email, phone, Birthday, role) VALUES (@Login, @Password, @Surname, @Name, @Email, @Phone, @Birthday, 2)";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Login", loginTextBox.Text);
                        command.Parameters.AddWithValue("@Password", passwordTextBox.Text);
                        command.Parameters.AddWithValue("@Surname", surnameTextBox.Text);
                        command.Parameters.AddWithValue("@Name", nameTextBox.Text);
                        command.Parameters.AddWithValue("@Email", emailTextBox.Text);
                        command.Parameters.AddWithValue("@Phone", maskedPhoneTextBox.Text);
                        command.Parameters.AddWithValue("@Birthday", birthdayDateTimePicker.Value);
                        command.ExecuteNonQuery();
                    }
                    else // Обновление существующего тренера
                    {
                        // Выполняем UPDATE-запрос
                        string query = "UPDATE K_User SET login = @Login, password = @Password, surname = @Surname, name = @Name, email = @Email, phone = @Phone, Birthday = @Birthday WHERE id_user = @UserId";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Login", loginTextBox.Text);
                        command.Parameters.AddWithValue("@Password", passwordTextBox.Text);
                        command.Parameters.AddWithValue("@Surname", surnameTextBox.Text);
                        command.Parameters.AddWithValue("@Name", nameTextBox.Text);
                        command.Parameters.AddWithValue("@Email", emailTextBox.Text);
                        command.Parameters.AddWithValue("@Phone", maskedPhoneTextBox.Text);
                        command.Parameters.AddWithValue("@Birthday", birthdayDateTimePicker.Value);
                        command.Parameters.AddWithValue("@UserId", this.UserId);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Данные успешно сохранены.");
                this.Close();
            }
        }

        // Валидация полей
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(loginTextBox.Text) || string.IsNullOrWhiteSpace(passwordTextBox.Text) ||
                string.IsNullOrWhiteSpace(surnameTextBox.Text) || string.IsNullOrWhiteSpace(nameTextBox.Text) ||
                string.IsNullOrWhiteSpace(emailTextBox.Text) || string.IsNullOrWhiteSpace(maskedPhoneTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return false;
            }

            // Проверка email
            if (!Regex.IsMatch(emailTextBox.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Введите корректный email.");
                return false;
            }

            // Проверка даты рождения
            if (birthdayDateTimePicker.Value > DateTime.Now)
            {
                MessageBox.Show("Дата рождения не может быть больше сегодняшней.");
                return false;
            }

            return true;
        }

        private void AddEditCoach_Load(object sender, EventArgs e)
        {
            maskedPhoneTextBox.Mask = "+7 (000) 000-0000";

            if (UserId != null) // Если редактируем существующего тренера
            {
                LoadTrainerData();
            }
        }

        private void LoadTrainerData()
        {
            string connectionString = Dannie.connectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT login, password, surname, name, email, phone, Birthday FROM K_User WHERE id_user = @UserId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", this.UserId);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    loginTextBox.Text = reader["login"].ToString();
                    passwordTextBox.Text = reader["password"].ToString();
                    surnameTextBox.Text = reader["surname"].ToString();
                    nameTextBox.Text = reader["name"].ToString();
                    emailTextBox.Text = reader["email"].ToString();
                    maskedPhoneTextBox.Text = reader["phone"].ToString();
                    birthdayDateTimePicker.Value = Convert.ToDateTime(reader["Birthday"]);
                }
                reader.Close();
            }
        }
    }
}
