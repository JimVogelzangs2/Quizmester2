using Microsoft.Data.SqlClient;
using Quizmester;
using System;
using System.Windows.Forms;

namespace Quizmester
{
    public partial class FormMain : Form
    {
        string connectionString = "Server=localhost\\SQLEXPRESS08;Database=NewDatabaseName;Trusted_Connection=True;TrustServerCertificate=True;";

        public FormMain()
        {
            InitializeComponent();
        }

        // Event voor de knop "Start Clash of Clans Quiz"
        private void btnStartClashQuiz_Click(object sender, EventArgs e)
        {
            FormQuiz quizForm = new FormQuiz("Clash of Clans"); // <-- geef de naam door
            quizForm.Show();
            this.Hide();
        }


        private int GetQuizId(string quizName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT QuizID FROM Quizzes WHERE QuizName = @name";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", quizName);

                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
        }
    }
}
