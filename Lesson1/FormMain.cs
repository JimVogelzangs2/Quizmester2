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

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadTopHighscores();
        }

        private void LoadTopHighscores()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string createTable = @"IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Highscores')
                                      BEGIN
                                        CREATE TABLE Highscores (
                                            HighscoreID INT IDENTITY(1,1) PRIMARY KEY,
                                            PlayerName NVARCHAR(100) NOT NULL,
                                            QuizType NVARCHAR(100) NOT NULL,
                                            Score INT NOT NULL,
                                            CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
                                        );
                                      END";
                new SqlCommand(createTable, con).ExecuteNonQuery();

                string query = @"SELECT TOP 10 PlayerName, QuizType, Score, CreatedAt
                                   FROM Highscores
                                   ORDER BY Score DESC, CreatedAt ASC";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                var dt = new System.Data.DataTable();
                da.Fill(dt);
                dgvHighscores.DataSource = dt;
            }
        }

        // Event voor de knop "Start Clash of Clans Quiz"
        private void btnStartClashQuiz_Click(object sender, EventArgs e)
        {
            FormQuiz quizForm = new FormQuiz("Clash of Clans"); // <-- geef de naam door
            quizForm.Show();
            this.Hide();
        }

        // Event voor de knop "Start Clash Royale Quiz"
        private void btnStartClashRoyaleQuiz_Click(object sender, EventArgs e)
        {
            FormQuiz quizForm = new FormQuiz("Clash Royale");
            quizForm.Show();
            this.Hide();
        }

        // Event voor de knop "Alle Vragen"
        private void btnStartAllQuiz_Click(object sender, EventArgs e)
        {
            FormQuiz quizForm = new FormQuiz("ALL");
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

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            FormAdminQuestions adminForm = new FormAdminQuestions();
            adminForm.Show();
            this.Hide();
        }
    }
}
