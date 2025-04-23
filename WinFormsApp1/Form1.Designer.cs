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
            this.languageLabel = new System.Windows.Forms.Label();
            this.languageComboBox = new System.Windows.Forms.ComboBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.versionLabel = new System.Windows.Forms.Label();
            this.versionListBox = new System.Windows.Forms.ListBox();
            this.customJavaPathCheckBox = new System.Windows.Forms.CheckBox();
            this.javaPathLabel = new System.Windows.Forms.Label();
            this.javaPathTextBox = new System.Windows.Forms.TextBox();
            this.memoryGroupBox = new System.Windows.Forms.GroupBox();
            this.memoryLabel = new System.Windows.Forms.Label();
            this.memorySlider = new System.Windows.Forms.TrackBar();
            this.actionButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.statusLabel = new System.Windows.Forms.Label();
            this.memoryGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memorySlider)).BeginInit();
            this.SuspendLayout();
            // 
            // languageLabel
            // 
            this.languageLabel.AutoSize = true;
            this.languageLabel.Location = new System.Drawing.Point(551, 20);
            this.languageLabel.Name = "languageLabel";
            this.languageLabel.Size = new System.Drawing.Size(87, 13);
            this.languageLabel.TabIndex = 0;
            this.languageLabel.Text = "Select language:";
            // 
            // languageComboBox
            // 
            this.languageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languageComboBox.FormattingEnabled = true;
            this.languageComboBox.Location = new System.Drawing.Point(644, 17);
            this.languageComboBox.Name = "languageComboBox";
            this.languageComboBox.Size = new System.Drawing.Size(121, 21);
            this.languageComboBox.TabIndex = 1;
            this.languageComboBox.SelectedIndexChanged += new System.EventHandler(this.languageComboBox_SelectedIndexChanged);
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(109, 57);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(58, 13);
            this.usernameLabel.TabIndex = 2;
            this.usernameLabel.Text = "Username:";
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(173, 54);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(192, 20);
            this.usernameTextBox.TabIndex = 3;
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(471, 129);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(124, 13);
            this.versionLabel.TabIndex = 4;
            this.versionLabel.Text = "Select Minecraft version:";
            // 
            // versionListBox
            // 
            this.versionListBox.FormattingEnabled = true;
            this.versionListBox.Location = new System.Drawing.Point(601, 129);
            this.versionListBox.Name = "versionListBox";
            this.versionListBox.Size = new System.Drawing.Size(284, 134);
            this.versionListBox.TabIndex = 5;
            this.versionListBox.SelectedIndexChanged += new System.EventHandler(this.versionListBox_SelectedIndexChanged);
            // 
            // customJavaPathCheckBox
            // 
            this.customJavaPathCheckBox.AutoSize = true;
            this.customJavaPathCheckBox.Location = new System.Drawing.Point(572, 277);
            this.customJavaPathCheckBox.Name = "customJavaPathCheckBox";
            this.customJavaPathCheckBox.Size = new System.Drawing.Size(129, 17);
            this.customJavaPathCheckBox.TabIndex = 6;
            this.customJavaPathCheckBox.Text = "Set custom Java path";
            this.customJavaPathCheckBox.UseVisualStyleBackColor = true;
            this.customJavaPathCheckBox.CheckedChanged += new System.EventHandler(this.customJavaPathCheckBox_CheckedChanged);
            // 
            // javaPathLabel
            // 
            this.javaPathLabel.AutoSize = true;
            this.javaPathLabel.Location = new System.Drawing.Point(569, 297);
            this.javaPathLabel.Name = "javaPathLabel";
            this.javaPathLabel.Size = new System.Drawing.Size(57, 13);
            this.javaPathLabel.TabIndex = 7;
            this.javaPathLabel.Text = "Java path:";
            // 
            // javaPathTextBox
            // 
            this.javaPathTextBox.Enabled = false;
            this.javaPathTextBox.Location = new System.Drawing.Point(632, 294);
            this.javaPathTextBox.Name = "javaPathTextBox";
            this.javaPathTextBox.Size = new System.Drawing.Size(224, 20);
            this.javaPathTextBox.TabIndex = 8;
            // 
            // memoryGroupBox
            // 
            this.memoryGroupBox.Controls.Add(this.memoryLabel);
            this.memoryGroupBox.Location = new System.Drawing.Point(572, 313);
            this.memoryGroupBox.Name = "memoryGroupBox";
            this.memoryGroupBox.Size = new System.Drawing.Size(284, 70);
            this.memoryGroupBox.TabIndex = 9;
            this.memoryGroupBox.TabStop = false;
            this.memoryGroupBox.Text = "Memory allocation:";
            // 
            // memoryLabel
            // 
            this.memoryLabel.AutoSize = true;
            this.memoryLabel.Location = new System.Drawing.Point(235, 31);
            this.memoryLabel.Name = "memoryLabel";
            this.memoryLabel.Size = new System.Drawing.Size(31, 13);
            this.memoryLabel.TabIndex = 1;
            this.memoryLabel.Text = "2 GB";
            // 
            // memorySlider
            // 
            this.memorySlider.Location = new System.Drawing.Point(588, 340);
            this.memorySlider.Name = "memorySlider";
            this.memorySlider.Size = new System.Drawing.Size(223, 45);
            this.memorySlider.TabIndex = 0;
            // 
            // actionButton
            // 
            this.actionButton.Enabled = false;
            this.actionButton.Location = new System.Drawing.Point(266, 394);
            this.actionButton.Name = "actionButton";
            this.actionButton.Size = new System.Drawing.Size(284, 30);
            this.actionButton.TabIndex = 10;
            this.actionButton.Text = "Download Version";
            this.actionButton.UseVisualStyleBackColor = true;
            this.actionButton.Click += new System.EventHandler(this.actionButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(15, 430);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(823, 23);
            this.progressBar.TabIndex = 11;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(364, 460);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(41, 13);
            this.statusLabel.TabIndex = 12;
            this.statusLabel.Text = "Ready.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 482);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.memorySlider);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.actionButton);
            this.Controls.Add(this.memoryGroupBox);
            this.Controls.Add(this.javaPathTextBox);
            this.Controls.Add(this.javaPathLabel);
            this.Controls.Add(this.customJavaPathCheckBox);
            this.Controls.Add(this.versionListBox);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.languageComboBox);
            this.Controls.Add(this.languageLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Minecraft Non-Premium Launcher";
            this.memoryGroupBox.ResumeLayout(false);
            this.memoryGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memorySlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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