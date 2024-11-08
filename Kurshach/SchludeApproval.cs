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
    public partial class SchludeApproval : Form
    {
        public SchludeApproval()
        {
            InitializeComponent();
        }

        private void SchludeApproval_Load(object sender, EventArgs e)
        {
            LoadSectionsComboBox();
            LoadTrainersComboBox();
            LoadScheduleData(); // Загружает данные в DataGridView

        }

        private void LoadScheduleData()
        {
            string connectionString = Dannie.connectionString;
            string query = @"
        SELECT 
            K_Schelude.id_schelude AS Код_расписания,
            CONCAT(K_User.surname, ' ', K_User.name) AS Тренер,
            K_Section.name AS Секция,
            K_Section.description AS Описание_секции,
            K_Schelude.time_start AS Время_начала,
            K_Schelude.time_end AS Время_конца,
            K_Schelude.day_of_week AS День_недели,
            CASE WHEN K_Schelude.Approved = 1 THEN 'Да' ELSE 'Нет' END AS Одобрено
        FROM 
            K_Schelude
        JOIN 
            K_Section ON K_Schelude.id_section = K_Section.id_section
        LEFT JOIN 
            K_User ON K_User.id_user = K_Section.id_trener";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();

                dataGridView1.Rows.Clear();

                while (reader.Read())
                {
                    int rowIndex = dataGridView1.Rows.Add();
                    dataGridView1.Rows[rowIndex].Cells["Код_расписания"].Value = reader["Код_расписания"];
                    dataGridView1.Rows[rowIndex].Cells["Тренер"].Value = reader["Тренер"];
                    dataGridView1.Rows[rowIndex].Cells["Секция"].Value = reader["Секция"];
                    dataGridView1.Rows[rowIndex].Cells["Описание_секции"].Value = reader["Описание_секции"];
                    dataGridView1.Rows[rowIndex].Cells["Время_начала"].Value = reader["Время_начала"];
                    dataGridView1.Rows[rowIndex].Cells["Время_конца"].Value = reader["Время_конца"];
                    dataGridView1.Rows[rowIndex].Cells["День_недели"].Value = reader["День_недели"];
                    dataGridView1.Rows[rowIndex].Cells["Одобрено"].Value = reader["Одобрено"];
                }

                reader.Close();
            }
        }


        private void LoadSectionsComboBox()
        {
            string connectionString = Dannie.connectionString;
            string query = "SELECT DISTINCT name FROM K_Section";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();

                sectionsComboBox.Items.Clear();
                sectionsComboBox.Items.Add("Все секции"); // Добавить опцию для отображения всех секций

                while (reader.Read())
                {
                    sectionsComboBox.Items.Add(reader["name"].ToString());
                }

                reader.Close();
            }

            sectionsComboBox.SelectedIndex = 0; // Устанавливаем первый элемент по умолчанию
        }


        private void LoadTrainersComboBox()
        {
            string connectionString = Dannie.connectionString;
            string query = "SELECT DISTINCT CONCAT(surname, ' ', name) AS FullName FROM K_User WHERE role = 2";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();

                trainersComboBox.Items.Clear();
                trainersComboBox.Items.Add("Все тренеры"); // Добавить опцию для отображения всех тренеров

                while (reader.Read())
                {
                    trainersComboBox.Items.Add(reader["FullName"].ToString());
                }

                reader.Close();
            }

            trainersComboBox.SelectedIndex = 0; // Устанавливаем первый элемент по умолчанию
        }


        

        private void button1_Click(object sender, EventArgs e)
        {
            Admin Admin = new Admin();
            Admin.Show();
            this.Hide();
        }
        private void UpdateScheduleApproval(int scheduleId, bool isApproved)
        {
            string connectionString = Dannie.connectionString;
            string query = "UPDATE K_Schelude SET Approved = @Approved WHERE id_schelude = @ScheduleId";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@Approved", isApproved ? 1 : 0);
                command.Parameters.AddWithValue("@ScheduleId", scheduleId);
                command.ExecuteNonQuery();
            }
        }



        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Пожалуйста, выберите только одну строку для одобрения.");
                return;
            }

            // Получаем ID расписания из выбранной строки
            int selectedScheduleId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Код_расписания"].Value);

            // Показываем сообщение для выбора одобрения или отклонения
            var result = MessageBox.Show("Вы хотите одобрить это расписание?", "Подтверждение", MessageBoxButtons.YesNoCancel);

            bool isApproved;

            // Проверка выбора пользователя
            if (result == DialogResult.Yes)
            {
                isApproved = true; // Одобрить
            }
            else if (result == DialogResult.No)
            {
                isApproved = false; // Не одобрить
            }
            else
            {
                return; // Отмена действия
            }

            // Обновляем значение "Одобрено" в DataGridView для выбранной строки
            dataGridView1.SelectedRows[0].Cells["Одобрено"].Value = isApproved ? "Да" : "Нет";

            // Обновляем значение в базе данных
            UpdateScheduleApproval(selectedScheduleId, isApproved);

        }





        private void button5_Click(object sender, EventArgs e)
        {
            sectionsComboBox.SelectedIndex = 0; // Устанавливаем "Все секции"
            trainersComboBox.SelectedIndex = 0; // Устанавливаем "Все тренеры"
            dayOfWeekComboBox.SelectedIndex = -1; // Очищаем выбор дня недели
            approvedCheckBox.Checked = false;
            notApprovedCheckBox.Checked = false;

            LoadScheduleData(); // Перезагружаем данные без фильтров
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = Dannie.connectionString;

            // Базовый запрос для фильтрации
            string query = @"
    SELECT 
        K_Schelude.id_schelude AS Код_расписания,
        CONCAT(K_User.surname, ' ', K_User.name) AS Тренер,
        K_Section.name AS Секция,
        K_Section.description AS Описание_секции,
        K_Schelude.time_start AS Время_начала,
        K_Schelude.time_end AS Время_конца,
        K_Schelude.day_of_week AS День_недели,
        CASE WHEN K_Schelude.Approved = 1 THEN 'Да' ELSE 'Нет' END AS Одобрено
    FROM 
        K_Schelude
    JOIN 
        K_Section ON K_Schelude.id_section = K_Section.id_section
    LEFT JOIN 
        K_User ON K_User.id_user = K_Section.id_trener
    WHERE 1=1"; // 1=1 для упрощения добавления условий

            // Проверка фильтра по секции
            if (sectionsComboBox.SelectedIndex > 0)
            {
                query += " AND K_Section.name = @Section";
            }

            // Проверка фильтра по тренеру
            if (trainersComboBox.SelectedIndex > 0)
            {
                query += " AND CONCAT(K_User.surname, ' ', K_User.name) = @Trainer";
            }

            // Проверка фильтра по дню недели
            if (dayOfWeekComboBox.SelectedIndex != -1)
            {
                query += " AND K_Schelude.day_of_week = @DayOfWeek";
            }

            // Проверка состояния чекбоксов для фильтрации по одобрению
            if (approvedCheckBox.Checked)
            {
                query += " AND K_Schelude.Approved = 1";
            }
            else if (notApprovedCheckBox.Checked)
            {
                query += " AND K_Schelude.Approved = 0";
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);

                // Параметры для фильтров
                if (sectionsComboBox.SelectedIndex > 0)
                {
                    command.Parameters.AddWithValue("@Section", sectionsComboBox.SelectedItem.ToString());
                }

                if (trainersComboBox.SelectedIndex > 0)
                {
                    command.Parameters.AddWithValue("@Trainer", trainersComboBox.SelectedItem.ToString());
                }

                if (dayOfWeekComboBox.SelectedIndex != -1)
                {
                    command.Parameters.AddWithValue("@DayOfWeek", dayOfWeekComboBox.SelectedItem.ToString());
                }

                SqlDataReader reader = command.ExecuteReader();

                dataGridView1.Rows.Clear();

                while (reader.Read())
                {
                    int rowIndex = dataGridView1.Rows.Add();
                    dataGridView1.Rows[rowIndex].Cells["Код_расписания"].Value = reader["Код_расписания"];
                    dataGridView1.Rows[rowIndex].Cells["Тренер"].Value = reader["Тренер"];
                    dataGridView1.Rows[rowIndex].Cells["Секция"].Value = reader["Секция"];
                    dataGridView1.Rows[rowIndex].Cells["Описание_секции"].Value = reader["Описание_секции"];
                    dataGridView1.Rows[rowIndex].Cells["Время_начала"].Value = reader["Время_начала"];
                    dataGridView1.Rows[rowIndex].Cells["Время_конца"].Value = reader["Время_конца"];
                    dataGridView1.Rows[rowIndex].Cells["День_недели"].Value = reader["День_недели"];
                    dataGridView1.Rows[rowIndex].Cells["Одобрено"].Value = reader["Одобрено"];
                }

                reader.Close();
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void notApprovedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (notApprovedCheckBox.Checked)
            {
                approvedCheckBox.Checked = false;
            }
        }

        private void approvedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (approvedCheckBox.Checked)
            {
                notApprovedCheckBox.Checked = false;
            }
        }
    }
}
