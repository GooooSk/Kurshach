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

namespace Kurshach
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Admin Admin = new Admin();
            Admin.Show();
            this.Hide();
        }

        private void LoadUserData()
        {
            string connectionString = Dannie.connectionString;
            string query = @"
        SELECT 
            K_User.id_user AS Код_пользователя,
            K_User.login AS Логин,
            K_User.password AS Пароль,
            K_role.Name AS Роль, -- Здесь будет 'Тренер' или 'Участник'
            K_User.surname AS Фамилия,
            K_User.name AS Имя,
            K_User.email AS Почта,
            K_User.phone AS Телефон,
            K_User.Birthday AS Дата_рождения
        FROM 
            K_User
        JOIN 
            K_role ON K_User.role = K_role.id_role
        WHERE 
            K_User.role != 1"; // Исключаем администраторов

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();

                dataGridView1.Rows.Clear();

                while (reader.Read())
                {
                    int rowIndex = dataGridView1.Rows.Add();
                    dataGridView1.Rows[rowIndex].Cells["Код_пользователя"].Value = reader["Код_пользователя"];
                    dataGridView1.Rows[rowIndex].Cells["Логин"].Value = reader["Логин"];
                    dataGridView1.Rows[rowIndex].Cells["Пароль"].Value = reader["Пароль"];
                    dataGridView1.Rows[rowIndex].Cells["Роль"].Value = reader["Роль"]; // Роль отображается текстом
                    dataGridView1.Rows[rowIndex].Cells["Фамилия"].Value = reader["Фамилия"];
                    dataGridView1.Rows[rowIndex].Cells["Имя"].Value = reader["Имя"];
                    dataGridView1.Rows[rowIndex].Cells["Почта"].Value = reader["Почта"];
                    dataGridView1.Rows[rowIndex].Cells["Телефон"].Value = reader["Телефон"];
                    dataGridView1.Rows[rowIndex].Cells["Дата_рождения"].Value = reader["Дата_рождения"];
                }

                reader.Close();
            }
        }

        private void Users_Load(object sender, EventArgs e)
        {
            LoadUserData();
        }


        private void FilterUserData()
        {
            string connectionString = Dannie.connectionString;

            // Базовый SQL-запрос с объединением таблиц
            string query = @"
        SELECT 
            K_User.id_user AS Код_пользователя,
            K_User.login AS Логин,
            K_User.password AS Пароль,
            K_role.Name AS Роль,
            K_User.surname AS Фамилия,
            K_User.name AS Имя,
            K_User.email AS Почта,
            K_User.phone AS Телефон,
            K_User.Birthday AS Дата_рождения
        FROM 
            K_User
        JOIN 
            K_role ON K_User.role = K_role.id_role
        WHERE 
            K_User.role != 1"; // Исключаем администраторов

            List<SqlParameter> parameters = new List<SqlParameter>();

            // Добавляем фильтр по фамилии, если поле не пустое
            if (!string.IsNullOrWhiteSpace(surnameTextBox.Text))
            {
                query += " AND K_User.surname LIKE @Surname";
                parameters.Add(new SqlParameter("@Surname", "%" + surnameTextBox.Text.Trim() + "%"));
            }

            // Добавляем фильтр по имени, если поле не пустое
            if (!string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                query += " AND K_User.name LIKE @Name";
                parameters.Add(new SqlParameter("@Name", "%" + nameTextBox.Text.Trim() + "%"));
            }

            // Добавляем фильтр по логину, если поле не пустое
            if (!string.IsNullOrWhiteSpace(loginTextBox.Text))
            {
                query += " AND K_User.login LIKE @Login";
                parameters.Add(new SqlParameter("@Login", "%" + loginTextBox.Text.Trim() + "%"));
            }

            // Добавляем фильтр по роли - тренер
            if (trainerCheckBox.Checked)
            {
                query += " AND K_role.Name = 'Тренер'";
            }
            else if (participantCheckBox.Checked) // Добавляем фильтр по роли - участник
            {
                query += " AND K_role.Name = 'Участник'";
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);

                // Добавляем параметры в команду
                foreach (var param in parameters)
                {
                    command.Parameters.Add(param);
                }

                SqlDataReader reader = command.ExecuteReader();
                dataGridView1.Rows.Clear();

                while (reader.Read())
                {
                    int rowIndex = dataGridView1.Rows.Add();
                    dataGridView1.Rows[rowIndex].Cells["Код_пользователя"].Value = reader["Код_пользователя"];
                    dataGridView1.Rows[rowIndex].Cells["Логин"].Value = reader["Логин"];
                    dataGridView1.Rows[rowIndex].Cells["Пароль"].Value = reader["Пароль"];
                    dataGridView1.Rows[rowIndex].Cells["Роль"].Value = reader["Роль"];
                    dataGridView1.Rows[rowIndex].Cells["Фамилия"].Value = reader["Фамилия"];
                    dataGridView1.Rows[rowIndex].Cells["Имя"].Value = reader["Имя"];
                    dataGridView1.Rows[rowIndex].Cells["Почта"].Value = reader["Почта"];
                    dataGridView1.Rows[rowIndex].Cells["Телефон"].Value = reader["Телефон"];
                    dataGridView1.Rows[rowIndex].Cells["Дата_рождения"].Value = reader["Дата_рождения"];
                }

                reader.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FilterUserData();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Очищаем текстовые поля и снимаем отметки с чекбоксов
            surnameTextBox.Clear();
            nameTextBox.Clear();
            loginTextBox.Clear();
            trainerCheckBox.Checked = false;
            participantCheckBox.Checked = false;

            // Заново загружаем все данные
            LoadUserData();
        }

        private void trainerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (trainerCheckBox.Checked)
            {
                participantCheckBox.Checked = false;
            }
        }

        private void participantCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (participantCheckBox.Checked)
            {
                trainerCheckBox.Checked = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddEditCoach addForm = new AddEditCoach();
            addForm.ShowDialog();
            LoadUserData(); // Обновляем список пользователей после закрытия формы
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int selectedUserId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Код_пользователя"].Value);
                AddEditCoach editForm = new AddEditCoach();
                editForm.UserId = selectedUserId; // Передаем ID выбранного тренера
                editForm.ShowDialog();
                LoadUserData(); // Обновляем список пользователей после закрытия формы
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите одного тренера для редактирования.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Пожалуйста, выберите одну строку для удаления.");
                return;
            }

            // Проверяем, что выбранный пользователь — тренер
            if (dataGridView1.SelectedRows[0].Cells["Роль"].Value.ToString() != "Тренер")
            {
                MessageBox.Show("Можно удалить только тренера.");
                return;
            }

            int userId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Код_пользователя"].Value);

            var result = MessageBox.Show("Вы уверены, что хотите удалить этого тренера?", "Подтверждение удаления", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string connectionString = Dannie.connectionString;
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM K_User WHERE id_user = @UserId", sqlConnection);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.ExecuteNonQuery();
                }

                LoadUserData(); // Перезагружаем данные после удаления
            }

        }
    }
}
