using System.Drawing;
using System.Windows.Forms;

namespace Quizmester
{
    partial class FormMain
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
            this.btnStartClashQuiz = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartClashQuiz
            // 
            this.btnStartClashQuiz.Location = new System.Drawing.Point(50, 49);
            this.btnStartClashQuiz.Name = "btnStartClashQuiz";
            this.btnStartClashQuiz.Size = new System.Drawing.Size(201, 29);
            this.btnStartClashQuiz.TabIndex = 0;
            this.btnStartClashQuiz.Text = "Clash of Clans Quiz";
            this.btnStartClashQuiz.UseVisualStyleBackColor = true;
            this.btnStartClashQuiz.Click += new System.EventHandler(this.btnStartClashQuiz_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnStartClashQuiz);
            this.Name = "FormMain";
            this.Text = "Quizmester - Hoofdmenu";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnStartClashQuiz;
    }
}
