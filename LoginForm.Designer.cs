namespace kargotakipsistemi
{
    partial class LoginForm
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
            panel1 = new Panel();
            label3 = new Label();
            panel3 = new Panel();
            panel5 = new Panel();
            tb_loginSifre = new TextBox();
            label2 = new Label();
            panel2 = new Panel();
            btn_login = new Button();
            panel4 = new Panel();
            label1 = new Label();
            tb_loginEposta = new TextBox();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel5.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label3);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(264, 65);
            panel1.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Georgia", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(48, 27);
            label3.Name = "label3";
            label3.Size = new Size(161, 31);
            label3.TabIndex = 5;
            label3.Text = "Giriş Ekranı";
            // 
            // panel3
            // 
            panel3.Controls.Add(panel5);
            panel3.Controls.Add(panel2);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 65);
            panel3.Name = "panel3";
            panel3.Size = new Size(264, 171);
            panel3.TabIndex = 2;
            // 
            // panel5
            // 
            panel5.Controls.Add(tb_loginSifre);
            panel5.Controls.Add(label2);
            panel5.Location = new Point(12, 75);
            panel5.Name = "panel5";
            panel5.Size = new Size(237, 46);
            panel5.TabIndex = 8;
            // 
            // tb_loginSifre
            // 
            tb_loginSifre.BorderStyle = BorderStyle.FixedSingle;
            tb_loginSifre.Dock = DockStyle.Bottom;
            tb_loginSifre.Font = new Font("Consolas", 12F);
            tb_loginSifre.Location = new Point(0, 20);
            tb_loginSifre.MaxLength = 20;
            tb_loginSifre.Name = "tb_loginSifre";
            tb_loginSifre.PasswordChar = '*';
            tb_loginSifre.Size = new Size(237, 26);
            tb_loginSifre.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Consolas", 12F);
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(54, 19);
            label2.TabIndex = 9;
            label2.Text = "şifre";
            // 
            // panel2
            // 
            panel2.Controls.Add(btn_login);
            panel2.Controls.Add(panel4);
            panel2.Location = new Point(12, 6);
            panel2.Name = "panel2";
            panel2.Size = new Size(240, 153);
            panel2.TabIndex = 6;
            // 
            // btn_login
            // 
            btn_login.BackColor = Color.Lavender;
            btn_login.FlatAppearance.BorderColor = Color.Gray;
            btn_login.FlatStyle = FlatStyle.Flat;
            btn_login.Location = new Point(140, 127);
            btn_login.Name = "btn_login";
            btn_login.Size = new Size(100, 26);
            btn_login.TabIndex = 3;
            btn_login.Text = "Giriş yap";
            btn_login.UseVisualStyleBackColor = false;
            btn_login.Click += btn_login_Click;
            // 
            // panel4
            // 
            panel4.Controls.Add(label1);
            panel4.Controls.Add(tb_loginEposta);
            panel4.Location = new Point(0, 17);
            panel4.Name = "panel4";
            panel4.Size = new Size(237, 46);
            panel4.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Consolas", 12F);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(153, 19);
            label1.TabIndex = 8;
            label1.Text = "personel e posta";
            // 
            // tb_loginEposta
            // 
            tb_loginEposta.BorderStyle = BorderStyle.FixedSingle;
            tb_loginEposta.Dock = DockStyle.Bottom;
            tb_loginEposta.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            tb_loginEposta.Location = new Point(0, 20);
            tb_loginEposta.MaxLength = 50;
            tb_loginEposta.Name = "tb_loginEposta";
            tb_loginEposta.Size = new Size(237, 26);
            tb_loginEposta.TabIndex = 1;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(264, 236);
            Controls.Add(panel3);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoginForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel3;
        private Label label3;
        private TextBox tb_loginEposta;
        private Panel panel5;
        private Panel panel2;
        private Panel panel4;
        private Label label2;
        private Label label1;
        private Button btn_login;
        private TextBox tb_loginSifre;
    }
}