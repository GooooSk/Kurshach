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
    public partial class Schelude : Form
    {
        public Schelude()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Coach Coach = new Coach();
            Coach.Show();
            this.Hide();
        }
        private void LoadScheduleForTrainer(int trainerId)
        {
            string connectionString = Dannie.connectionString;

            // SQL-запрос для выборки расписания, привязанного к тренеру, включая описание секции
            string query = @"
        SELECT 
            K_Schelude.id_schelude AS Код_расписания,
            K_Section.name AS Секция,
            K_Section.description AS Описание_Секции,
            K_Schelude.time_start AS Время_начала,
            K_Schelude.time_end AS Время_конца,
            K_Schelude.day_of_week AS День_недели,
            CASE WHEN K_Schelude.Approved = 1 THEN 'Да' ELSE 'Нет' END AS Одобрено
        FROM 
            K_Schelude
        JOIN 
            K_Section ON K_Section.id_section = K_Schelude.id_section
        WHERE 
            K_Section.id_trener = @TrainerId"; // Отображаем только секции тренера

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@TrainerId", trainerId);

                SqlDataReader reader = command.ExecuteReader();

                // Очищаем DataGridView перед загрузкой
                dataGridView1.Rows.Clear();

                // Заполняем DataGridView строками с данными расписания
                while (reader.Read())
                {
                    int rowIndex = dataGridView1.Rows.Add();
                    dataGridView1.Rows[rowIndex].Cells["Код_расписания"].Value = reader["Код_расписания"];
                    dataGridView1.Rows[rowIndex].Cells["Секция"].Value = reader["Секция"];
                    dataGridView1.Rows[rowIndex].Cells["Описание_Секции"].Value = reader["Описание_Секции"];
                    dataGridView1.Rows[rowIndex].Cells["Время_начала"].Value = reader["Время_начала"];
                    dataGridView1.Rows[rowIndex].Cells["Время_конца"].Value = reader["Время_конца"];
                    dataGridView1.Rows[rowIndex].Cells["День_недели"].Value = reader["День_недели"];
                    dataGridView1.Rows[rowIndex].Cells["Одобрено"].Value = reader["Одобрено"];
                }

                reader.Close();
            }
        }
        private void Schelude_Load(object sender, EventArgs e)
        {
            int trainerId = Dannie.Id; // ID тренера
            FillSectionsComboBox(trainerId); // Заполняем `ComboBox` секциями тренера
            LoadScheduleWithFilter(trainerId); // Загружаем расписание тренера
        }

        private void FillSectionsComboBox(int trainerId)
        {
            string connectionString = Dannie.connectionString;
            string query = @"
        SELECT DISTINCT K_Section.name 
        FROM K_Section
        WHERE K_Section.id_trener = @TrainerId";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@TrainerId", trainerId);

                SqlDataReader reader = command.ExecuteReader();

                sectionsComboBox.Items.Clear();

                while (reader.Read())
                {
                    sectionsComboBox.Items.Add(reader["name"].ToString());
                }

                reader.Close();
            }
        }

        private void LoadScheduleWithFilter(int trainerId)
        {
            string connectionString = Dannie.connectionString;
            string selectedSection = sectionsComboBox.SelectedItem?.ToString();
            string selectedDay = dayOfWeekComboBox.SelectedItem?.ToString();

            // Основной запрос для загрузки расписания тренера
            string query = @"
        SELECT 
            K_Schelude.id_schelude AS Код_расписания,
            K_Section.name AS Секция,
            K_Section.description AS Описание_Секции,
            K_Schelude.time_start AS Время_начала,
            K_Schelude.time_end AS Время_конца,
            K_Schelude.day_of_week AS День_недели,
            CASE WHEN K_Schelude.Approved = 1 THEN 'Да' ELSE 'Нет' END AS Одобрено
        FROM 
            K_Schelude
        JOIN 
            K_Section ON K_Section.id_section = K_Schelude.id_section
        WHERE 
            K_Section.id_trener = @TrainerId";

            // Условие для фильтрации по выбранной секции
            if (!string.IsNullOrEmpty(selectedSection))
            {
                query += " AND K_Section.name = @SelectedSection";
            }

            // Условие для фильтрации по выбранному дню недели
            if (!string.IsNullOrEmpty(selectedDay))
            {
                query += " AND K_Schelude.day_of_week = @SelectedDay";
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@TrainerId", trainerId);

                if (!string.IsNullOrEmpty(selectedSection))
                {
                    command.Parameters.AddWithValue("@SelectedSection", selectedSection);
                }

                if (!string.IsNullOrEmpty(selectedDay))
                {
                    command.Parameters.AddWithValue("@SelectedDay", selectedDay);
                }

                SqlDataReader reader = command.ExecuteReader();

                // Очищаем DataGridView перед загрузкой
                dataGridView1.Rows.Clear();

                // Заполняем DataGridView строками с данными расписания
                while (reader.Read())
                {
                    int rowIndex = dataGridView1.Rows.Add();
                    dataGridView1.Rows[rowIndex].Cells["Код_расписания"].Value = reader["Код_расписания"];
                    dataGridView1.Rows[rowIndex].Cells["Секция"].Value = reader["Секция"];
                    dataGridView1.Rows[rowIndex].Cells["Описание_Секции"].Value = reader["Описание_Секции"];
                    dataGridView1.Rows[rowIndex].Cells["Время_начала"].Value = reader["Время_начала"];
                    dataGridView1.Rows[rowIndex].Cells["Время_конца"].Value = reader["Время_конца"];
                    dataGridView1.Rows[rowIndex].Cells["День_недели"].Value = reader["День_недели"];
                    dataGridView1.Rows[rowIndex].Cells["Одобрено"].Value = reader["Одобрено"];
                }

                reader.Close();
            }
        }

        private void ClearFilters(int trainerId)
        {
            sectionsComboBox.SelectedIndex = -1; // Сбрасываем секцию
            dayOfWeekComboBox.SelectedIndex = -1; // Сбрасываем день недели

            // Загружаем расписание без фильтров (все записи тренера)
            LoadScheduleWithFilter(trainerId);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int trainerId = Dannie.Id; // Используем ID текущего тренера
            LoadScheduleWithFilter(trainerId);
        }

        private void button5_Click(object sender, EventArgs e)
        {

            int trainerId = Dannie.Id; // Используем ID текущего тренера
            ClearFilters(trainerId);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Пожалуйста, выберите одну строку для редактирования.");
                return;
            }

            // Получаем ID выбранного расписания
            int selectedScheduleId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Код_расписания"].Value);

            // Открываем форму в режиме редактирования
            AddEditForm editForm = new AddEditForm(true, selectedScheduleId);
            editForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Открываем форму в режиме добавления
            AddEditForm addForm = new AddEditForm(false);
            addForm.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
