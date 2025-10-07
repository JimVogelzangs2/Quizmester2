using System.Windows.Forms;
using System.Drawing;

namespace Quizmester
{
    partial class FormAdminQuestions
    {
        private DataGridView dgvQuestions;
        private TextBox txtQuestion;
        private TextBox txtCorrect;
        private TextBox txtFalse1;
        private TextBox txtFalse2;
        private TextBox txtFalse3;
        private TextBox txtType;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnBack;
        private Label lblQuestion;
        private Label lblCorrect;
        private Label lblFalse1;
        private Label lblFalse2;
        private Label lblFalse3;
        private Label lblType;

        private void InitializeComponent()
        {
            this.dgvQuestions = new DataGridView();
            this.txtQuestion = new TextBox();
            this.txtCorrect = new TextBox();
            this.txtFalse1 = new TextBox();
            this.txtFalse2 = new TextBox();
            this.txtFalse3 = new TextBox();
            this.txtType = new TextBox();
            this.btnAdd = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.btnBack = new Button();
            this.lblQuestion = new Label();
            this.lblCorrect = new Label();
            this.lblFalse1 = new Label();
            this.lblFalse2 = new Label();
            this.lblFalse3 = new Label();
            this.lblType = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuestions)).BeginInit();
            this.SuspendLayout();

            // 
            // dgvQuestions
            // 
            this.dgvQuestions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQuestions.Location = new Point(12, 12);
            this.dgvQuestions.Name = "dgvQuestions";
            this.dgvQuestions.Size = new Size(776, 300);
            this.dgvQuestions.TabIndex = 0;
            this.dgvQuestions.SelectionChanged += new System.EventHandler(this.dgvQuestions_SelectionChanged);

            // 
            // txtQuestion
            // 
            this.txtQuestion.Location = new Point(150, 330);
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.Size = new Size(400, 27);
            this.txtQuestion.TabIndex = 1;

            // 
            // txtCorrect
            // 
            this.txtCorrect.Location = new Point(150, 370);
            this.txtCorrect.Name = "txtCorrect";
            this.txtCorrect.Size = new Size(400, 27);
            this.txtCorrect.TabIndex = 2;

            // 
            // txtFalse1
            // 
            this.txtFalse1.Location = new Point(150, 410);
            this.txtFalse1.Name = "txtFalse1";
            this.txtFalse1.Size = new Size(400, 27);
            this.txtFalse1.TabIndex = 3;

            // 
            // txtFalse2
            // 
            this.txtFalse2.Location = new Point(150, 450);
            this.txtFalse2.Name = "txtFalse2";
            this.txtFalse2.Size = new Size(400, 27);
            this.txtFalse2.TabIndex = 4;

            // 
            // txtFalse3
            // 
            this.txtFalse3.Location = new Point(150, 490);
            this.txtFalse3.Name = "txtFalse3";
            this.txtFalse3.Size = new Size(400, 27);
            this.txtFalse3.TabIndex = 5;

            // 
            // txtType
            // 
            this.txtType.Location = new Point(150, 530);
            this.txtType.Name = "txtType";
            this.txtType.Size = new Size(200, 27);
            this.txtType.TabIndex = 6;

            // 
            // btnAdd
            // 
            this.btnAdd.Location = new Point(600, 330);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(100, 35);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Toevoegen";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // 
            // btnEdit
            // 
            this.btnEdit.Location = new Point(600, 380);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(100, 35);
            this.btnEdit.TabIndex = 8;
            this.btnEdit.Text = "Bewerken";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            // 
            // btnDelete
            // 
            this.btnDelete.Location = new Point(600, 430);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(100, 35);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Verwijderen";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // 
            // btnBack
            // 
            this.btnBack.Location = new Point(600, 480);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new Size(100, 35);
            this.btnBack.TabIndex = 10;
            this.btnBack.Text = "Terug";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Location = new Point(12, 333);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new Size(132, 20);
            this.lblQuestion.TabIndex = 11;
            this.lblQuestion.Text = "Vraag:";

            // 
            // lblCorrect
            // 
            this.lblCorrect.AutoSize = true;
            this.lblCorrect.Location = new Point(12, 373);
            this.lblCorrect.Name = "lblCorrect";
            this.lblCorrect.Size = new Size(132, 20);
            this.lblCorrect.TabIndex = 12;
            this.lblCorrect.Text = "Juiste Antwoord:";

            // 
            // lblFalse1
            // 
            this.lblFalse1.AutoSize = true;
            this.lblFalse1.Location = new Point(12, 413);
            this.lblFalse1.Name = "lblFalse1";
            this.lblFalse1.Size = new Size(132, 20);
            this.lblFalse1.TabIndex = 13;
            this.lblFalse1.Text = "Fout Antwoord 1:";

            // 
            // lblFalse2
            // 
            this.lblFalse2.AutoSize = true;
            this.lblFalse2.Location = new Point(12, 453);
            this.lblFalse2.Name = "lblFalse2";
            this.lblFalse2.Size = new Size(132, 20);
            this.lblFalse2.TabIndex = 14;
            this.lblFalse2.Text = "Fout Antwoord 2:";

            // 
            // lblFalse3
            // 
            this.lblFalse3.AutoSize = true;
            this.lblFalse3.Location = new Point(12, 493);
            this.lblFalse3.Name = "lblFalse3";
            this.lblFalse3.Size = new Size(132, 20);
            this.lblFalse3.TabIndex = 15;
            this.lblFalse3.Text = "Fout Antwoord 3:";

            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new Point(12, 533);
            this.lblType.Name = "lblType";
            this.lblType.Size = new Size(132, 20);
            this.lblType.TabIndex = 16;
            this.lblType.Text = "Type:";

            // 
            // FormAdminQuestions
            // 
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 600);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblFalse3);
            this.Controls.Add(this.lblFalse2);
            this.Controls.Add(this.lblFalse1);
            this.Controls.Add(this.lblCorrect);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.txtFalse3);
            this.Controls.Add(this.txtFalse2);
            this.Controls.Add(this.txtFalse1);
            this.Controls.Add(this.txtCorrect);
            this.Controls.Add(this.txtQuestion);
            this.Controls.Add(this.dgvQuestions);
            this.Name = "FormAdminQuestions";
            this.Text = "Admin - Vragen Beheren";
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuestions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}