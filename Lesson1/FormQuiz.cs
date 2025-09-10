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

        // Dynamische lijsten
        private List<string> questions = new List<string>();
        private List<string[]> answers = new List<string[]>();
        private List<int> correctAnswers = new List<int>();

        // 🔹 jouw database connectie (pas Database= aan)
        private string connectionString = "Server=localhost\\SQLEXPRESS08;Database=NewDatabaseName;Trusted_Connection=True;TrustServerCertificate=True;";

        // Start een quiz op basis van het QuestionType (bv. "Clash of Clans")
        public FormQuiz(string quizType)
        {
            InitializeComponent();
            LoadQuestionsFromDatabase(quizType);
            LoadQuestion();
        }

        // 🔹 Vragen inladen uit DB
        private void LoadQuestionsFromDatabase(string quizType)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"SELECT Question, CorrectAnswer, FalseAnswerOne, FalseAnswerTwo, FalseAnswerThree
                                 FROM Questions
                                 WHERE QuestionType = @quizType
                                 ORDER BY QuestionID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@quizType", quizType);

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

                lblScore.Text = $"Score: {score}/{currentQuestionIndex}";
            }
            else
            {
                MessageBox.Show($"🎉 Klaar! Je eindscore is {score}/{questions.Count}");
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

            if (selectedAnswer == correctAnswers[currentQuestionIndex])
            {
                score++;
            }

            currentQuestionIndex++;
            LoadQuestion();
        }
    }
}
