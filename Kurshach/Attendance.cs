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
    public partial class Attendance : Form
    {
        public Attendance()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.Attendance_Load);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Coach Coach = new Coach();
            Coach.Show();
            this.Hide();
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

        private void FilterBookingsForTrainer(int trainerId)
        {
            string connectionString = Dannie.connectionString;
            string selectedSection = sectionsComboBox.SelectedItem?.ToString();
            string selectedDay = dayOfWeekComboBox.SelectedItem?.ToString();
            bool showHistory = historyCheckBox.Checked;

            // Базовый запрос для отображения записей
            string query = @"
        SELECT 
            K_Booking.id_booking AS Код_записи,
            CONCAT(K_User.surname, ' ', K_User.name) AS Фамилия_имя,
            K_Section.name AS Секция,
            K_Schelude.time_start AS Время_начала,
            K_Schelude.time_end AS Время_конца,
            K_Schelude.day_of_week AS День_недели,
            K_Booking.Attendance_status AS Присутствие,
            K_Booking.DateTime AS Дата_записи
        FROM 
            K_Booking
        JOIN 
            K_Schelude ON K_Schelude.id_schelude = K_Booking.id_schelude
        JOIN 
            K_Section ON K_Section.id_section = K_Schelude.id_section
        JOIN 
            K_User ON K_User.id_user = K_Booking.id_user
        WHERE 
            K_Section.id_trener = @TrainerId
            AND K_Schelude.Approved = 1";

            // Условие для фильтрации по секции, если она выбрана
            if (!string.IsNullOrEmpty(selectedSection))
            {
                query += " AND K_Section.name = @SelectedSection";
            }

            // Условие для фильтрации по дню недели, если он выбран
            if (!string.IsNullOrEmpty(selectedDay))
            {
                query += " AND K_Schelude.day_of_week = @SelectedDay";
            }

            // Условие для отображения только неотмеченных записей, если история не выбрана
            if (!showHistory)
            {
                query += " AND K_Booking.Attendance_status IS NULL";
            }

            // Добавляем сортировку по дате, сначала новые записи
            query += " ORDER BY K_Booking.DateTime DESC";

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

                // Очищаем DataGridView перед фильтрацией
                dataGridView1.Rows.Clear();

                // Заполняем DataGridView строками, соответствующими фильтру
                while (reader.Read())
                {
                    int rowIndex = dataGridView1.Rows.Add();
                    dataGridView1.Rows[rowIndex].Cells["Код_записи"].Value = reader["Код_записи"];
                    dataGridView1.Rows[rowIndex].Cells["Фамилия_имя"].Value = reader["Фамилия_имя"];
                    dataGridView1.Rows[rowIndex].Cells["Секция"].Value = reader["Секция"];
                    dataGridView1.Rows[rowIndex].Cells["Время_начала"].Value = reader["Время_начала"];
                    dataGridView1.Rows[rowIndex].Cells["Время_конца"].Value = reader["Время_конца"];
                    dataGridView1.Rows[rowIndex].Cells["День_недели"].Value = reader["День_недели"];
                    dataGridView1.Rows[rowIndex].Cells["Присутствие"].Value = reader["Присутствие"] == DBNull.Value ? "Не выбрано" : (bool)reader["Присутствие"] ? "Да" : "Нет";
                }

                reader.Close();
            }
        }


        private void ClearFilter(int trainerId)
        {
            sectionsComboBox.SelectedIndex = 0; // Сбросить ComboBox на пустое значение
            FilterBookingsForTrainer(trainerId); // Загрузить все записи без фильтра
        }

        private void FillSectionsComboBox2(int trainerId)
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

                sectionsComboBox2.Items.Clear();

                while (reader.Read())
                {
                    sectionsComboBox2.Items.Add(reader["name"].ToString());
                }

                reader.Close();
            }
        }
        private void FillUsersComboBox()
        {
            string connectionString = Dannie.connectionString;
            string query = @"
        SELECT DISTINCT K_User.id_user, CONCAT(K_User.surname, ' ', K_User.name) AS ФамилияИмя
        FROM K_Booking
        JOIN K_User ON K_User.id_user = K_Booking.id_user
        WHERE K_Booking.Attendance_status IS NOT NULL";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);

                SqlDataReader reader = command.ExecuteReader();

                usersComboBox.Items.Clear();

                while (reader.Read())
                {
                    usersComboBox.Items.Add(new { Id = reader["id_user"], Name = reader["ФамилияИмя"].ToString() });
                }

                reader.Close();
            }

            // Устанавливаем свойство для отображения имени в ComboBox
            usersComboBox.DisplayMember = "Name";
            usersComboBox.ValueMember = "Id";
        }

        private void CalculateAbsences()
        {
            string connectionString = Dannie.connectionString;
            int? selectedUserId = usersComboBox.SelectedItem != null ? (int?)((dynamic)usersComboBox.SelectedItem).Id : null;
            string selectedSection = sectionsComboBox2.SelectedItem?.ToString();
            DateTime startDate = dateTimePickerStart.Value;
            DateTime endDate = dateTimePickerEnd.Value;

            // Базовый запрос для подсчета пропусков
            string query = @"
        SELECT COUNT(*) 
        FROM K_Booking
        JOIN K_Schelude ON K_Schelude.id_schelude = K_Booking.id_schelude
        JOIN K_Section ON K_Section.id_section = K_Schelude.id_section
        WHERE K_Booking.Attendance_status = 0
        AND K_Booking.DateTime BETWEEN @StartDate AND @EndDate";

            // Условие для фильтрации по пользователю, если он выбран
            if (selectedUserId != null)
            {
                query += " AND K_Booking.id_user = @UserId";
            }

            // Условие для фильтрации по секции, если она выбрана в sectionsComboBox2
            if (!string.IsNullOrEmpty(selectedSection))
            {
                query += " AND K_Section.name = @Section";
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);

                if (selectedUserId != null)
                {
                    command.Parameters.AddWithValue("@UserId", selectedUserId);
                }

                if (!string.IsNullOrEmpty(selectedSection))
                {
                    command.Parameters.AddWithValue("@Section", selectedSection);
                }

                int absenceCount = (int)command.ExecuteScalar();

                // Отображаем результат пользователю
                MessageBox.Show($"Количество пропусков: {absenceCount}", "Результат");
            }
        }


        private void Attendance_Load(object sender, EventArgs e)
        {
            int trainerId = Dannie.Id; // Предполагается, что `Dannie.Id` — это ID тренера
            FillSectionsComboBox(trainerId); // Заполнение основного ComboBox секций
            FillSectionsComboBox2(trainerId); // Заполнение второго ComboBox секций
            FillUsersComboBox(); // Заполнение ComboBox пользователей
            FilterBookingsForTrainer(trainerId);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int trainerId = Dannie.Id;
            FilterBookingsForTrainer(trainerId);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int trainerId = Dannie.Id; // Получаем ID тренера из глобальной переменной или другого контекста

            sectionsComboBox.SelectedIndex = -1; // Сбросить ComboBox секций
            dayOfWeekComboBox.SelectedIndex = -1; // Сбросить ComboBox дней недели
            historyCheckBox.Checked = false; // Сбросить CheckBox "Отображать историю"

            // Загрузить данные без истории (только записи с Attendance_status = NULL)
            LoadBookingsForTrainer(trainerId);
        }


        private void LoadBookingsForTrainer(int trainerId)
        {
            string connectionString = Dannie.connectionString;

            string query = @"
        SELECT 
            K_Booking.id_booking AS Код_записи,
            CONCAT(K_User.surname, ' ', K_User.name) AS Фамилия_имя,
            K_Section.name AS Секция,
            K_Schelude.time_start AS Время_начала,
            K_Schelude.time_end AS Время_конца,
            K_Schelude.day_of_week AS День_недели,
            K_Booking.Attendance_status AS Присутствие,
            K_Booking.DateTime AS Дата_записи
        FROM 
            K_Booking
        JOIN 
            K_Schelude ON K_Schelude.id_schelude = K_Booking.id_schelude
        JOIN 
            K_Section ON K_Section.id_section = K_Schelude.id_section
        JOIN 
            K_User ON K_User.id_user = K_Booking.id_user
        WHERE 
            K_Section.id_trener = @TrainerId
            AND K_Schelude.Approved = 1
            AND K_Booking.Attendance_status IS NULL
        ORDER BY K_Booking.DateTime DESC"; // Новые записи будут первыми

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@TrainerId", trainerId);

                SqlDataReader reader = command.ExecuteReader();

                // Очищаем DataGridView перед загрузкой
                dataGridView1.Rows.Clear();

                // Заполняем DataGridView только неотмеченными записями
                while (reader.Read())
                {
                    int rowIndex = dataGridView1.Rows.Add();
                    dataGridView1.Rows[rowIndex].Cells["Код_записи"].Value = reader["Код_записи"];
                    dataGridView1.Rows[rowIndex].Cells["Фамилия_имя"].Value = reader["Фамилия_имя"];
                    dataGridView1.Rows[rowIndex].Cells["Секция"].Value = reader["Секция"];
                    dataGridView1.Rows[rowIndex].Cells["Время_начала"].Value = reader["Время_начала"];
                    dataGridView1.Rows[rowIndex].Cells["Время_конца"].Value = reader["Время_конца"];
                    dataGridView1.Rows[rowIndex].Cells["День_недели"].Value = reader["День_недели"];
                    dataGridView1.Rows[rowIndex].Cells["Присутствие"].Value = "Не выбрано";
                }

                reader.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Проверка, что строка выбрана
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите запись для отметки присутствия.");
                return;
            }

            // Получение id_booking из выбранной строки
            int bookingId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Код_записи"].Value);

            // Спрашиваем у тренера, присутствовал ли участник
            DialogResult result = MessageBox.Show("Присутствовал ли человек?", "Отметка присутствия", MessageBoxButtons.YesNo);

            bool attendanceStatus = result == DialogResult.Yes;

            // Обновление статуса присутствия в базе данных
            UpdateAttendanceStatus(bookingId, attendanceStatus);

            // Удаление строки из DataGridView после обновления, чтобы показать только неотмеченные записи
            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
        }

        private void UpdateAttendanceStatus(int bookingId, bool status)
        {
            string connectionString = Dannie.connectionString;
            string query = "UPDATE K_Booking SET Attendance_status = @Status WHERE id_booking = @BookingId";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@Status", status ? 1 : 0);
                command.Parameters.AddWithValue("@BookingId", bookingId);
                command.ExecuteNonQuery();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CalculateAbsences();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sectionsComboBox2.SelectedIndex = -1;

            // Очистка ComboBox для пользователя
            usersComboBox.SelectedIndex = -1;

            // Сброс DateTimePicker до текущей даты
            dateTimePickerStart.Value = DateTime.Now;
            dateTimePickerEnd.Value = DateTime.Now;
        }
    }
}
