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
    public partial class Coach : Form
    {
        public Coach()
        {
            InitializeComponent();
        }

        private void CheckTrainerNotifications()
        {
            string connectionString = Dannie.connectionString;
            string query = @"
        SELECT COUNT(*)
        FROM K_notification_User 
        JOIN K_Notification ON K_Notification.id_notification = K_notification_User.id_notification
        WHERE K_notification_User.id_trener = @TrainerId 
          AND K_Notification.sent_date = @TodayDate";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@TrainerId", Dannie.Id);
                command.Parameters.AddWithValue("@TodayDate", DateTime.Today);

                int notificationCount = (int)command.ExecuteScalar();

                // Меняем текст кнопки в зависимости от наличия уведомлений на сегодня
                notificationButton.Text = notificationCount > 0 ? "Редактировать уведомление" : "Добавить уведомление";
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Autho Auth = new Autho();
            Auth.Show();
            this.Hide();
        }

        private void Coach_Load(object sender, EventArgs e)
        {
            label1.Text = Dannie.FirstName;
            label2.Text = Dannie.LastName;
            CheckTrainerNotifications();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Schelude Schelude = new Schelude();
            Schelude.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Attendance Attendance = new Attendance();
            Attendance.Show();
            this.Hide();
        }

        private void notificationButton_Click(object sender, EventArgs e)
        {
            AddEditNotification AddEditNotification = new AddEditNotification();
            AddEditNotification.Show();
            this.Close();

        }
    }
 }

