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
    public partial class User : Form
    {
        private int selectedScheduleId = -1; // Хранит id выбранного расписания

      




        public User()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void User_Load(object sender, EventArgs e)
        {
            label1.Text = Dannie.FirstName;
            label2.Text = Dannie.LastName;

            CheckNotifications();

            // Строка подключения к базе данных
            string connectionString = Dannie.connectionString;

            // Запрос для получения данных о расписании, секциях и тренерах
            string query = @"
       SELECT 
    K_Schelude.id_schelude AS Код_расписания,
    CONCAT(K_User.surname, ' ', K_User.name) AS Тренер,
    K_Schelude.day_of_week AS День_недели,
    K_Section.name AS Секция,
    K_Section.description AS Описание,
    K_Schelude.time_start AS Время_начала,
    K_Schelude.time_end AS Время_конца
FROM 
    K_Schelude
JOIN 
    K_Section ON K_Section.id_section = K_Schelude.id_section
LEFT JOIN 
    K_User ON K_User.id_user = K_Section.id_trener AND K_User.role = 2
WHERE 
    K_Schelude.Approved = 1;";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();

                // Очистка DataGridView перед добавлением новых данных
                dataGridView1.Rows.Clear();

                // Чтение данных и добавление строк в DataGridView
                while (reader.Read())
                {
                    int rowIndex = dataGridView1.Rows.Add();
                    dataGridView1.Rows[rowIndex].Cells["Код_расписания"].Value = reader["Код_расписания"];
                    dataGridView1.Rows[rowIndex].Cells["Тренер"].Value = reader["Тренер"] == DBNull.Value ? "—" : reader["Тренер"].ToString();
                    dataGridView1.Rows[rowIndex].Cells["День_недели"].Value = reader["День_недели"].ToString();
                    dataGridView1.Rows[rowIndex].Cells["Секция"].Value = reader["Секция"].ToString();
                    dataGridView1.Rows[rowIndex].Cells["Описание"].Value = reader["Описание"].ToString();
                    dataGridView1.Rows[rowIndex].Cells["Время_начала"].Value = reader["Время_начала"].ToString();
                    dataGridView1.Rows[rowIndex].Cells["Время_конца"].Value = reader["Время_конца"].ToString();
                }

                reader.Close();
            }


            // Заполнение ComboBox для дней недели

            // Заполнение ComboBox для секций
            FillComboBox("SELECT name FROM K_Section", sectionsComboBox);

        }

        private void FillComboBox(string query, ComboBox comboBox)
        {
            string connectionString = Dannie.connectionString;
    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
    {
        sqlConnection.Open();
        SqlCommand command = new SqlCommand(query, sqlConnection);
        SqlDataReader reader = command.ExecuteReader();

        comboBox.Items.Clear();
        while (reader.Read())
        {
            comboBox.Items.Add(reader[0].ToString());
        }

        reader.Close();
    }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Autho Auth = new Autho();
            Auth.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Очищаем DataGridView перед обновлением
            dataGridView1.Rows.Clear();

            string connectionString = Dannie.connectionString;

            // Базовый запрос
            string query = @"
SELECT 
    K_Schelude.id_schelude AS Код_расписания,
    CONCAT(K_User.surname, ' ', K_User.name) AS Тренер,
    K_Schelude.day_of_week AS День_недели,
    K_Section.name AS Секция,
    K_Section.description AS Описание,
    K_Schelude.time_start AS Время_начала,
    K_Schelude.time_end AS Время_конца
FROM 
    K_Schelude
JOIN 
    K_Section ON K_Section.id_section = K_Schelude.id_section
LEFT JOIN 
    K_User ON K_User.id_user = K_Section.id_trener AND K_User.role = 2
WHERE 
    K_Schelude.Approved = 1";

            // Условия для фильтрации
            List<string> filters = new List<string>();
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (dayOfWeekComboBox.SelectedItem != null)
            {
                filters.Add("K_Schelude.day_of_week = @DayOfWeek");
                parameters.Add(new SqlParameter("@DayOfWeek", dayOfWeekComboBox.SelectedItem.ToString()));
            }

            if (sectionsComboBox.SelectedItem != null)
            {
                filters.Add("K_Section.name = @Section");
                parameters.Add(new SqlParameter("@Section", sectionsComboBox.SelectedItem.ToString()));
            }

            // Добавляем условия к запросу, если они существуют
            if (filters.Count > 0)
            {
                query += " AND " + string.Join(" AND ", filters);
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);

                // Добавляем параметры к команде
                foreach (var param in parameters)
                {
                    command.Parameters.Add(param);
                }

                SqlDataReader reader = command.ExecuteReader();

                // Чтение данных и добавление строк в DataGridView
                while (reader.Read())
                {
                    int rowIndex = dataGridView1.Rows.Add();
                    dataGridView1.Rows[rowIndex].Cells["Код_расписания"].Value = reader["Код_расписания"];
                    dataGridView1.Rows[rowIndex].Cells["Тренер"].Value = reader["Тренер"] == DBNull.Value ? "—" : reader["Тренер"].ToString();
                    dataGridView1.Rows[rowIndex].Cells["День_недели"].Value = reader["День_недели"].ToString();
                    dataGridView1.Rows[rowIndex].Cells["Секция"].Value = reader["Секция"].ToString();
                    dataGridView1.Rows[rowIndex].Cells["Описание"].Value = reader["Описание"].ToString();
                    dataGridView1.Rows[rowIndex].Cells["Время_начала"].Value = reader["Время_начала"].ToString();
                    dataGridView1.Rows[rowIndex].Cells["Время_конца"].Value = reader["Время_конца"].ToString();
                }

                reader.Close();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Очистка DataGridView и ComboBox
            dataGridView1.Rows.Clear();
            dayOfWeekComboBox.SelectedIndex = -1;
            sectionsComboBox.SelectedIndex = -1;


            // Строка подключения к базе данных
            string connectionString = Dannie.connectionString;

            // Запрос для получения данных о расписании, секциях и тренерах
            string query = @"
       SELECT 
    K_Schelude.id_schelude AS Код_расписания,
    CONCAT(K_User.surname, ' ', K_User.name) AS Тренер,
    K_Schelude.day_of_week AS День_недели,
    K_Section.name AS Секция,
    K_Section.description AS Описание,
    K_Schelude.time_start AS Время_начала,
    K_Schelude.time_end AS Время_конца
FROM 
    K_Schelude
JOIN 
    K_Section ON K_Section.id_section = K_Schelude.id_section
LEFT JOIN 
    K_User ON K_User.id_user = K_Section.id_trener AND K_User.role = 2
WHERE 
    K_Schelude.Approved = 1;";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();

                // Очистка DataGridView перед добавлением новых данных
                dataGridView1.Rows.Clear();

                // Чтение данных и добавление строк в DataGridView
                while (reader.Read())
                {
                    int rowIndex = dataGridView1.Rows.Add();
                    dataGridView1.Rows[rowIndex].Cells["Код_расписания"].Value = reader["Код_расписания"];
                    dataGridView1.Rows[rowIndex].Cells["Тренер"].Value = reader["Тренер"] == DBNull.Value ? "—" : reader["Тренер"].ToString();
                    dataGridView1.Rows[rowIndex].Cells["День_недели"].Value = reader["День_недели"].ToString();
                    dataGridView1.Rows[rowIndex].Cells["Секция"].Value = reader["Секция"].ToString();
                    dataGridView1.Rows[rowIndex].Cells["Описание"].Value = reader["Описание"].ToString();
                    dataGridView1.Rows[rowIndex].Cells["Время_начала"].Value = reader["Время_начала"].ToString();
                    dataGridView1.Rows[rowIndex].Cells["Время_конца"].Value = reader["Время_конца"].ToString();
                }

                reader.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (selectedScheduleId == -1)
            {
                MessageBox.Show("Пожалуйста, выберите расписание для записи.");
                return;
            }

            // Подтверждение у пользователя
            var result = MessageBox.Show("Вы уверены, что хотите записаться на это занятие?", "Подтверждение записи", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Вставка записи в таблицу K_Booking
                string connectionString = Dannie.connectionString;
                string query = @"
            INSERT INTO K_Booking (id_user, id_schelude, DateTime) 
            VALUES (@UserId, @ScheduleId, @CurrentDateTime)";

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand command = new SqlCommand(query, sqlConnection);

                    // Установка значений параметров
                    command.Parameters.AddWithValue("@UserId", Dannie.Id); // ID текущего пользователя
                    command.Parameters.AddWithValue("@ScheduleId", selectedScheduleId);
                    command.Parameters.AddWithValue("@CurrentDateTime", DateTime.Now);

                    // Выполнение команды
                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Вы успешно записаны на занятие.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при записи на занятие: " + ex.Message);
                    }
                }
            }
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получаем id расписания из выбранной строки
                selectedScheduleId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Код_расписания"].Value);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Dannie.Id == -1) // Проверяем, что пользователь авторизован
            {
                MessageBox.Show("Пожалуйста, войдите в систему, чтобы увидеть свои тренировки.");
                return;
            }

            string connectionString = Dannie.connectionString;

            // Запрос для отображения предстоящих тренировок, удовлетворяющих условиям
            string query = @"
        SELECT 
            K_Booking.id_booking AS Код_записи,
            CONCAT(K_User.surname, ' ', K_User.name) AS Тренер,
            K_Schelude.day_of_week AS День_недели,
            K_Section.name AS Секция,
            K_Section.description AS Описание,
            K_Schelude.time_start AS Время_начала,
            K_Schelude.time_end AS Время_конца,
            K_Booking.Attendance_status AS Статус_посещения
        FROM 
            K_Booking
        JOIN 
            K_Schelude ON K_Schelude.id_schelude = K_Booking.id_schelude
        JOIN 
            K_Section ON K_Section.id_section = K_Schelude.id_section
        JOIN 
            K_User ON K_User.id_user = K_Section.id_trener
        WHERE 
            K_Booking.id_user = @UserId
            AND (K_Schelude.time_start >= @TodayDate
                 OR (K_Booking.Attendance_status IS NULL 
                     AND K_Booking.DateTime >= DATEADD(DAY, -7, @TodayDate)))";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@UserId", Dannie.Id);
                command.Parameters.AddWithValue("@TodayDate", DateTime.Today);

                SqlDataReader reader = command.ExecuteReader();

                // Очищаем DataGridView перед отображением данных
                dataGridView1.Rows.Clear();

                // Чтение данных и добавление строк в DataGridView
                while (reader.Read())
                {
                    // Получаем значение `Attendance_status`, чтобы проверить его
                    var attendanceStatus = reader["Статус_посещения"];

                    // Проверка: если статус посещения не NULL, не добавляем запись
                    if (attendanceStatus != DBNull.Value)
                        continue;

                    int rowIndex = dataGridView1.Rows.Add();
                    dataGridView1.Rows[rowIndex].Cells["Код_расписания"].Value = reader["Код_записи"];
                    dataGridView1.Rows[rowIndex].Cells["Тренер"].Value = reader["Тренер"];
                    dataGridView1.Rows[rowIndex].Cells["День_недели"].Value = reader["День_недели"];
                    dataGridView1.Rows[rowIndex].Cells["Секция"].Value = reader["Секция"];
                    dataGridView1.Rows[rowIndex].Cells["Описание"].Value = reader["Описание"];
                    dataGridView1.Rows[rowIndex].Cells["Время_начала"].Value = reader["Время_начала"];
                    dataGridView1.Rows[rowIndex].Cells["Время_конца"].Value = reader["Время_конца"];
                }

                reader.Close();
            }
        }


        public class Notification
        {
            public int Id { get; set; }          // Идентификатор уведомления
            public int UserId { get; set; }       // Идентификатор пользователя
            public DateTime SentDate { get; set; } // Дата отправки уведомления
            public string Message { get; set; }    // Текст уведомления
            public bool Checked { get; set; }      // Статус прочтения (True или False)
            public string TrainerName { get; set; } // Имя тренера, отправившего уведомление
        }


        private List<Notification> notifications = new List<Notification>();



        private void CheckNotifications()
        {
            string connectionString = Dannie.connectionString;
            string query = @"
        SELECT 
            n.id_notification, 
            n.sent_date, 
            n.message, 
            nu.Checked, 
            CONCAT(t.surname, ' ', t.name) AS TrainerName
        FROM 
            K_Notification n
        JOIN 
            K_notification_User nu ON n.id_notification = nu.id_notification
        JOIN 
            K_User t ON nu.id_trener = t.id_user
        WHERE 
            nu.id_user = @UserId AND n.sent_date = @TodayDate";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@UserId", Dannie.Id);
                command.Parameters.AddWithValue("@TodayDate", DateTime.Today);

                SqlDataReader reader = command.ExecuteReader();
                notifications.Clear();
                bool hasUncheckedNotifications = false;

                while (reader.Read())
                {
                    notifications.Add(new Notification
                    {
                        Id = reader.GetInt32(0),
                        SentDate = reader.GetDateTime(1),
                        Message = reader.GetString(2),
                        Checked = reader.GetBoolean(3),
                        TrainerName = reader.GetString(4)
                    });

                    if (!reader.GetBoolean(3)) // Проверка на непрочитанные уведомления
                    {
                        hasUncheckedNotifications = true;
                    }
                }

                reader.Close();
                notificationButton.BackColor = hasUncheckedNotifications ? Color.Red : SystemColors.Control;
            }
        }





        private void button2_Click(object sender, EventArgs e)
        {
            if (notifications.Count > 0)
            {
                StringBuilder notificationText = new StringBuilder();

                foreach (var notification in notifications)
                {
                    notificationText.AppendLine($"От тренера: {notification.TrainerName}");
                    notificationText.AppendLine($"Дата уведомления: {notification.SentDate.ToShortDateString()}");
                    notificationText.AppendLine($"Сообщение: {notification.Message}");
                    notificationText.AppendLine();
                }

                MessageBox.Show(notificationText.ToString(), "Уведомления");

                UpdateNotificationsAsChecked();
                notificationButton.BackColor = SystemColors.Control;
            }
            else
            {
                MessageBox.Show("На сегодня нет уведомлений.");
            }
        }


        private void UpdateNotificationsAsChecked()
        {
            string connectionString = Dannie.connectionString;
            string query = "UPDATE K_notification_User SET Checked = 1 WHERE id_notification = @NotificationId AND id_user = @UserId";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                foreach (var notification in notifications)
                {
                    if (!notification.Checked) // Обновляем только непрочитанные уведомления
                    {
                        SqlCommand command = new SqlCommand(query, sqlConnection);
                        command.Parameters.AddWithValue("@NotificationId", notification.Id);
                        command.Parameters.AddWithValue("@UserId", Dannie.Id);
                        command.ExecuteNonQuery();

                        // Обновляем статус в объекте
                        notification.Checked = true;
                    }
                }
            }
        }


    }





}
