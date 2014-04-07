namespace FileSearching
{
    partial class FileSearchingForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFolderBrowserDirectoryButton = new System.Windows.Forms.Button();
            this.startDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.fileNameTemplateTextBox = new System.Windows.Forms.TextBox();
            this.textInFileTextBox = new System.Windows.Forms.TextBox();
            this.startDirectoryLabel = new System.Windows.Forms.Label();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.textInFileLabel = new System.Windows.Forms.Label();
            this.searchButton = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.suspendButton = new System.Windows.Forms.Button();
            this.timeLabel = new System.Windows.Forms.Label();
            this.currentHandlingFileTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numberOfHandledFilesLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // openFolderBrowserDirectoryButton
            // 
            this.openFolderBrowserDirectoryButton.Location = new System.Drawing.Point(502, 12);
            this.openFolderBrowserDirectoryButton.Name = "openFolderBrowserDirectoryButton";
            this.openFolderBrowserDirectoryButton.Size = new System.Drawing.Size(98, 23);
            this.openFolderBrowserDirectoryButton.TabIndex = 0;
            this.openFolderBrowserDirectoryButton.Text = "Выбрать директорию";
            this.openFolderBrowserDirectoryButton.UseVisualStyleBackColor = true;
            this.openFolderBrowserDirectoryButton.Click += new System.EventHandler(this.openFolderBrowserDirectoryButton_Click);
            // 
            // startDirectoryTextBox
            // 
            this.startDirectoryTextBox.Location = new System.Drawing.Point(123, 15);
            this.startDirectoryTextBox.Name = "startDirectoryTextBox";
            this.startDirectoryTextBox.Size = new System.Drawing.Size(368, 20);
            this.startDirectoryTextBox.TabIndex = 1;
            this.startDirectoryTextBox.TextChanged += new System.EventHandler(this.startDirectoryTextBox_TextChanged);
            // 
            // fileNameTemplateTextBox
            // 
            this.fileNameTemplateTextBox.Location = new System.Drawing.Point(123, 49);
            this.fileNameTemplateTextBox.Name = "fileNameTemplateTextBox";
            this.fileNameTemplateTextBox.Size = new System.Drawing.Size(368, 20);
            this.fileNameTemplateTextBox.TabIndex = 2;
            this.fileNameTemplateTextBox.TextChanged += new System.EventHandler(this.fileNameTemplateTextBox_TextChanged);
            // 
            // textInFileTextBox
            // 
            this.textInFileTextBox.Location = new System.Drawing.Point(123, 93);
            this.textInFileTextBox.Name = "textInFileTextBox";
            this.textInFileTextBox.Size = new System.Drawing.Size(368, 20);
            this.textInFileTextBox.TabIndex = 3;
            this.textInFileTextBox.TextChanged += new System.EventHandler(this.textInFileTextBox_TextChanged);
            // 
            // startDirectoryLabel
            // 
            this.startDirectoryLabel.AutoSize = true;
            this.startDirectoryLabel.Location = new System.Drawing.Point(1, 18);
            this.startDirectoryLabel.Name = "startDirectoryLabel";
            this.startDirectoryLabel.Size = new System.Drawing.Size(95, 13);
            this.startDirectoryLabel.TabIndex = 4;
            this.startDirectoryLabel.Text = "Текущий каталог";
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Location = new System.Drawing.Point(1, 56);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(116, 13);
            this.fileNameLabel.TabIndex = 5;
            this.fileNameLabel.Text = "Имя файла содержит";
            // 
            // textInFileLabel
            // 
            this.textInFileLabel.AutoSize = true;
            this.textInFileLabel.Location = new System.Drawing.Point(1, 96);
            this.textInFileLabel.Name = "textInFileLabel";
            this.textInFileLabel.Size = new System.Drawing.Size(88, 13);
            this.textInFileLabel.TabIndex = 6;
            this.textInFileLabel.Text = "Файл содержит";
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(12, 119);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(98, 23);
            this.searchButton.TabIndex = 7;
            this.searchButton.Text = "Поиск";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(21, 213);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(615, 475);
            this.treeView1.TabIndex = 8;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // suspendButton
            // 
            this.suspendButton.Location = new System.Drawing.Point(123, 119);
            this.suspendButton.Name = "suspendButton";
            this.suspendButton.Size = new System.Drawing.Size(98, 23);
            this.suspendButton.TabIndex = 9;
            this.suspendButton.Text = "Приостановить";
            this.suspendButton.UseVisualStyleBackColor = true;
            this.suspendButton.Click += new System.EventHandler(this.suspendButton_Click);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(341, 124);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(13, 13);
            this.timeLabel.TabIndex = 10;
            this.timeLabel.Text = "0";
            // 
            // currentHandlingFileTextBox
            // 
            this.currentHandlingFileTextBox.Location = new System.Drawing.Point(12, 170);
            this.currentHandlingFileTextBox.Name = "currentHandlingFileTextBox";
            this.currentHandlingFileTextBox.Size = new System.Drawing.Size(935, 20);
            this.currentHandlingFileTextBox.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(236, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Прошедшее время";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(395, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Число обработанных файлов";
            // 
            // numberOfHandledFilesLabel
            // 
            this.numberOfHandledFilesLabel.AutoSize = true;
            this.numberOfHandledFilesLabel.Location = new System.Drawing.Point(556, 126);
            this.numberOfHandledFilesLabel.Name = "numberOfHandledFilesLabel";
            this.numberOfHandledFilesLabel.Size = new System.Drawing.Size(13, 13);
            this.numberOfHandledFilesLabel.TabIndex = 14;
            this.numberOfHandledFilesLabel.Text = "0";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FileSearchingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 700);
            this.Controls.Add(this.numberOfHandledFilesLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.currentHandlingFileTextBox);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.suspendButton);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.textInFileLabel);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.startDirectoryLabel);
            this.Controls.Add(this.textInFileTextBox);
            this.Controls.Add(this.fileNameTemplateTextBox);
            this.Controls.Add(this.startDirectoryTextBox);
            this.Controls.Add(this.openFolderBrowserDirectoryButton);
            this.Name = "FileSearchingForm";
            this.Text = "Searching Files";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileSearchingForm_FormClosing);
            this.Load += new System.EventHandler(this.FileSearchingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button openFolderBrowserDirectoryButton;
        private System.Windows.Forms.TextBox startDirectoryTextBox;
        private System.Windows.Forms.TextBox fileNameTemplateTextBox;
        private System.Windows.Forms.TextBox textInFileTextBox;
        private System.Windows.Forms.Label startDirectoryLabel;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Label textInFileLabel;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button suspendButton;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.TextBox currentHandlingFileTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label numberOfHandledFilesLabel;
        private System.Windows.Forms.Timer timer1;
    }
}

