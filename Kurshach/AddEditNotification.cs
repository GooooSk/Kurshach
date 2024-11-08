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
    public partial class AddEditNotification : Form
    {

        public int NotificationId { get; set; } = -1; // -1 для нового уведомления, иначе редактируемое уведомление


        public AddEditNotification()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Coach Coach = new Coach();
            Coach.Show();
            this.Close();
            
        }

        private void LoadUsers()
        {
            string connectionString = Dannie.connectionString;
            string query = "SELECT id_user, CONCAT(surname, ' ', name) AS FullName FROM K_User WHERE role = 3"; // роль 3 для пользователей

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();

                userSelectionListBox.Items.Clear();
                while (reader.Read())
                {
                    int userId = reader.GetInt32(0);
                    string fullName = reader.GetString(1);
                    userSelectionListBox.Items.Add(new UserItem { Id = userId, FullName = fullName });
                }
                reader.Close();
            }
        }

        private void LoadNotificationData()
        {
            string connectionString = Dannie.connectionString;
            string query = "SELECT message FROM K_Notification WHERE id_notification = @NotificationId";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@NotificationId", NotificationId);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    messageTextBox.Text = reader.GetString(0);
                }
                reader.Close();
            }

            // Загружаем выбранных пользователей для текущего уведомления
            LoadSelectedUsers();
        }
        private void LoadSelectedUsers()
        {
            string connectionString = Dannie.connectionString;
            string query = "SELECT id_user FROM K_notification_User WHERE id_notification = @NotificationId";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@NotificationId", NotificationId);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int userId = reader.GetInt32(0);
                    for (int i = 0; i < userSelectionListBox.Items.Count; i++)
                    {
                        if (((UserItem)userSelectionListBox.Items[i]).Id == userId)
                        {
                            userSelectionListBox.SetItemChecked(i, true);
                            break;
                        }
                    }
                }
                reader.Close();
            }
        }

        private class UserItem
        {
            public int Id { get; set; }
            public string FullName { get; set; }

            public override string ToString() => FullName; // Отображаем полное имя в списке
        }


        private void saveNotificationButton_Click(object sender, EventArgs e)
        {
            string connectionString = Dannie.connectionString;
            string notificationMessage = messageTextBox.Text.Trim();

            // Проверка, что сообщение не пустое
            if (string.IsNullOrEmpty(notificationMessage))
            {
                MessageBox.Show("Пожалуйста, введите текст уведомления.");
                return;
            }

            // Сбор выбранных пользователей
            List<int> selectedUserIds = new List<int>();
            foreach (var item in userSelectionListBox.CheckedItems)
            {
                var user = (UserItem)item;
                selectedUserIds.Add(user.Id);
            }

            if (selectedUserIds.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите хотя бы одного пользователя для уведомления.");
                return;
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                if (NotificationId != -1) // Если уведомление уже существует
                {
                    // Обновляем текст уведомления в K_Notification
                    string updateQuery = "UPDATE K_Notification SET message = @Message, sent_date = @Date WHERE id_notification = @NotificationId";
                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection))
                    {
                        updateCommand.Parameters.AddWithValue("@Message", notificationMessage);
                        updateCommand.Parameters.AddWithValue("@Date", DateTime.Today);
                        updateCommand.Parameters.AddWithValue("@NotificationId", NotificationId);

                        updateCommand.ExecuteNonQuery();
                    }
                }
                else
                {
                    // Вставляем новое уведомление, если NotificationId = -1
                    string insertQuery = "INSERT INTO K_Notification (message, sent_date) OUTPUT INSERTED.id_notification VALUES (@Message, @Date)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection))
                    {
                        insertCommand.Parameters.AddWithValue("@Message", notificationMessage);
                        insertCommand.Parameters.AddWithValue("@Date", DateTime.Today);
                        NotificationId = (int)insertCommand.ExecuteScalar(); // Получаем новый ID
                    }
                }

                // Устанавливаем `Checked = 0` для пользователей, независимо от того, создаем или обновляем уведомление
                foreach (int userId in selectedUserIds)
                {
                    string linkQuery = @"
                IF EXISTS (SELECT 1 FROM K_notification_User WHERE id_notification = @NotificationId AND id_user = @UserId)
                    UPDATE K_notification_User SET Checked = 0 WHERE id_notification = @NotificationId AND id_user = @UserId
                ELSE
                    INSERT INTO K_notification_User (id_notification, id_user, Checked, id_trener) 
                    VALUES (@NotificationId, @UserId, 0, @TrainerId)";

                    using (SqlCommand linkCommand = new SqlCommand(linkQuery, sqlConnection))
                    {
                        linkCommand.Parameters.AddWithValue("@NotificationId", NotificationId);
                        linkCommand.Parameters.AddWithValue("@UserId", userId);
                        linkCommand.Parameters.AddWithValue("@TrainerId", Dannie.Id);

                        linkCommand.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Уведомление успешно сохранено.");
                this.DialogResult = DialogResult.OK;

                Coach Coach = new Coach();
                Coach.Show();
                this.Close();
            }
        }
    
      


        

        private void AddEditNotification_Load(object sender, EventArgs e)
        {
           LoadUsers();
            // Загружаем пользователей, записанных к тренеру
            LoadUsersForTrainer(Dannie.Id);

            // Проверяем, есть ли уведомление на сегодня для текущего тренера
            CheckTodayNotification();

            // Если найдено уведомление, загрузить данные
            if (NotificationId != -1)
            {
                LoadNotificationData(); // Заполняет messageTextBox существующим сообщением
            }
        }

        private void CheckTodayNotification()
        {
            string connectionString = Dannie.connectionString;

            // Обновлённый запрос, который ищет уведомления для текущего тренера и даты
            string query = @"
        SELECT K_Notification.id_notification, K_Notification.message 
        FROM K_Notification
        JOIN K_notification_User ON K_Notification.id_notification = K_notification_User.id_notification
        WHERE K_notification_User.id_trener = @TrainerId 
            AND K_Notification.sent_date = @TodayDate";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@TrainerId", Dannie.Id);  // Используем идентификатор тренера
                command.Parameters.AddWithValue("@TodayDate", DateTime.Today);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    NotificationId = reader.GetInt32(0);
                    messageTextBox.Text = reader.GetString(1); // Загружаем текст уведомления
                    saveNotificationButton.Text = "Редактировать уведомление"; // Меняем текст кнопки
                }
                reader.Close();
            }
        }


        private void LoadUsersForTrainer(int trainerId)
        {
            string connectionString = Dannie.connectionString;

            // Запрос для получения пользователей, записанных к тренеру
            string query = @"
        SELECT DISTINCT K_User.id_user, CONCAT(K_User.surname, ' ', K_User.name) AS FullName
        FROM K_Booking
        JOIN K_Schelude ON K_Booking.id_schelude = K_Schelude.id_schelude
        JOIN K_Section ON K_Schelude.id_section = K_Section.id_section
        JOIN K_User ON K_Booking.id_user = K_User.id_user
        WHERE K_Section.id_trener = @TrainerId";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@TrainerId", trainerId);

                SqlDataReader reader = command.ExecuteReader();

                userSelectionListBox.Items.Clear(); // Очищаем список перед добавлением пользователей

                // Заполняем список пользователями, записанными к тренеру
                while (reader.Read())
                {
                    int userId = reader.GetInt32(0);
                    string fullName = reader.GetString(1);
                    userSelectionListBox.Items.Add(new UserItem { Id = userId, FullName = fullName });
                }
                reader.Close();
            }
        }

    }
}
