namespace ShaoComputation
{
    partial class Form1
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
            this.Begin = new System.Windows.Forms.Button();
            this.GA_Button = new System.Windows.Forms.Button();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.ChooseBtn = new System.Windows.Forms.Button();
            this.PathBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Begin
            // 
            this.Begin.Location = new System.Drawing.Point(124, 29);
            this.Begin.Name = "Begin";
            this.Begin.Size = new System.Drawing.Size(75, 23);
            this.Begin.TabIndex = 0;
            this.Begin.Text = "开始计算";
            this.Begin.UseVisualStyleBackColor = true;
            this.Begin.Click += new System.EventHandler(this.Begin_Click);
            // 
            // GA_Button
            // 
            this.GA_Button.Location = new System.Drawing.Point(326, 29);
            this.GA_Button.Name = "GA_Button";
            this.GA_Button.Size = new System.Drawing.Size(75, 23);
            this.GA_Button.TabIndex = 1;
            this.GA_Button.Text = "遗传计算";
            this.GA_Button.UseVisualStyleBackColor = true;
            this.GA_Button.Click += new System.EventHandler(this.GA_Button_Click);
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(124, 132);
            this.messageBox.Multiline = true;
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(334, 151);
            this.messageBox.TabIndex = 2;
            // 
            // ChooseBtn
            // 
            this.ChooseBtn.Location = new System.Drawing.Point(22, 29);
            this.ChooseBtn.Name = "ChooseBtn";
            this.ChooseBtn.Size = new System.Drawing.Size(75, 23);
            this.ChooseBtn.TabIndex = 3;
            this.ChooseBtn.Text = "选择文件";
            this.ChooseBtn.UseVisualStyleBackColor = true;
            this.ChooseBtn.Click += new System.EventHandler(this.ChooseBtn_Click);
            // 
            // PathBox
            // 
            this.PathBox.Location = new System.Drawing.Point(124, 68);
            this.PathBox.Multiline = true;
            this.PathBox.Name = "PathBox";
            this.PathBox.Size = new System.Drawing.Size(334, 45);
            this.PathBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "文件路径";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "计算进度提示";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 335);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PathBox);
            this.Controls.Add(this.ChooseBtn);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.GA_Button);
            this.Controls.Add(this.Begin);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Begin;
        private System.Windows.Forms.Button GA_Button;
        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.Button ChooseBtn;
        private System.Windows.Forms.TextBox PathBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

