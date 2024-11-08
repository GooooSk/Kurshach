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
    public partial class AddEditForm : Form
    {
          private Schelude parentForm; // Ссылка на форму Schelude
    private bool isEditMode;
    private int selectedScheduleId;

    public AddEditForm(Schelude parentForm, bool isEditMode, int scheduleId = 0)
    {
        InitializeComponent();
        this.parentForm = parentForm; // Инициализируем ссылку на форму Schelude
        this.isEditMode = isEditMode;
        this.selectedScheduleId = scheduleId;

        // Устанавливаем заголовок в зависимости от режима
        this.Text = isEditMode ? "Редактирование расписания" : "Добавление расписания";
    }

        public AddEditForm(bool editMode, int scheduleId = -1)
        {
            InitializeComponent();
            isEditMode = editMode;
            selectedScheduleId = scheduleId;
        }



        private void LoadScheduleData(int scheduleId)
        {
            string connectionString = Dannie.connectionString;
            string query = @"
        SELECT 
            K_Schelude.id_schelude,
            K_Section.name AS Секция,
            K_Section.description AS Описание_Секции,
            K_Schelude.time_start AS Время_начала,
            K_Schelude.time_end AS Время_конца,
            K_Schelude.day_of_week AS День_недели
        FROM 
            K_Schelude
        JOIN 
            K_Section ON K_Section.id_section = K_Schelude.id_section
        WHERE 
            K_Schelude.id_schelude = @ScheduleId";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@ScheduleId", scheduleId);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // Заполнение полей формы значениями из базы данных
                    sectionTextbox.Text = reader["Секция"].ToString();
                    textBoxDesc.Text = reader["Описание_Секции"].ToString();
                    startTimeComboBox.Text = reader["Время_начала"].ToString();
                    endTimeComboBox.Text = reader["Время_конца"].ToString();
                    dayOfWeekComboBox.Text = reader["День_недели"].ToString();
                }
                reader.Close();
            }
        }

        private void AddEditForm_Load(object sender, EventArgs e)
        {
            // Устанавливаем текст заголовка
            this.Text = isEditMode ? "Редактирование расписания" : "Добавление расписания";

            // Если это режим редактирования, загружаем данные
            if (isEditMode)
            {
                LoadScheduleData(selectedScheduleId);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Проверка на заполнение всех полей
            if (string.IsNullOrWhiteSpace(sectionTextbox.Text) ||
                string.IsNullOrWhiteSpace(textBoxDesc.Text) ||
                string.IsNullOrWhiteSpace(startTimeComboBox.Text) ||
                string.IsNullOrWhiteSpace(endTimeComboBox.Text) ||
                string.IsNullOrWhiteSpace(dayOfWeekComboBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка заполнения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Преобразование значений ComboBox в TimeSpan для проверки времени
            if (TimeSpan.TryParse(startTimeComboBox.Text, out TimeSpan startTime) &&
                TimeSpan.TryParse(endTimeComboBox.Text, out TimeSpan endTime))
            {
                // Проверка: время начала должно быть раньше времени окончания и не должно совпадать
                if (startTime >= endTime)
                {
                    MessageBox.Show("Время начала должно быть раньше времени окончания и не может совпадать с ним.", "Ошибка времени", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите корректные значения времени.", "Ошибка времени", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Если проверки пройдены, продолжаем сохранение данных
            string connectionString = Dannie.connectionString;
            string query;

            if (isEditMode) // Режим редактирования
            {
                query = @"
            UPDATE K_Schelude
            SET time_start = @TimeStart,
                time_end = @TimeEnd,
                day_of_week = @DayOfWeek,
                Approved = 0 -- Меняем статус на 'На проверке'
            WHERE id_schelude = @ScheduleId;

            UPDATE K_Section
            SET name = @SectionName,
                description = @SectionDesc
            WHERE id_section = (
                SELECT id_section FROM K_Schelude WHERE id_schelude = @ScheduleId
            );";
            }
            else // Режим добавления
            {
                query = @"
            INSERT INTO K_Section (name, description, id_trener)
            VALUES (@SectionName, @SectionDesc, @TrainerId);

            INSERT INTO K_Schelude (id_section, time_start, time_end, day_of_week, Approved)
            VALUES (
                (SELECT TOP 1 id_section FROM K_Section ORDER BY id_section DESC),
                @TimeStart, @TimeEnd, @DayOfWeek, 0 -- Новый статус 'На проверке'
            );";
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);

                // Параметры для секции
                command.Parameters.AddWithValue("@SectionName", sectionTextbox.Text);
                command.Parameters.AddWithValue("@SectionDesc", textBoxDesc.Text);
                command.Parameters.AddWithValue("@TrainerId", Dannie.Id); // Установка ID тренера

                // Параметры для расписания
                command.Parameters.AddWithValue("@TimeStart", startTimeComboBox.Text);
                command.Parameters.AddWithValue("@TimeEnd", endTimeComboBox.Text);
                command.Parameters.AddWithValue("@DayOfWeek", dayOfWeekComboBox.Text);

                if (isEditMode)
                {
                    command.Parameters.AddWithValue("@ScheduleId", selectedScheduleId);
                }

                command.ExecuteNonQuery();
            }

            MessageBox.Show("Данные успешно сохранены.");
            this.Close();
        }
    }
}
