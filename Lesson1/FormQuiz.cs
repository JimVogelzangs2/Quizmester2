/* Quiz formulier: beheert quiz logica, vragen laden, timers, scores */

using System; /* Basis systeemfuncties */
using System.Collections.Generic; /* Voor lijsten */
using System.Windows.Forms; /* Windows Forms */
using Microsoft.Data.SqlClient; /* Database verbindingen */

namespace Quizmester
{
    public partial class FormQuiz : Form
    {
        /* Variabelen voor quiz staat */
        private int currentQuestionIndex = 0; /* Huidige vraag index */
        private int score = 0; /* Huidige score */
        private int remainingSeconds = 10; /* Seconden per vraag */
        private int quizRemainingSeconds = 120; /* Totale quiz tijd */
        private bool skipUsed = false; /* Skip gebruikt? */
        private bool fiftyFiftyUsed = false; /* 50/50 gebruikt? */
        private int specialQuestionIndex = -1; /* Speciale vraag index */
        private bool isSpeedQuiz = false; /* Is speed quiz? */
        private int correctAnswersCount = 0; /* Correcte antwoorden teller */
        private int speedQuizStartTime = 0; /* Starttijd speed quiz */
        private int speedQuizPenaltyTime = 0; /* Penalty tijd */

        /* Dynamische lijsten voor vragen en antwoorden */
        private List<string> questions = new List<string>(); /* Vragen lijst */
        private List<string[]> answers = new List<string[]>(); /* Antwoorden arrays */
        private List<int> correctAnswers = new List<int>(); /* Correcte antwoord indices */

        /* Database verbinding string */
        private string connectionString = "Server=localhost\\SQLEXPRESS08;Database=NewDatabaseName;Trusted_Connection=True;TrustServerCertificate=True;";
        private string currentQuizType = string.Empty; /* Huidig quiz type */

        /* Constructor: initialiseert quiz gebaseerd op type */
        public FormQuiz(string quizType)
        {
            InitializeComponent();
            currentQuizType = quizType;
            LoadQuestionsFromDatabase(quizType);

            /* Controleer of het speed quiz is */
            isSpeedQuiz = quizType.Equals("SPEED", StringComparison.OrdinalIgnoreCase);

            if (isSpeedQuiz)
            {
                /* Speed quiz setup */
                speedQuizStartTime = Environment.TickCount;
                lblQuizTimer.Text = "00:00";
                lblTimer.Visible = false; /* Verberg vraag timer */
                btnSkip.Visible = false; /* Verberg skip knop */
                btnFiftyFifty.Visible = false; /* Verberg 50/50 knop */
                correctAnswersCount = 0;
                speedQuizPenaltyTime = 0;

                /* Start quiz timer voor speed quiz */
                if (quizTimer != null)
                {
                    quizTimer.Stop();
                    quizTimer.Start();
                }
            }
            else
            {
                /* Normale quiz setup */
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

                /* Selecteer speciale vraag random in eerste 20 */
                Random rnd = new Random();
                specialQuestionIndex = rnd.Next(0, Math.Min(20, questions.Count));

                btnSkip.Enabled = true; /* Enable skip knop */
                btnFiftyFifty.Enabled = true; /* Enable 50/50 knop */
            }

            LoadQuestion();
        }

