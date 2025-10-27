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
        private bool skipUsed = false; // Track if skip has been used
        private bool fiftyFiftyUsed = false; // Track if 50/50 has been used
        private int specialQuestionIndex = -1; // Index of the special question
        private bool isSpeedQuiz = false; // Track if this is speed quiz mode
        private int correctAnswersCount = 0; // Count correct answers for speed quiz
        private int speedQuizStartTime = 0; // Start time for speed quiz
        private int speedQuizPenaltyTime = 0; // Penalty time for wrong answers

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

            // Check if this is speed quiz
            isSpeedQuiz = quizType.Equals("SPEED", StringComparison.OrdinalIgnoreCase);

            if (isSpeedQuiz)
            {
                // Speed quiz setup
                speedQuizStartTime = Environment.TickCount;
                lblQuizTimer.Text = "00:00";
                lblTimer.Visible = false; // Hide question timer
                btnSkip.Visible = false; // Hide skip button
                btnFiftyFifty.Visible = false; // Hide 50/50 button
                correctAnswersCount = 0;
                speedQuizPenaltyTime = 0;

                // Start the quiz timer for speed quiz
                if (quizTimer != null)
                {
                    quizTimer.Stop();
                    quizTimer.Start();
                }
            }
            else
            {
                // Normal quiz setup
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

                // Select special question randomly within first 20 questions
                Random rnd = new Random();
                specialQuestionIndex = rnd.Next(0, Math.Min(20, questions.Count));

                btnSkip.Enabled = true; // Enable skip button at start
                btnFiftyFifty.Enabled = true; // Enable 50/50 button at start
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
                else if (string.Equals(quizType, "SPEED", StringComparison.OrdinalIgnoreCase))
                {
                    // Speed quiz: gebruik alle vragen
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
            // For speed quiz, only continue if we haven't reached 10 correct answers yet
            if (isSpeedQuiz && correctAnswersCount >= 10)
            {
                // Speed quiz completed
                int totalTime = (Environment.TickCount - speedQuizStartTime) / 1000 + speedQuizPenaltyTime;
                MessageBox.Show($"🎉 Speed Quiz compleet! Tijd: {totalTime} seconden\nKlik op OK om door te gaan.", "Speed Quiz afgerond", MessageBoxButtons.OK);
                SaveSpeedHighscore(totalTime);
                this.Close(); // Close the quiz form to return to main menu
                return;
            }

            if (currentQuestionIndex < questions.Count && (!isSpeedQuiz || correctAnswersCount < 10))
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

                // Re-enable all radio buttons for new question
                rbAnswer1.Enabled = true;
                rbAnswer2.Enabled = true;
                rbAnswer3.Enabled = true;
                rbAnswer4.Enabled = true;

                // Check if this is the special question
                bool isSpecialQuestion = (currentQuestionIndex == specialQuestionIndex);
                if (isSpecialQuestion)
                {
                    // Visual indicator: change background color and font
                    lblQuestion.BackColor = Color.Gold;
                    lblQuestion.Font = new Font("Segoe UI", 14F, FontStyle.Bold | FontStyle.Italic);
                    lblQuestion.ForeColor = Color.DarkRed;

                    // Play sound effect (loud ping)
                    System.Media.SystemSounds.Beep.Play();
                }
                else
                {
                    // Reset to normal appearance
                    lblQuestion.BackColor = Color.FromArgb(255, 255, 224);
                    lblQuestion.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                    lblQuestion.ForeColor = Color.Black;
                }

                if (isSpeedQuiz)
                {
                    lblScore.Text = $"Correct: {correctAnswersCount}/10  |  Tijd: {GetElapsedTime()}";
                }
                else
                {
                    lblScore.Text = $"Score: {score}  |  Vraag {currentQuestionIndex + 1}/{questions.Count}";
                }

                if (!isSpeedQuiz)
                {
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

                if (!isSpeedQuiz)
                {
                    // Bonus: +1 per resterende seconde van de quiz
                    int finalScore = score + Math.Max(quizRemainingSeconds, 0);
                    score = finalScore;
                    SaveHighscore();
                    MessageBox.Show($"🎉 Klaar! Eindscore: {finalScore} (vragen: {questions.Count})\nKlik op OK om door te gaan.", "Quiz afgerond", MessageBoxButtons.OK);
                }
                else
                {
                    // Speed quiz: show message that all questions are done but not enough correct answers
                    MessageBox.Show($"❌ Speed Quiz niet compleet! Je hebt {correctAnswersCount}/10 correcte antwoorden.\nKlik op OK om door te gaan.", "Speed Quiz gefaald", MessageBoxButtons.OK);
                }

                this.Close(); // Close the quiz form to return to main menu
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

            bool isCorrect = (selectedAnswer == correctAnswers[currentQuestionIndex]);

            if (isSpeedQuiz)
            {
                if (isCorrect)
                {
                    correctAnswersCount++;
                    if (correctAnswersCount >= 10)
                    {
                        // Speed quiz completed
                        int totalTime = (Environment.TickCount - speedQuizStartTime) / 1000 + speedQuizPenaltyTime;
                        MessageBox.Show($"🎉 Speed Quiz compleet! Tijd: {totalTime} seconden\nKlik op OK om door te gaan.", "Speed Quiz afgerond", MessageBoxButtons.OK);
                        SaveSpeedHighscore(totalTime);
                        this.Close(); // Close the quiz form to return to main menu
                        return;
                    }
                }
                else
                {
                    // Add 5 seconds penalty
                    speedQuizPenaltyTime += 5;
                }
            }
            else
            {
                if (isCorrect)
                {
                    // Extra points for special question
                    if (currentQuestionIndex == specialQuestionIndex)
                    {
                        score += 10; // special question: +10
                    }
                    else
                    {
                        score += 5; // goed antwoord: +5
                    }
                }
                else
                {
                    score -= 3; // fout antwoord: -3
                }
            }

            currentQuestionIndex++;
            LoadQuestion();
        }

        private void questionTimer_Tick(object sender, EventArgs e)
        {
            if (!isSpeedQuiz)
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
            if (!isSpeedQuiz)
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
                    this.Close(); // Close the quiz form to return to main menu
                }
            }
            else
            {
                // Update speed quiz timer display
                lblQuizTimer.Text = GetElapsedTime();
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

        private void SaveSpeedHighscore(int time)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string createTable = @"IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'SpeedHighscores')
                                           BEGIN
                                             CREATE TABLE SpeedHighscores (
                                                 SpeedHighscoreID INT IDENTITY(1,1) PRIMARY KEY,
                                                 PlayerName NVARCHAR(100) NOT NULL,
                                                 TimeSeconds INT NOT NULL,
                                                 CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
                                             );
                                           END";
                    new SqlCommand(createTable, con).ExecuteNonQuery();

                    string insert = "INSERT INTO SpeedHighscores (PlayerName, TimeSeconds) VALUES (@p, @t)";
                    using (SqlCommand cmd = new SqlCommand(insert, con))
                    {
                        cmd.Parameters.AddWithValue("@p", Program.Session.CurrentPlayerName);
                        cmd.Parameters.AddWithValue("@t", time);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        private string GetElapsedTime()
        {
            int elapsed = (Environment.TickCount - speedQuizStartTime) / 1000 + speedQuizPenaltyTime;
            int minutes = elapsed / 60;
            int seconds = elapsed % 60;
            return $"{minutes:00}:{seconds:00}";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (questionTimer != null) questionTimer.Stop();
            if (quizTimer != null) quizTimer.Stop();
            var result = MessageBox.Show("Weet je zeker dat je wilt stoppen?", "Quiz stoppen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close(); // Close the quiz form to return to main menu
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

        private void btnSkip_Click(object sender, EventArgs e)
        {
            if (skipUsed)
            {
                MessageBox.Show("Je hebt de skip functie al gebruikt in deze quiz!");
                return;
            }

            if (questionTimer != null)
            {
                questionTimer.Stop();
            }

            // Skip the question without changing score
            skipUsed = true;
            btnSkip.Enabled = false; // Disable the button after use
            currentQuestionIndex++;
            LoadQuestion();
        }

        private void btnFiftyFifty_Click(object sender, EventArgs e)
        {
            if (fiftyFiftyUsed)
            {
                MessageBox.Show("Je hebt de 50/50 functie al gebruikt in deze quiz!");
                return;
            }

            // Get the correct answer index
            int correctIndex = correctAnswers[currentQuestionIndex];

            // Create list of wrong answer indices
            List<int> wrongIndices = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                if (i != correctIndex)
                {
                    wrongIndices.Add(i);
                }
            }

            // Randomly select one wrong answer to keep
            Random rnd = new Random();
            int keepWrongIndex = wrongIndices[rnd.Next(wrongIndices.Count)];

            // Disable two wrong answers, keep correct and one wrong
            for (int i = 0; i < 4; i++)
            {
                if (i != correctIndex && i != keepWrongIndex)
                {
                    // Disable the radio button
                    switch (i)
                    {
                        case 0: rbAnswer1.Enabled = false; rbAnswer1.Checked = false; break;
                        case 1: rbAnswer2.Enabled = false; rbAnswer2.Checked = false; break;
                        case 2: rbAnswer3.Enabled = false; rbAnswer3.Checked = false; break;
                        case 3: rbAnswer4.Enabled = false; rbAnswer4.Checked = false; break;
                    }
                }
            }

            fiftyFiftyUsed = true;
            btnFiftyFifty.Enabled = false; // Disable the button after use
        }
    }
}
