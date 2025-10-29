/* Hoofdvorm applicatie: beheert hoofdscherm, quizzen starten, highscores bekijken, admin functies. */

using Microsoft.Data.SqlClient; /* Databaseverbindingen SQL Server */
using Quizmester; /* Eigen namespace applicatie */
using System; /* Basis systeemfuncties */
using System.Windows.Forms; /* Windows Forms componenten */

namespace Quizmester
{
    public partial class FormMain : Form
    {
        // Verbindingsstring voor de database. Dit verbindt met een lokale SQL Server Express instantie.
        string connectionString = "Server=localhost\\SQLEXPRESS08;Database=NewDatabaseName;Trusted_Connection=True;TrustServerCertificate=True;";

        /* Constructor: initialiseert formulier componenten */
        public FormMain()
        {
            InitializeComponent();
        }

        /* Form load event: laadt top highscores en speed highscores */
        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadTopHighscores();
            LoadTopSpeedHighscores();
        }

        /* Laadt top 10 highscores in DataGridView, creëert tabel indien nodig */
        private void LoadTopHighscores()
        {
            using (SqlConnection con = new SqlConnection(connectionString)) /* Auto disposal */
            {
                con.Open(); /* Open DB verbinding */
                /* Creëer Highscores tabel indien niet bestaat */
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
                new SqlCommand(createTable, con).ExecuteNonQuery(); /* Voer tabelcreatie uit */

                /* Haal top 10 highscores op */
                string query = @"SELECT TOP 10 PlayerName, QuizType, Score, CreatedAt
                                    FROM Highscores
                                    ORDER BY Score DESC, CreatedAt ASC";
                SqlDataAdapter da = new SqlDataAdapter(query, con); /* Data adapter */
                var dt = new System.Data.DataTable(); /* DataTable */
                da.Fill(dt); /* Vul DataTable */
                dgvHighscores.DataSource = dt; /* Bind aan DataGridView */
            }
        }

        /* Laadt top 10 speed highscores in DataGridView, creëert tabel indien nodig */
        private void LoadTopSpeedHighscores()
        {
            using (SqlConnection con = new SqlConnection(connectionString)) /* Auto disposal */
            {
                con.Open(); /* Open DB verbinding */
                /* Creëer SpeedHighscores tabel indien niet bestaat */
                string createTable = @"IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'SpeedHighscores')
                                       BEGIN
                                         CREATE TABLE SpeedHighscores (
                                             SpeedHighscoreID INT IDENTITY(1,1) PRIMARY KEY,
                                             PlayerName NVARCHAR(100) NOT NULL,
                                             TimeSeconds INT NOT NULL,
                                             CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
                                         );
                                       END";
                new SqlCommand(createTable, con).ExecuteNonQuery(); /* Voer tabelcreatie uit */

                /* Haal top 10 speed highscores op */
                string query = @"SELECT TOP 10 PlayerName, TimeSeconds, CreatedAt
                                    FROM SpeedHighscores
                                    ORDER BY TimeSeconds ASC, CreatedAt ASC";
                SqlDataAdapter da = new SqlDataAdapter(query, con); /* Data adapter */
                var dt = new System.Data.DataTable(); /* DataTable */
                da.Fill(dt); /* Vul DataTable */
                dgvSpeedHighscores.DataSource = dt; /* Bind aan DataGridView */
            }
        }

        /* Start Clash of Clans quiz */
        private void btnStartClashQuiz_Click(object sender, EventArgs e)
        {
            FormQuiz quizForm = new FormQuiz("Clash of Clans");
            this.Hide();
            quizForm.ShowDialog();
            this.Show();
        }

        /* Start Clash Royale quiz */
        private void btnStartClashRoyaleQuiz_Click(object sender, EventArgs e)
        {
            FormQuiz quizForm = new FormQuiz("Clash Royale");
            this.Hide();
            quizForm.ShowDialog();
            this.Show();
        }

        /* Start alle vragen quiz */
        private void btnStartAllQuiz_Click(object sender, EventArgs e)
        {
            FormQuiz quizForm = new FormQuiz("ALL");
            this.Hide();
            quizForm.ShowDialog();
            this.Show();
        }

        /* Start speed quiz */
        private void btnStartSpeedQuiz_Click(object sender, EventArgs e)
        {
            FormQuiz quizForm = new FormQuiz("SPEED");
            this.Hide();
            quizForm.ShowDialog();
            this.Show();
        }


        /* Haalt QuizID op basis van quiz naam */
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

        /* Admin knop: controleert toegang en opent admin formulier */
        private void btnAdmin_Click(object sender, EventArgs e)
        {
            if (Program.Session.CurrentPlayerName != "Admin")
            {
                MessageBox.Show("Alleen gebruikers met de naam 'Admin' hebben toegang tot deze functie!", "Toegang geweigerd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FormAdminQuestions adminForm = new FormAdminQuestions();
            adminForm.Show();
            this.Hide();
        }
    }
}