        /* Laadt vragen uit database gebaseerd op quiz type */
        private void LoadQuestionsFromDatabase(string quizType)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query;
                SqlCommand cmd;
                if (string.Equals(quizType, "ALL", StringComparison.OrdinalIgnoreCase))
                {
                    /* Alle vragen uit alle quiztypes */
                    query = @"SELECT Question, CorrectAnswer, FalseAnswerOne, FalseAnswerTwo, FalseAnswerThree
                              FROM Questions
                              ORDER BY NEWID()";
                    cmd = new SqlCommand(query, con);
                }
                else if (string.Equals(quizType, "SPEED", StringComparison.OrdinalIgnoreCase))
                {
                    /* Speed quiz: gebruik alle vragen */
                    query = @"SELECT Question, CorrectAnswer, FalseAnswerOne, FalseAnswerTwo, FalseAnswerThree
                              FROM Questions
                              ORDER BY NEWID()";
                    cmd = new SqlCommand(query, con);
                }
                else
                {
                    /* Alleen vragen voor het gekozen type */
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

                    /* Mix antwoorden random */
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

        /* Laadt volgende vraag of eindigt quiz */
        private void LoadQuestion()
        {
            /* Voor speed quiz: stop als 10 correct bereikt */
            if (isSpeedQuiz && correctAnswersCount >= 10)
            {
                /* Speed quiz compleet */
                int totalTime = (Environment.TickCount - speedQuizStartTime) / 1000 + speedQuizPenaltyTime;
                MessageBox.Show($"🎉 Speed Quiz compleet! Tijd: {totalTime} seconden\nKlik op OK om door te gaan.", "Speed Quiz afgerond", MessageBoxButtons.OK);
                SaveSpeedHighscore(totalTime);
                this.Close(); /* Sluit quiz formulier */
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

                /* Heractiveer alle radio buttons */
                rbAnswer1.Enabled = true;
                rbAnswer2.Enabled = true;
                rbAnswer3.Enabled = true;
                rbAnswer4.Enabled = true;

                /* Controleer of dit speciale vraag is */
                bool isSpecialQuestion = (currentQuestionIndex == specialQuestionIndex);
                if (isSpecialQuestion)
                {
                    /* Visuele indicator: verander kleur en font */
                    lblQuestion.BackColor = Color.Gold;
                    lblQuestion.Font = new Font("Segoe UI", 14F, FontStyle.Bold | FontStyle.Italic);
                    lblQuestion.ForeColor = Color.DarkRed;

                    /* Speel geluid effect */
                    System.Media.SystemSounds.Beep.Play();
                }
                else
                {
                    /* Reset naar normaal uiterlijk */
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
                    /* Reset en start vraag timer */
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
                    /* Bonus: +1 per resterende seconde */
                    int finalScore = score + Math.Max(quizRemainingSeconds, 0);
                    score = finalScore;
                    SaveHighscore();
                    MessageBox.Show($"🎉 Klaar! Eindscore: {finalScore} (vragen: {questions.Count})\nKlik op OK om door te gaan.", "Quiz afgerond", MessageBoxButtons.OK);
                }
                else
                {
                    /* Speed quiz: niet genoeg correcte antwoorden */
                    MessageBox.Show($"❌ Speed Quiz niet compleet! Je hebt {correctAnswersCount}/10 correcte antwoorden.\nKlik op OK om door te gaan.", "Speed Quiz gefaald", MessageBoxButtons.OK);
                }

                this.Close(); /* Sluit quiz formulier */
            }
        }

        /* Volgende knop: controleert antwoord en gaat verder */
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
                        /* Speed quiz compleet */
                        int totalTime = (Environment.TickCount - speedQuizStartTime) / 1000 + speedQuizPenaltyTime;
                        MessageBox.Show($"🎉 Speed Quiz compleet! Tijd: {totalTime} seconden\nKlik op OK om door te gaan.", "Speed Quiz afgerond", MessageBoxButtons.OK);
                        SaveSpeedHighscore(totalTime);
                        this.Close(); /* Sluit quiz formulier */
                        return;
                    }
                }
                else
                {
                    /* Voeg 5 seconden penalty toe */
                    speedQuizPenaltyTime += 5;
                }
            }
            else
            {
                if (isCorrect)
                {
                    /* Extra punten voor speciale vraag */
                    if (currentQuestionIndex == specialQuestionIndex)
                    {
                        score += 10; /* Speciale vraag: +10 */
                    }
                    else
                    {
                        score += 5; /* Goed antwoord: +5 */
                    }
                }
                else
                {
                    score -= 3; /* Fout antwoord: -3 */
                }
            }

            currentQuestionIndex++;
            LoadQuestion();
        }

        /* Vraag timer tick: aftellen per vraag */
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
                    /* Tijd op: geen punten, ga door */
                    currentQuestionIndex++;
                    LoadQuestion();
                }
            }
        }

        /* Form sluiten: cleanup timers */
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

        /* Quiz timer tick: totale quiz tijd */
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
                    /* Stop timers en eindig quiz */
                    if (questionTimer != null) questionTimer.Stop();
                    if (quizTimer != null) quizTimer.Stop();
                    /* Geen bonus tijd */
                    SaveHighscore();
                    MessageBox.Show($"⏰ Tijd is op! Eindscore: {score} (vragen: {questions.Count})\nKlik op OK om door te gaan.", "Tijd op", MessageBoxButtons.OK);
                    this.Close(); /* Sluit quiz formulier */
                }
            }
            else
            {
                /* Update speed quiz timer display */
                lblQuizTimer.Text = GetElapsedTime();
            }
        }

        /* Slaat normale highscore op in database */
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

        /* Slaat speed quiz highscore op in database */
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

        /* Geeft verstreken tijd voor speed quiz */
        private string GetElapsedTime()
        {
            int elapsed = (Environment.TickCount - speedQuizStartTime) / 1000 + speedQuizPenaltyTime;
            int minutes = elapsed / 60;
            int seconds = elapsed % 60;
            return $"{minutes:00}:{seconds:00}";
        }

        /* Stop knop: bevestiging om quiz te stoppen */
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (questionTimer != null) questionTimer.Stop();
            if (quizTimer != null) quizTimer.Stop();
            var result = MessageBox.Show("Weet je zeker dat je wilt stoppen?", "Quiz stoppen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close(); /* Sluit quiz formulier */
            }
            else
            {
                /* Ga door waar je was */
                if (questionTimer != null) questionTimer.Start();
                if (quizTimer != null) quizTimer.Start();
            }
        }

        /* Leeg event handler voor score label */
        private void lblScore_Click(object sender, EventArgs e)
        {

        }

        /* Skip knop: sla vraag over zonder score verandering */
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

            /* Skip vraag zonder score verandering */
            skipUsed = true;
            btnSkip.Enabled = false; /* Disable na gebruik */
            currentQuestionIndex++;
            LoadQuestion();
        }

        /* 50/50 knop: helpt door twee foute antwoorden uit te schakelen */
        private void btnFiftyFifty_Click(object sender, EventArgs e)
        {
            if (fiftyFiftyUsed)
            {
                MessageBox.Show("Je hebt de 50/50 functie al gebruikt in deze quiz!");
                return;
            }

            /* Haal index van correct antwoord (0-3 voor rbAnswer1-4) */
            int correctIndex = correctAnswers[currentQuestionIndex];

            /* Maak lijst van indices van foute antwoorden (0-3) */
            List<int> wrongIndices = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                if (i != correctIndex)
                {
                    wrongIndices.Add(i);
                }
            }

            /* Kies random één fout antwoord om te houden (van de 3 foute) */
            Random rnd = new Random();
            int keepWrongIndex = wrongIndices[rnd.Next(wrongIndices.Count)];

            /* Schakel twee foute antwoorden uit, houd correct + 1 fout antwoord */
            for (int i = 0; i < 4; i++)
            {
                if (i != correctIndex && i != keepWrongIndex)
                {
                    /* Disable radio button voor foute antwoorden */
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
            btnFiftyFifty.Enabled = false; /* Kan niet meer gebruikt worden */
        }
    }
}
