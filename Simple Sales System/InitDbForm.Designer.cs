namespace Simple_Sales_System
{
    partial class InitDbForm
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
            this.TestBtn = new System.Windows.Forms.Button();
            this.InitBtn = new System.Windows.Forms.Button();
            this.EnterBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.StringTB = new System.Windows.Forms.TextBox();
            this.TaskProgress = new System.Windows.Forms.ProgressBar();
            this.MessageTB = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TestBtn
            // 
            this.TestBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TestBtn.Location = new System.Drawing.Point(13, 165);
            this.TestBtn.Name = "TestBtn";
            this.TestBtn.Size = new System.Drawing.Size(160, 47);
            this.TestBtn.TabIndex = 0;
            this.TestBtn.Text = "Test Connection";
            this.TestBtn.UseVisualStyleBackColor = true;
            this.TestBtn.Click += new System.EventHandler(this.TestBtn_Click);
            // 
            // InitBtn
            // 
            this.InitBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.InitBtn.Location = new System.Drawing.Point(273, 165);
            this.InitBtn.Name = "InitBtn";
            this.InitBtn.Size = new System.Drawing.Size(160, 47);
            this.InitBtn.TabIndex = 1;
            this.InitBtn.Text = "Init Database";
            this.InitBtn.UseVisualStyleBackColor = true;
            this.InitBtn.Click += new System.EventHandler(this.InitBtn_Click);
            // 
            // EnterBtn
            // 
            this.EnterBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.EnterBtn.Enabled = false;
            this.EnterBtn.Location = new System.Drawing.Point(561, 165);
            this.EnterBtn.Name = "EnterBtn";
            this.EnterBtn.Size = new System.Drawing.Size(160, 47);
            this.EnterBtn.TabIndex = 2;
            this.EnterBtn.Text = "Enter";
            this.EnterBtn.UseVisualStyleBackColor = true;
            this.EnterBtn.Click += new System.EventHandler(this.EnterBtn_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "ConnectionString";
            // 
            // StringTB
            // 
            this.StringTB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StringTB.Location = new System.Drawing.Point(160, 42);
            this.StringTB.Name = "StringTB";
            this.StringTB.Size = new System.Drawing.Size(557, 25);
            this.StringTB.TabIndex = 5;
            // 
            // TaskProgress
            // 
            this.TaskProgress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TaskProgress.Location = new System.Drawing.Point(12, 85);
            this.TaskProgress.Name = "TaskProgress";
            this.TaskProgress.Size = new System.Drawing.Size(704, 27);
            this.TaskProgress.TabIndex = 6;
            // 
            // MessageTB
            // 
            this.MessageTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.MessageTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MessageTB.Location = new System.Drawing.Point(12, 128);
            this.MessageTB.Name = "MessageTB";
            this.MessageTB.ReadOnly = true;
            this.MessageTB.Size = new System.Drawing.Size(704, 25);
            this.MessageTB.TabIndex = 7;
            this.MessageTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.MessageTB);
            this.panel1.Controls.Add(this.TaskProgress);
            this.panel1.Controls.Add(this.StringTB);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.EnterBtn);
            this.panel1.Controls.Add(this.InitBtn);
            this.panel1.Controls.Add(this.TestBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(733, 224);
            this.panel1.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.OrangeRed;
            this.label2.Location = new System.Drawing.Point(48, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(598, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "For using this app , you should provide a connection string";
            // 
            // InitDbForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 224);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(751, 271);
            this.MinimumSize = new System.Drawing.Size(751, 271);
            this.Name = "InitDbForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InitDbForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TestBtn;
        private System.Windows.Forms.Button InitBtn;
        private System.Windows.Forms.Button EnterBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox StringTB;
        private System.Windows.Forms.ProgressBar TaskProgress;
        private System.Windows.Forms.TextBox MessageTB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
    }
}