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
			this.btnStartClashRoyaleQuiz = new System.Windows.Forms.Button();
			this.btnStartAllQuiz = new System.Windows.Forms.Button();
			this.dgvHighscores = new System.Windows.Forms.DataGridView();
			this.lblTitle = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dgvHighscores)).BeginInit();
		          this.SuspendLayout();
		          //
		          // lblTitle
		          //
		          this.lblTitle.AutoSize = true;
		          this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
		          this.lblTitle.Location = new Point(50, 20);
		          this.lblTitle.Name = "lblTitle";
		          this.lblTitle.Size = new Size(200, 28);
		          this.lblTitle.TabIndex = 4;
		          this.lblTitle.Text = "Kies een Quiz";
		          //
		          // btnStartClashQuiz
		          //
		          this.btnStartClashQuiz.BackColor = Color.FromArgb(255, 193, 7);
		          this.btnStartClashQuiz.FlatStyle = FlatStyle.Flat;
		          this.btnStartClashQuiz.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
		          this.btnStartClashQuiz.ForeColor = Color.Black;
		          this.btnStartClashQuiz.Location = new System.Drawing.Point(50, 70);
		          this.btnStartClashQuiz.Name = "btnStartClashQuiz";
		          this.btnStartClashQuiz.Size = new System.Drawing.Size(220, 50);
		          this.btnStartClashQuiz.TabIndex = 0;
		          this.btnStartClashQuiz.Text = "Clash of Clans Quiz";
		          this.btnStartClashQuiz.UseVisualStyleBackColor = false;
		          this.btnStartClashQuiz.Click += new System.EventHandler(this.btnStartClashQuiz_Click);

			// btnStartClashRoyaleQuiz
			//
			this.btnStartClashRoyaleQuiz.BackColor = Color.FromArgb(0, 123, 255);
			this.btnStartClashRoyaleQuiz.FlatStyle = FlatStyle.Flat;
			this.btnStartClashRoyaleQuiz.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
			this.btnStartClashRoyaleQuiz.ForeColor = Color.White;
			this.btnStartClashRoyaleQuiz.Location = new System.Drawing.Point(50, 130);
			this.btnStartClashRoyaleQuiz.Name = "btnStartClashRoyaleQuiz";
			this.btnStartClashRoyaleQuiz.Size = new System.Drawing.Size(220, 50);
			this.btnStartClashRoyaleQuiz.TabIndex = 2;
			this.btnStartClashRoyaleQuiz.Text = "Clash Royale Quiz";
			this.btnStartClashRoyaleQuiz.UseVisualStyleBackColor = false;
			this.btnStartClashRoyaleQuiz.Click += new System.EventHandler(this.btnStartClashRoyaleQuiz_Click);

			// btnStartAllQuiz
			//
			this.btnStartAllQuiz.BackColor = Color.FromArgb(40, 167, 69);
			this.btnStartAllQuiz.FlatStyle = FlatStyle.Flat;
			this.btnStartAllQuiz.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
			this.btnStartAllQuiz.ForeColor = Color.White;
			this.btnStartAllQuiz.Location = new System.Drawing.Point(50, 190);
			this.btnStartAllQuiz.Name = "btnStartAllQuiz";
			this.btnStartAllQuiz.Size = new System.Drawing.Size(220, 50);
			this.btnStartAllQuiz.TabIndex = 3;
			this.btnStartAllQuiz.Text = "Alle Vragen";
			this.btnStartAllQuiz.UseVisualStyleBackColor = false;
			this.btnStartAllQuiz.Click += new System.EventHandler(this.btnStartAllQuiz_Click);

			// dgvHighscores
			//
			this.dgvHighscores.AllowUserToAddRows = false;
			this.dgvHighscores.AllowUserToDeleteRows = false;
			this.dgvHighscores.BackgroundColor = Color.White;
			this.dgvHighscores.BorderStyle = BorderStyle.Fixed3D;
			this.dgvHighscores.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255);
			this.dgvHighscores.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
			this.dgvHighscores.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
			this.dgvHighscores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvHighscores.GridColor = Color.LightGray;
			this.dgvHighscores.ReadOnly = true;
			this.dgvHighscores.RowHeadersVisible = false;
			this.dgvHighscores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgvHighscores.Location = new System.Drawing.Point(300, 30);
			this.dgvHighscores.Name = "dgvHighscores";
			this.dgvHighscores.Size = new System.Drawing.Size(470, 380);
			this.dgvHighscores.TabIndex = 1;
		          //
		          // FormMain
		          //
		          this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
		          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = Color.FromArgb(248, 249, 250);
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.btnStartClashQuiz);
			this.Controls.Add(this.btnStartClashRoyaleQuiz);
			this.Controls.Add(this.btnStartAllQuiz);
			this.Controls.Add(this.dgvHighscores);
		          this.Name = "FormMain";
		          this.Text = "Quizmester - Hoofdmenu";
			this.Load += new System.EventHandler(this.FormMain_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvHighscores)).EndInit();
		          this.ResumeLayout(false);
			this.PerformLayout();
		      }

        #endregion

		private System.Windows.Forms.Button btnStartClashQuiz;
		private System.Windows.Forms.Button btnStartClashRoyaleQuiz;
		private System.Windows.Forms.Button btnStartAllQuiz;
		private System.Windows.Forms.DataGridView dgvHighscores;
		private System.Windows.Forms.Label lblTitle;
    }
}
