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
        private TabControl tabControl;
        private TabPage tabQuestions;
        private TabPage tabUsers;
        private DataGridView dgvUsers;
        private TextBox txtUserName;
        private TextBox txtUserPassword;
        private Button btnAddUser;
        private Button btnEditUser;
        private Button btnDeleteUser;
        private Label lblUserName;
        private Label lblUserPassword;

        private void InitializeComponent()
        {
            this.tabControl = new TabControl();
            this.tabQuestions = new TabPage();
            this.tabUsers = new TabPage();
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
            this.dgvUsers = new DataGridView();
            this.txtUserName = new TextBox();
            this.txtUserPassword = new TextBox();
            this.btnAddUser = new Button();
            this.btnEditUser = new Button();
            this.btnDeleteUser = new Button();
            this.lblUserName = new Label();
            this.lblUserPassword = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuestions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabQuestions.SuspendLayout();
            this.tabUsers.SuspendLayout();
            this.SuspendLayout();

            //
            // tabControl
            //
            this.tabControl.Controls.Add(this.tabQuestions);
            this.tabControl.Controls.Add(this.tabUsers);
            this.tabControl.Location = new Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new Size(776, 550);
            this.tabControl.TabIndex = 0;

            //
            // tabQuestions
            //
            this.tabQuestions.Controls.Add(this.dgvQuestions);
            this.tabQuestions.Controls.Add(this.txtQuestion);
            this.tabQuestions.Controls.Add(this.txtCorrect);
            this.tabQuestions.Controls.Add(this.txtFalse1);
            this.tabQuestions.Controls.Add(this.txtFalse2);
            this.tabQuestions.Controls.Add(this.txtFalse3);
            this.tabQuestions.Controls.Add(this.txtType);
            this.tabQuestions.Controls.Add(this.btnAdd);
            this.tabQuestions.Controls.Add(this.btnEdit);
            this.tabQuestions.Controls.Add(this.btnDelete);
            this.tabQuestions.Controls.Add(this.lblQuestion);
            this.tabQuestions.Controls.Add(this.lblCorrect);
            this.tabQuestions.Controls.Add(this.lblFalse1);
            this.tabQuestions.Controls.Add(this.lblFalse2);
            this.tabQuestions.Controls.Add(this.lblFalse3);
            this.tabQuestions.Controls.Add(this.lblType);
            this.tabQuestions.Controls.Add(this.btnBack);
            this.tabQuestions.Location = new Point(4, 24);
            this.tabQuestions.Name = "tabQuestions";
            this.tabQuestions.Padding = new Padding(3);
            this.tabQuestions.Size = new Size(768, 522);
            this.tabQuestions.TabIndex = 0;
            this.tabQuestions.Text = "Vragen Beheren";
            this.tabQuestions.UseVisualStyleBackColor = true;

            //
            // tabUsers
            //
            this.tabUsers.Controls.Add(this.dgvUsers);
            this.tabUsers.Controls.Add(this.txtUserName);
            this.tabUsers.Controls.Add(this.txtUserPassword);
            this.tabUsers.Controls.Add(this.btnAddUser);
            this.tabUsers.Controls.Add(this.btnEditUser);
            this.tabUsers.Controls.Add(this.btnDeleteUser);
            this.tabUsers.Controls.Add(this.lblUserName);
            this.tabUsers.Controls.Add(this.lblUserPassword);
            this.tabUsers.Controls.Add(this.btnBack);
            this.tabUsers.Location = new Point(4, 24);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Padding = new Padding(3);
            this.tabUsers.Size = new Size(768, 522);
            this.tabUsers.TabIndex = 1;
            this.tabUsers.Text = "Gebruikers Beheren";
            this.tabUsers.UseVisualStyleBackColor = true;

            //
            // dgvQuestions
            //
            this.dgvQuestions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQuestions.Location = new Point(6, 6);
            this.dgvQuestions.Name = "dgvQuestions";
            this.dgvQuestions.Size = new Size(756, 300);
            this.dgvQuestions.TabIndex = 0;
            this.dgvQuestions.SelectionChanged += new System.EventHandler(this.dgvQuestions_SelectionChanged);

            //
            // dgvUsers
            //
            this.dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Location = new Point(6, 6);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.Size = new Size(756, 300);
            this.dgvUsers.TabIndex = 0;
            this.dgvUsers.SelectionChanged += new System.EventHandler(this.dgvUsers_SelectionChanged);

            //
            // txtQuestion
            //
            this.txtQuestion.Location = new Point(150, 324);
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.Size = new Size(400, 27);
            this.txtQuestion.TabIndex = 1;

            //
            // txtCorrect
            //
            this.txtCorrect.Location = new Point(150, 364);
            this.txtCorrect.Name = "txtCorrect";
            this.txtCorrect.Size = new Size(400, 27);
            this.txtCorrect.TabIndex = 2;

            //
            // txtFalse1
            //
            this.txtFalse1.Location = new Point(150, 404);
            this.txtFalse1.Name = "txtFalse1";
            this.txtFalse1.Size = new Size(400, 27);
            this.txtFalse1.TabIndex = 3;

            //
            // txtFalse2
            //
            this.txtFalse2.Location = new Point(150, 444);
            this.txtFalse2.Name = "txtFalse2";
            this.txtFalse2.Size = new Size(400, 27);
            this.txtFalse2.TabIndex = 4;

            //
            // txtFalse3
            //
            this.txtFalse3.Location = new Point(150, 484);
            this.txtFalse3.Name = "txtFalse3";
            this.txtFalse3.Size = new Size(400, 27);
            this.txtFalse3.TabIndex = 5;

            //
            // txtType
            //
            this.txtType.Location = new Point(150, 524);
            this.txtType.Name = "txtType";
            this.txtType.Size = new Size(200, 27);
            this.txtType.TabIndex = 6;

            //
            // txtUserName
            //
            this.txtUserName.Location = new Point(150, 324);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new Size(200, 27);
            this.txtUserName.TabIndex = 1;

            //
            // txtUserPassword
            //
            this.txtUserPassword.Location = new Point(150, 364);
            this.txtUserPassword.Name = "txtUserPassword";
            this.txtUserPassword.Size = new Size(200, 27);
            this.txtUserPassword.TabIndex = 2;

            //
            // btnAdd
            //
            this.btnAdd.Location = new Point(600, 324);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(100, 35);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Toevoegen";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            //
            // btnAddUser
            //
            this.btnAddUser.Location = new Point(600, 324);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new Size(100, 35);
            this.btnAddUser.TabIndex = 3;
            this.btnAddUser.Text = "Toevoegen";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);

            //
            // btnEdit
            //
            this.btnEdit.Location = new Point(600, 364);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(100, 35);
            this.btnEdit.TabIndex = 8;
            this.btnEdit.Text = "Bewerken";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            //
            // btnEditUser
            //
            this.btnEditUser.Location = new Point(600, 364);
            this.btnEditUser.Name = "btnEditUser";
            this.btnEditUser.Size = new Size(100, 35);
            this.btnEditUser.TabIndex = 4;
            this.btnEditUser.Text = "Bewerken";
            this.btnEditUser.UseVisualStyleBackColor = true;
            this.btnEditUser.Click += new System.EventHandler(this.btnEditUser_Click);

            //
            // btnDelete
            //
            this.btnDelete.Location = new Point(600, 404);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(100, 35);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Verwijderen";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            //
            // btnDeleteUser
            //
            this.btnDeleteUser.Location = new Point(600, 404);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new Size(100, 35);
            this.btnDeleteUser.TabIndex = 5;
            this.btnDeleteUser.Text = "Verwijderen";
            this.btnDeleteUser.UseVisualStyleBackColor = true;
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);

            //
            // btnBack
            //
            this.btnBack.Location = new Point(600, 580);
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
            this.lblQuestion.Location = new Point(12, 327);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new Size(132, 20);
            this.lblQuestion.TabIndex = 11;
            this.lblQuestion.Text = "Vraag:";

            //
            // lblUserName
            //
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new Point(12, 327);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new Size(132, 20);
            this.lblUserName.TabIndex = 6;
            this.lblUserName.Text = "Gebruikersnaam:";

            //
            // lblUserPassword
            //
            this.lblUserPassword.AutoSize = true;
            this.lblUserPassword.Location = new Point(12, 367);
            this.lblUserPassword.Name = "lblUserPassword";
            this.lblUserPassword.Size = new Size(132, 20);
            this.lblUserPassword.TabIndex = 7;
            this.lblUserPassword.Text = "Wachtwoord:";

            //
            // lblCorrect
            //
            this.lblCorrect.AutoSize = true;
            this.lblCorrect.Location = new Point(12, 367);
            this.lblCorrect.Name = "lblCorrect";
            this.lblCorrect.Size = new Size(132, 20);
            this.lblCorrect.TabIndex = 12;
            this.lblCorrect.Text = "Juiste Antwoord:";

            //
            // lblFalse1
            //
            this.lblFalse1.AutoSize = true;
            this.lblFalse1.Location = new Point(12, 407);
            this.lblFalse1.Name = "lblFalse1";
            this.lblFalse1.Size = new Size(132, 20);
            this.lblFalse1.TabIndex = 13;
            this.lblFalse1.Text = "Fout Antwoord 1:";

            //
            // lblFalse2
            //
            this.lblFalse2.AutoSize = true;
            this.lblFalse2.Location = new Point(12, 447);
            this.lblFalse2.Name = "lblFalse2";
            this.lblFalse2.Size = new Size(132, 20);
            this.lblFalse2.TabIndex = 14;
            this.lblFalse2.Text = "Fout Antwoord 2:";

            //
            // lblFalse3
            //
            this.lblFalse3.AutoSize = true;
            this.lblFalse3.Location = new Point(12, 487);
            this.lblFalse3.Name = "lblFalse3";
            this.lblFalse3.Size = new Size(132, 20);
            this.lblFalse3.TabIndex = 15;
            this.lblFalse3.Text = "Fout Antwoord 3:";

            //
            // lblType
            //
            this.lblType.AutoSize = true;
            this.lblType.Location = new Point(12, 527);
            this.lblType.Name = "lblType";
            this.lblType.Size = new Size(132, 20);
            this.lblType.TabIndex = 16;
            this.lblType.Text = "Type:";

            //
            // FormAdminQuestions
            //
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 650);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.tabControl);
            this.Name = "FormAdminQuestions";
            this.Text = "Admin - Beheren";
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuestions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabQuestions.ResumeLayout(false);
            this.tabQuestions.PerformLayout();
            this.tabUsers.ResumeLayout(false);
            this.tabUsers.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}