using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Quizmester
{
    public partial class FormAdminQuestions : Form
    {
        private string connectionString = "Server=localhost\\SQLEXPRESS08;Database=NewDatabaseName;Trusted_Connection=True;TrustServerCertificate=True;";

        public FormAdminQuestions()
        {
            InitializeComponent();
            LoadQuestions();
            LoadUsers();
        }

        private void LoadQuestions()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT QuestionID, Question, CorrectAnswer, FalseAnswerOne, FalseAnswerTwo, FalseAnswerThree, QuestionType FROM Questions";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvQuestions.DataSource = dt;
            }
        }

        private void LoadUsers()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT PlayerID, PlayerName, Password FROM Player";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvUsers.DataSource = dt;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtQuestion.Text) ||
                string.IsNullOrWhiteSpace(txtCorrect.Text) ||
                string.IsNullOrWhiteSpace(txtFalse1.Text) ||
                string.IsNullOrWhiteSpace(txtFalse2.Text) ||
                string.IsNullOrWhiteSpace(txtFalse3.Text) ||
                string.IsNullOrWhiteSpace(txtType.Text))
            {
                MessageBox.Show("Vul alle velden in.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string insert = "INSERT INTO Questions (Question, CorrectAnswer, FalseAnswerOne, FalseAnswerTwo, FalseAnswerThree, QuestionType) VALUES (@q, @c, @f1, @f2, @f3, @t)";
                using (SqlCommand cmd = new SqlCommand(insert, con))
                {
                    cmd.Parameters.AddWithValue("@q", txtQuestion.Text);
                    cmd.Parameters.AddWithValue("@c", txtCorrect.Text);
                    cmd.Parameters.AddWithValue("@f1", txtFalse1.Text);
                    cmd.Parameters.AddWithValue("@f2", txtFalse2.Text);
                    cmd.Parameters.AddWithValue("@f3", txtFalse3.Text);
                    cmd.Parameters.AddWithValue("@t", txtType.Text);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadQuestions();
            ClearFields();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvQuestions.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecteer een vraag om te bewerken.");
                return;
            }

            int id = (int)dgvQuestions.SelectedRows[0].Cells["QuestionID"].Value;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string update = "UPDATE Questions SET Question=@q, CorrectAnswer=@c, FalseAnswerOne=@f1, FalseAnswerTwo=@f2, FalseAnswerThree=@f3, QuestionType=@t WHERE QuestionID=@id";
                using (SqlCommand cmd = new SqlCommand(update, con))
                {
                    cmd.Parameters.AddWithValue("@q", txtQuestion.Text);
                    cmd.Parameters.AddWithValue("@c", txtCorrect.Text);
                    cmd.Parameters.AddWithValue("@f1", txtFalse1.Text);
                    cmd.Parameters.AddWithValue("@f2", txtFalse2.Text);
                    cmd.Parameters.AddWithValue("@f3", txtFalse3.Text);
                    cmd.Parameters.AddWithValue("@t", txtType.Text);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadQuestions();
            ClearFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvQuestions.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecteer een vraag om te verwijderen.");
                return;
            }

            int id = (int)dgvQuestions.SelectedRows[0].Cells["QuestionID"].Value;

            var result = MessageBox.Show("Weet je zeker dat je deze vraag wilt verwijderen?", "Verwijderen", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string delete = "DELETE FROM Questions WHERE QuestionID=@id";
                    using (SqlCommand cmd = new SqlCommand(delete, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadQuestions();
                ClearFields();
            }
        }

        private void dgvQuestions_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvQuestions.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvQuestions.SelectedRows[0];
                txtQuestion.Text = row.Cells["Question"].Value.ToString();
                txtCorrect.Text = row.Cells["CorrectAnswer"].Value.ToString();
                txtFalse1.Text = row.Cells["FalseAnswerOne"].Value.ToString();
                txtFalse2.Text = row.Cells["FalseAnswerTwo"].Value.ToString();
                txtFalse3.Text = row.Cells["FalseAnswerThree"].Value.ToString();
                txtType.Text = row.Cells["QuestionType"].Value.ToString();
            }
        }

        private void ClearFields()
        {
            txtQuestion.Text = "";
            txtCorrect.Text = "";
            txtFalse1.Text = "";
            txtFalse2.Text = "";
            txtFalse3.Text = "";
            txtType.Text = "";
        }

        private void ClearUserFields()
        {
            txtUserName.Text = "";
            txtUserPassword.Text = "";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new FormMain().Show();
            this.Close();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text) ||
                string.IsNullOrWhiteSpace(txtUserPassword.Text))
            {
                MessageBox.Show("Vul gebruikersnaam en wachtwoord in.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Check if username already exists
                string checkQuery = "SELECT COUNT(*) FROM Player WHERE PlayerName=@username";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@username", txtUserName.Text);

                int exists = (int)checkCmd.ExecuteScalar();

                if (exists > 0)
                {
                    MessageBox.Show("Deze gebruikersnaam bestaat al.");
                    return;
                }

                // Add new user
                string insert = "INSERT INTO Player (PlayerName, Password) VALUES (@username, @password)";
                using (SqlCommand cmd = new SqlCommand(insert, con))
                {
                    cmd.Parameters.AddWithValue("@username", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@password", txtUserPassword.Text);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadUsers();
            ClearUserFields();
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecteer een gebruiker om te bewerken.");
                return;
            }

            int id = (int)dgvUsers.SelectedRows[0].Cells["PlayerID"].Value;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string update = "UPDATE Player SET PlayerName=@username, Password=@password WHERE PlayerID=@id";
                using (SqlCommand cmd = new SqlCommand(update, con))
                {
                    cmd.Parameters.AddWithValue("@username", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@password", txtUserPassword.Text);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadUsers();
            ClearUserFields();
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecteer een gebruiker om te verwijderen.");
                return;
            }

            int id = (int)dgvUsers.SelectedRows[0].Cells["PlayerID"].Value;

            var result = MessageBox.Show("Weet je zeker dat je deze gebruiker wilt verwijderen?", "Verwijderen", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string delete = "DELETE FROM Player WHERE PlayerID=@id";
                    using (SqlCommand cmd = new SqlCommand(delete, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadUsers();
                ClearUserFields();
            }
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvUsers.SelectedRows[0];
                txtUserName.Text = row.Cells["PlayerName"].Value.ToString();
                txtUserPassword.Text = row.Cells["Password"].Value.ToString();
            }
        }
    }
}