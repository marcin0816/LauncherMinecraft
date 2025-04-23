namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            languageLabel = new Label();
            languageComboBox = new ComboBox();
            usernameLabel = new Label();
            usernameTextBox = new TextBox();
            versionLabel = new Label();
            versionListBox = new ListBox();
            customJavaPathCheckBox = new CheckBox();
            javaPathLabel = new Label();
            javaPathTextBox = new TextBox();
            memoryGroupBox = new GroupBox();
            memoryLabel = new Label();
            memorySlider = new TrackBar();
            actionButton = new Button();
            progressBar = new ProgressBar();
            statusLabel = new Label();
            memoryGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)memorySlider).BeginInit();
            SuspendLayout();
            // 
            // languageLabel
            // 
            languageLabel.AutoSize = true;
            languageLabel.Location = new Point(643, 23);
            languageLabel.Margin = new Padding(4, 0, 4, 0);
            languageLabel.Name = "languageLabel";
            languageLabel.Size = new Size(93, 15);
            languageLabel.TabIndex = 0;
            languageLabel.Text = "Select language:";
            // 
            // languageComboBox
            // 
            languageComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            languageComboBox.FormattingEnabled = true;
            languageComboBox.Location = new Point(751, 20);
            languageComboBox.Margin = new Padding(4, 3, 4, 3);
            languageComboBox.Name = "languageComboBox";
            languageComboBox.Size = new Size(140, 23);
            languageComboBox.TabIndex = 1;
            languageComboBox.SelectedIndexChanged += languageComboBox_SelectedIndexChanged;
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new Point(127, 66);
            usernameLabel.Margin = new Padding(4, 0, 4, 0);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(63, 15);
            usernameLabel.TabIndex = 2;
            usernameLabel.Text = "Username:";
            // 
            // usernameTextBox
            // 
            usernameTextBox.Location = new Point(202, 62);
            usernameTextBox.Margin = new Padding(4, 3, 4, 3);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(223, 23);
            usernameTextBox.TabIndex = 3;
            // 
            // versionLabel
            // 
            versionLabel.AutoSize = true;
            versionLabel.Location = new Point(550, 149);
            versionLabel.Margin = new Padding(4, 0, 4, 0);
            versionLabel.Name = "versionLabel";
            versionLabel.Size = new Size(136, 15);
            versionLabel.TabIndex = 4;
            versionLabel.Text = "Select Minecraft version:";
            // 
            // versionListBox
            // 
            versionListBox.FormattingEnabled = true;
            versionListBox.ItemHeight = 15;
            versionListBox.Location = new Point(701, 149);
            versionListBox.Margin = new Padding(4, 3, 4, 3);
            versionListBox.Name = "versionListBox";
            versionListBox.Size = new Size(331, 154);
            versionListBox.TabIndex = 5;
            versionListBox.SelectedIndexChanged += versionListBox_SelectedIndexChanged;
            // 
            // customJavaPathCheckBox
            // 
            customJavaPathCheckBox.AutoSize = true;
            customJavaPathCheckBox.Location = new Point(667, 320);
            customJavaPathCheckBox.Margin = new Padding(4, 3, 4, 3);
            customJavaPathCheckBox.Name = "customJavaPathCheckBox";
            customJavaPathCheckBox.Size = new Size(137, 19);
            customJavaPathCheckBox.TabIndex = 6;
            customJavaPathCheckBox.Text = "Set custom Java path";
            customJavaPathCheckBox.UseVisualStyleBackColor = true;
            customJavaPathCheckBox.CheckedChanged += customJavaPathCheckBox_CheckedChanged;
            // 
            // javaPathLabel
            // 
            javaPathLabel.AutoSize = true;
            javaPathLabel.Location = new Point(664, 343);
            javaPathLabel.Margin = new Padding(4, 0, 4, 0);
            javaPathLabel.Name = "javaPathLabel";
            javaPathLabel.Size = new Size(59, 15);
            javaPathLabel.TabIndex = 7;
            javaPathLabel.Text = "Java path:";
            // 
            // javaPathTextBox
            // 
            javaPathTextBox.Enabled = false;
            javaPathTextBox.Location = new Point(737, 339);
            javaPathTextBox.Margin = new Padding(4, 3, 4, 3);
            javaPathTextBox.Name = "javaPathTextBox";
            javaPathTextBox.Size = new Size(261, 23);
            javaPathTextBox.TabIndex = 8;
            // 
            // memoryGroupBox
            // 
            memoryGroupBox.Controls.Add(memoryLabel);
            memoryGroupBox.Controls.Add(memorySlider);
            memoryGroupBox.Location = new Point(667, 368);
            memoryGroupBox.Margin = new Padding(4, 3, 4, 3);
            memoryGroupBox.Name = "memoryGroupBox";
            memoryGroupBox.Padding = new Padding(4, 3, 4, 3);
            memoryGroupBox.Size = new Size(331, 74);
            memoryGroupBox.TabIndex = 9;
            memoryGroupBox.TabStop = false;
            memoryGroupBox.Text = "Memory allocation:";
            memoryGroupBox.Enter += memoryGroupBox_Enter;
            // 
            // memoryLabel
            // 
            memoryLabel.AutoSize = true;
            memoryLabel.Location = new Point(274, 36);
            memoryLabel.Margin = new Padding(4, 0, 4, 0);
            memoryLabel.Name = "memoryLabel";
            memoryLabel.Size = new Size(31, 15);
            memoryLabel.TabIndex = 1;
            memoryLabel.Text = "2 GB";
            // 
            // memorySlider
            // 
            memorySlider.Location = new Point(6, 36);
            memorySlider.Margin = new Padding(4, 3, 4, 3);
            memorySlider.Name = "memorySlider";
            memorySlider.Size = new Size(260, 45);
            memorySlider.TabIndex = 0;
            // 
            // actionButton
            // 
            actionButton.Enabled = false;
            actionButton.Location = new Point(310, 455);
            actionButton.Margin = new Padding(4, 3, 4, 3);
            actionButton.Name = "actionButton";
            actionButton.Size = new Size(331, 35);
            actionButton.TabIndex = 10;
            actionButton.Text = "Download Version";
            actionButton.UseVisualStyleBackColor = true;
            actionButton.Click += actionButton_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(18, 496);
            progressBar.Margin = new Padding(4, 3, 4, 3);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(960, 27);
            progressBar.TabIndex = 11;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(425, 531);
            statusLabel.Margin = new Padding(4, 0, 4, 0);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(42, 15);
            statusLabel.TabIndex = 12;
            statusLabel.Text = "Ready.";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1046, 556);
            Controls.Add(statusLabel);
            Controls.Add(progressBar);
            Controls.Add(actionButton);
            Controls.Add(memoryGroupBox);
            Controls.Add(javaPathTextBox);
            Controls.Add(javaPathLabel);
            Controls.Add(customJavaPathCheckBox);
            Controls.Add(versionListBox);
            Controls.Add(versionLabel);
            Controls.Add(usernameTextBox);
            Controls.Add(usernameLabel);
            Controls.Add(languageComboBox);
            Controls.Add(languageLabel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Minecraft Non-Premium Launcher";
            memoryGroupBox.ResumeLayout(false);
            memoryGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)memorySlider).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label languageLabel;
        private System.Windows.Forms.ComboBox languageComboBox;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.ListBox versionListBox;
        private System.Windows.Forms.CheckBox customJavaPathCheckBox;
        private System.Windows.Forms.Label javaPathLabel;
        private System.Windows.Forms.TextBox javaPathTextBox;
        private System.Windows.Forms.GroupBox memoryGroupBox;
        private System.Windows.Forms.Label memoryLabel;
        private System.Windows.Forms.TrackBar memorySlider;
        private System.Windows.Forms.Button actionButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label statusLabel;
    }
}