﻿namespace Kurshach
{
    partial class SchludeApproval
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.sectionsComboBox = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dayOfWeekComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.approvedCheckBox = new System.Windows.Forms.CheckBox();
            this.notApprovedCheckBox = new System.Windows.Forms.CheckBox();
            this.trainersComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Код_расписания = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Тренер = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Секция = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Описание_секции = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Время_начала = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Время_конца = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.День_недели = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Одобрено = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.Location = new System.Drawing.Point(14, 573);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(119, 43);
            this.button4.TabIndex = 106;
            this.button4.Text = "Одобрить";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button5.Location = new System.Drawing.Point(25, 333);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(99, 30);
            this.button5.TabIndex = 97;
            this.button5.Text = "Очистить";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // sectionsComboBox
            // 
            this.sectionsComboBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.sectionsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sectionsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.sectionsComboBox.FormattingEnabled = true;
            this.sectionsComboBox.Items.AddRange(new object[] {
            ""});
            this.sectionsComboBox.Location = new System.Drawing.Point(12, 120);
            this.sectionsComboBox.Name = "sectionsComboBox";
            this.sectionsComboBox.Size = new System.Drawing.Size(115, 21);
            this.sectionsComboBox.TabIndex = 97;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.Location = new System.Drawing.Point(12, 285);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(122, 30);
            this.button3.TabIndex = 96;
            this.button3.Text = "Фильтровать";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(24, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 40);
            this.label5.TabIndex = 93;
            this.label5.Text = "Выбор дня\r\n   недели";
            // 
            // dayOfWeekComboBox
            // 
            this.dayOfWeekComboBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dayOfWeekComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dayOfWeekComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dayOfWeekComboBox.FormattingEnabled = true;
            this.dayOfWeekComboBox.Items.AddRange(new object[] {
            "Понедельник",
            "Вторник",
            "Среда",
            "Четверг",
            "Пятница",
            "Суббота",
            "Воскресенье"});
            this.dayOfWeekComboBox.Location = new System.Drawing.Point(12, 55);
            this.dayOfWeekComboBox.Name = "dayOfWeekComboBox";
            this.dayOfWeekComboBox.Size = new System.Drawing.Size(115, 21);
            this.dayOfWeekComboBox.TabIndex = 90;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(39, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 20);
            this.label4.TabIndex = 91;
            this.label4.Text = "Секции";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(48, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 20);
            this.label6.TabIndex = 104;
            this.label6.Text = "Фильтры";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel3.Controls.Add(this.approvedCheckBox);
            this.panel3.Controls.Add(this.notApprovedCheckBox);
            this.panel3.Controls.Add(this.trainersComboBox);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.button5);
            this.panel3.Controls.Add(this.sectionsComboBox);
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.dayOfWeekComboBox);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(24, 152);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(148, 382);
            this.panel3.TabIndex = 103;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // approvedCheckBox
            // 
            this.approvedCheckBox.AutoSize = true;
            this.approvedCheckBox.Location = new System.Drawing.Point(28, 253);
            this.approvedCheckBox.Name = "approvedCheckBox";
            this.approvedCheckBox.Size = new System.Drawing.Size(90, 17);
            this.approvedCheckBox.TabIndex = 101;
            this.approvedCheckBox.Text = "Одобренные";
            this.approvedCheckBox.UseVisualStyleBackColor = true;
            this.approvedCheckBox.CheckedChanged += new System.EventHandler(this.approvedCheckBox_CheckedChanged);
            // 
            // notApprovedCheckBox
            // 
            this.notApprovedCheckBox.AutoSize = true;
            this.notApprovedCheckBox.Location = new System.Drawing.Point(22, 230);
            this.notApprovedCheckBox.Name = "notApprovedCheckBox";
            this.notApprovedCheckBox.Size = new System.Drawing.Size(105, 17);
            this.notApprovedCheckBox.TabIndex = 100;
            this.notApprovedCheckBox.Text = "Не одобренные";
            this.notApprovedCheckBox.UseVisualStyleBackColor = true;
            this.notApprovedCheckBox.CheckedChanged += new System.EventHandler(this.notApprovedCheckBox_CheckedChanged);
            // 
            // trainersComboBox
            // 
            this.trainersComboBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.trainersComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.trainersComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.trainersComboBox.FormattingEnabled = true;
            this.trainersComboBox.Items.AddRange(new object[] {
            ""});
            this.trainersComboBox.Location = new System.Drawing.Point(9, 193);
            this.trainersComboBox.Name = "trainersComboBox";
            this.trainersComboBox.Size = new System.Drawing.Size(115, 21);
            this.trainersComboBox.TabIndex = 99;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(36, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 98;
            this.label1.Text = "Тренер";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Код_расписания,
            this.Тренер,
            this.Секция,
            this.Описание_секции,
            this.Время_начала,
            this.Время_конца,
            this.День_недели,
            this.Одобрено});
            this.dataGridView1.Location = new System.Drawing.Point(203, 152);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(913, 382);
            this.dataGridView1.TabIndex = 102;
            // 
            // Код_расписания
            // 
            this.Код_расписания.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Код_расписания.HeaderText = "Код_расписания";
            this.Код_расписания.Name = "Код_расписания";
            // 
            // Тренер
            // 
            this.Тренер.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Тренер.HeaderText = "Тренер";
            this.Тренер.Name = "Тренер";
            // 
            // Секция
            // 
            this.Секция.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Секция.HeaderText = "Секция";
            this.Секция.Name = "Секция";
            // 
            // Описание_секции
            // 
            this.Описание_секции.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Описание_секции.HeaderText = "Описание_секции";
            this.Описание_секции.Name = "Описание_секции";
            // 
            // Время_начала
            // 
            this.Время_начала.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Время_начала.HeaderText = "Время_начала";
            this.Время_начала.Name = "Время_начала";
            // 
            // Время_конца
            // 
            this.Время_конца.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Время_конца.HeaderText = "Время_конца";
            this.Время_конца.Name = "Время_конца";
            // 
            // День_недели
            // 
            this.День_недели.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.День_недели.HeaderText = "День_недели";
            this.День_недели.Name = "День_недели";
            // 
            // Одобрено
            // 
            this.Одобрено.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Одобрено.HeaderText = "Одобрено";
            this.Одобрено.Name = "Одобрено";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(511, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 25);
            this.label2.TabIndex = 18;
            this.label2.Text = "Расписание";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1147, 74);
            this.panel1.TabIndex = 101;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(1032, 573);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 46);
            this.button1.TabIndex = 100;
            this.button1.Text = "Назад";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SchludeApproval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 631);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Name = "SchludeApproval";
            this.Text = "SchludeApproval";
            this.Load += new System.EventHandler(this.SchludeApproval_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox sectionsComboBox;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox dayOfWeekComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Код_расписания;
        private System.Windows.Forms.DataGridViewTextBoxColumn Тренер;
        private System.Windows.Forms.DataGridViewTextBoxColumn Секция;
        private System.Windows.Forms.DataGridViewTextBoxColumn Описание_секции;
        private System.Windows.Forms.DataGridViewTextBoxColumn Время_начала;
        private System.Windows.Forms.DataGridViewTextBoxColumn Время_конца;
        private System.Windows.Forms.DataGridViewTextBoxColumn День_недели;
        private System.Windows.Forms.DataGridViewTextBoxColumn Одобрено;
        private System.Windows.Forms.ComboBox trainersComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox notApprovedCheckBox;
        private System.Windows.Forms.CheckBox approvedCheckBox;
    }
}