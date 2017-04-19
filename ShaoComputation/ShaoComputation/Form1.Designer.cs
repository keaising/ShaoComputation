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
            this.SuspendLayout();
            // 
            // Begin
            // 
            this.Begin.Location = new System.Drawing.Point(54, 29);
            this.Begin.Name = "Begin";
            this.Begin.Size = new System.Drawing.Size(75, 23);
            this.Begin.TabIndex = 0;
            this.Begin.Text = "开始计算";
            this.Begin.UseVisualStyleBackColor = true;
            this.Begin.Click += new System.EventHandler(this.Begin_Click);
            // 
            // GA_Button
            // 
            this.GA_Button.Location = new System.Drawing.Point(230, 29);
            this.GA_Button.Name = "GA_Button";
            this.GA_Button.Size = new System.Drawing.Size(75, 23);
            this.GA_Button.TabIndex = 1;
            this.GA_Button.Text = "遗传计算";
            this.GA_Button.UseVisualStyleBackColor = true;
            this.GA_Button.Click += new System.EventHandler(this.GA_Button_Click);
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(54, 87);
            this.messageBox.Multiline = true;
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(251, 129);
            this.messageBox.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 260);
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
    }
}

