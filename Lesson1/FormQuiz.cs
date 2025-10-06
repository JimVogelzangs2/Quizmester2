using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Quizmester
{
    public partial class FormQuiz : Form
    {
        private int currentQuestionIndex = 0;
        private int score = 0;
        private int remainingSeconds = 10; // per vraag
        private int quizRemainingSeconds = 120; // totale quiz

        // Dynamische lijsten
        private List<string> questions = new List<string>();
        private List<string[]> answers = new List<string[]>();
        private List<int> correctAnswers = new List<int>();

        // 🔹 jouw database connectie (pas Database= aan)
        private string connectionString = "Server=localhost\\SQLEXPRESS08;Database=NewDatabaseName;Trusted_Connection=True;TrustServerCertificate=True;";
        private string currentQuizType = string.Empty;

        // Start een quiz op basis van het QuestionType (bv. "Clash of Clans")
        public FormQuiz(string quizType)
        {
            InitializeComponent();
            currentQuizType = quizType;
            LoadQuestionsFromDatabase(quizType);
            // Start totale quiz-timer
            quizRemainingSeconds = 120;
            if (lblQuizTimer != null)
            {
                lblQuizTimer.Text = quizRemainingSeconds.ToString();
            }
            if (quizTimer != null)
            {
                quizTimer.Stop();
                quizTimer.Start();
            }

            LoadQuestion();
        }

        // 🔹 Vragen inladen uit DB
        private void LoadQuestionsFromDatabase(string quizType)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query;
                SqlCommand cmd;
                if (string.Equals(quizType, "ALL", StringComparison.OrdinalIgnoreCase))
                {
                    // Alle vragen uit alle quiztypes
                    query = @"SELECT Question, CorrectAnswer, FalseAnswerOne, FalseAnswerTwo, FalseAnswerThree
                              FROM Questions
                              ORDER BY NEWID()";
                    cmd = new SqlCommand(query, con);
                }
                else
                {
                    // Alleen vragen voor het gekozen type
                    query = @"SELECT Question, CorrectAnswer, FalseAnswerOne, FalseAnswerTwo, FalseAnswerThree
                              FROM Questions
                              WHERE QuestionType = @quizType
                              ORDER BY NEWID()";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@quizType", quizType);
                }

                SqlDataReader reader = cmd.ExecuteReader();
                Random rnd = new Random();

                while (reader.Read())
                {
                    string vraag = reader.GetString(0);
                    string correct = reader.GetString(1);
                    string f1 = reader.GetString(2);
                    string f2 = reader.GetString(3);
                    string f3 = reader.GetString(4);

                    questions.Add(vraag);

                    // Mix antwoorden
                    var allAnswers = new List<string> { correct, f1, f2, f3 };
                    for (int i = allAnswers.Count - 1; i > 0; i--)
                    {
                        int j = rnd.Next(i + 1);
                        (allAnswers[i], allAnswers[j]) = (allAnswers[j], allAnswers[i]);
                    }

                    answers.Add(allAnswers.ToArray());
                    correctAnswers.Add(Array.IndexOf(allAnswers.ToArray(), correct));
                }
            }
        }

        // 🔹 Vraag laden
        private void LoadQuestion()
        {
            if (currentQuestionIndex < questions.Count)
            {
                lblQuestion.Text = questions[currentQuestionIndex];
                rbAnswer1.Text = answers[currentQuestionIndex][0];
                rbAnswer2.Text = answers[currentQuestionIndex][1];
                rbAnswer3.Text = answers[currentQuestionIndex][2];
                rbAnswer4.Text = answers[currentQuestionIndex][3];

                rbAnswer1.Checked = false;
                rbAnswer2.Checked = false;
                rbAnswer3.Checked = false;
                rbAnswer4.Checked = false;

                lblScore.Text = $"Score: {score}  |  Vraag {currentQuestionIndex + 1}/{questions.Count}";

                // Reset en start de timer voor deze vraag
                remainingSeconds = 10;
                if (lblTimer != null)
                {
                    lblTimer.Text = remainingSeconds.ToString();
                }
                if (questionTimer != null)
                {
                    questionTimer.Stop();
                    questionTimer.Start();
                }
            }
            else
            {
                if (questionTimer != null)
                {
                    questionTimer.Stop();
                }
                if (quizTimer != null)
                {
                    quizTimer.Stop();
                }
                // Bonus: +1 per resterende seconde van de quiz
                int finalScore = score + Math.Max(quizRemainingSeconds, 0);
                score = finalScore;
                SaveHighscore();
                MessageBox.Show($"🎉 Klaar! Eindscore: {finalScore} (vragen: {questions.Count})\nKlik op OK om door te gaan.", "Quiz afgerond", MessageBoxButtons.OK);
                new FormMain().Show();
                this.Close();
            }
        }

        // 🔹 Volgende-knop
        private void btnNext_Click(object sender, EventArgs e)
        {
            int selectedAnswer = -1;
            if (rbAnswer1.Checked) selectedAnswer = 0;
            if (rbAnswer2.Checked) selectedAnswer = 1;
            if (rbAnswer3.Checked) selectedAnswer = 2;
            if (rbAnswer4.Checked) selectedAnswer = 3;

            if (selectedAnswer == -1)
            {
                MessageBox.Show("❗ Selecteer een antwoord voordat je doorgaat!");
                return;
            }

            if (questionTimer != null)
            {
                questionTimer.Stop();
            }

            if (selectedAnswer == correctAnswers[currentQuestionIndex])
            {
                score += 5; // goed antwoord: +5
            }
            else
            {
                score -= 3; // fout antwoord: -3
            }

            currentQuestionIndex++;
            LoadQuestion();
        }

        private void questionTimer_Tick(object sender, EventArgs e)
        {
            remainingSeconds--;
            if (lblTimer != null)
            {
                lblTimer.Text = Math.Max(remainingSeconds, 0).ToString();
            }

            if (remainingSeconds <= 0)
            {
                if (questionTimer != null)
                {
                    questionTimer.Stop();
                }
                // Tijd is op: geen punten en ga door
                currentQuestionIndex++;
                LoadQuestion();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (questionTimer != null)
            {
                questionTimer.Stop();
                questionTimer.Dispose();
            }
            if (quizTimer != null)
            {
                quizTimer.Stop();
                quizTimer.Dispose();
            }
            base.OnFormClosed(e);
        }

        private void quizTimer_Tick(object sender, EventArgs e)
        {
            quizRemainingSeconds--;
            if (lblQuizTimer != null)
            {
                lblQuizTimer.Text = Math.Max(quizRemainingSeconds, 0).ToString();
            }
            if (quizRemainingSeconds <= 0)
            {
                // Stop beide timers en beëindig de quiz wegens tijd op
                if (questionTimer != null) questionTimer.Stop();
                if (quizTimer != null) quizTimer.Stop();
                // Geen bonus (0 seconden over)
                SaveHighscore();
                MessageBox.Show($"⏰ Tijd is op! Eindscore: {score} (vragen: {questions.Count})\nKlik op OK om door te gaan.", "Tijd op", MessageBoxButtons.OK);
                new FormMain().Show();
                this.Close();
            }
        }

        private void SaveHighscore()
        {
            try
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

                    string insert = "INSERT INTO Highscores (PlayerName, QuizType, Score) VALUES (@p, @q, @s)";
                    using (SqlCommand cmd = new SqlCommand(insert, con))
                    {
                        cmd.Parameters.AddWithValue("@p", Program.Session.CurrentPlayerName);
                        cmd.Parameters.AddWithValue("@q", currentQuizType);
                        cmd.Parameters.AddWithValue("@s", score);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (questionTimer != null) questionTimer.Stop();
            if (quizTimer != null) quizTimer.Stop();
            var result = MessageBox.Show("Weet je zeker dat je wilt stoppen?", "Quiz stoppen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                new FormMain().Show();
                this.Close();
            }
            else
            {
                // Ga door waar je was
                if (questionTimer != null) questionTimer.Start();
                if (quizTimer != null) quizTimer.Start();
            }
        }

        private void lblScore_Click(object sender, EventArgs e)
        {

        }
    }
}
