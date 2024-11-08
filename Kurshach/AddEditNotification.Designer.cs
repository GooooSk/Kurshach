namespace Kurshach
{
    partial class AddEditNotification
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
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.saveNotificationButton = new System.Windows.Forms.Button();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.userSelectionListBox = new System.Windows.Forms.CheckedListBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(80, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(330, 25);
            this.label2.TabIndex = 19;
            this.label2.Text = "Добавление/редактирование";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(499, 82);
            this.panel1.TabIndex = 27;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(386, 466);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 37);
            this.button1.TabIndex = 24;
            this.button1.Text = "Назад";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // saveNotificationButton
            // 
            this.saveNotificationButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.saveNotificationButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.saveNotificationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveNotificationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveNotificationButton.Location = new System.Drawing.Point(133, 341);
            this.saveNotificationButton.Name = "saveNotificationButton";
            this.saveNotificationButton.Size = new System.Drawing.Size(215, 37);
            this.saveNotificationButton.TabIndex = 99;
            this.saveNotificationButton.Text = "Сохранить";
            this.saveNotificationButton.UseVisualStyleBackColor = false;
            this.saveNotificationButton.Click += new System.EventHandler(this.saveNotificationButton_Click);
            // 
            // messageTextBox
            // 
            this.messageTextBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.messageTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.messageTextBox.Location = new System.Drawing.Point(133, 152);
            this.messageTextBox.Multiline = true;
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.messageTextBox.Size = new System.Drawing.Size(217, 19);
            this.messageTextBox.TabIndex = 100;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(163, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 24);
            this.label1.TabIndex = 101;
            this.label1.Text = "Уведомление:";
            // 
            // userSelectionListBox
            // 
            this.userSelectionListBox.FormattingEnabled = true;
            this.userSelectionListBox.Location = new System.Drawing.Point(133, 193);
            this.userSelectionListBox.Name = "userSelectionListBox";
            this.userSelectionListBox.Size = new System.Drawing.Size(215, 109);
            this.userSelectionListBox.TabIndex = 102;
            // 
            // AddEditNotification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 515);
            this.Controls.Add(this.userSelectionListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.saveNotificationButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Name = "AddEditNotification";
            this.Text = "AddEditNotification";
            this.Load += new System.EventHandler(this.AddEditNotification_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button saveNotificationButton;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox userSelectionListBox;
    }
}