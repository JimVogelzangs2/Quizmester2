using System.Windows.Forms;
using System.Drawing;

namespace Quizmester
{
    partial class FormQuiz
    {
        private Label lblQuestion;
        private RadioButton rbAnswer1;
        private RadioButton rbAnswer2;
        private RadioButton rbAnswer3;
        private RadioButton rbAnswer4;
        private Button btnNext;
        private Label lblScore;
  private Label lblTimer;
  private System.Windows.Forms.Timer questionTimer;
  private Label lblQuizTimer;
  private System.Windows.Forms.Timer quizTimer;
  private Button btnStop;
  private Button btnSkip;

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblQuestion = new Label();
            rbAnswer1 = new RadioButton();
            rbAnswer2 = new RadioButton();
            rbAnswer3 = new RadioButton();
            rbAnswer4 = new RadioButton();
            btnNext = new Button();
            lblScore = new Label();
            lblTimer = new Label();
            questionTimer = new System.Windows.Forms.Timer(components);
            lblQuizTimer = new Label();
            quizTimer = new System.Windows.Forms.Timer(components);
            btnStop = new Button();
            btnSkip = new Button();
            SuspendLayout();
            // 
            // lblQuestion
            // 
            lblQuestion.BackColor = Color.FromArgb(255, 255, 224);
            lblQuestion.BorderStyle = BorderStyle.FixedSingle;
            lblQuestion.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblQuestion.Location = new Point(57, 27);
            lblQuestion.Name = "lblQuestion";
            lblQuestion.Size = new Size(571, 79);
            lblQuestion.TabIndex = 0;
            lblQuestion.Text = "Hier komt de vraag";
            lblQuestion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // rbAnswer1
            // 
            rbAnswer1.Font = new Font("Segoe UI", 10F);
            rbAnswer1.Location = new Point(114, 133);
            rbAnswer1.Margin = new Padding(3, 4, 3, 4);
            rbAnswer1.Name = "rbAnswer1";
            rbAnswer1.Size = new Size(457, 40);
            rbAnswer1.TabIndex = 1;
            rbAnswer1.Text = "Antwoord 1";
            // 
            // rbAnswer2
            // 
            rbAnswer2.Font = new Font("Segoe UI", 10F);
            rbAnswer2.Location = new Point(114, 187);
            rbAnswer2.Margin = new Padding(3, 4, 3, 4);
            rbAnswer2.Name = "rbAnswer2";
            rbAnswer2.Size = new Size(457, 40);
            rbAnswer2.TabIndex = 2;
            rbAnswer2.Text = "Antwoord 2";
            // 
            // rbAnswer3
            // 
            rbAnswer3.Font = new Font("Segoe UI", 10F);
            rbAnswer3.Location = new Point(114, 240);
            rbAnswer3.Margin = new Padding(3, 4, 3, 4);
            rbAnswer3.Name = "rbAnswer3";
            rbAnswer3.Size = new Size(457, 40);
            rbAnswer3.TabIndex = 3;
            rbAnswer3.Text = "Antwoord 3";
            // 
            // rbAnswer4
            // 
            rbAnswer4.Font = new Font("Segoe UI", 10F);
            rbAnswer4.Location = new Point(114, 293);
            rbAnswer4.Margin = new Padding(3, 4, 3, 4);
            rbAnswer4.Name = "rbAnswer4";
            rbAnswer4.Size = new Size(457, 40);
            rbAnswer4.TabIndex = 4;
            rbAnswer4.Text = "Antwoord 4";
            // 
            // btnNext
            // 
            btnNext.BackColor = Color.FromArgb(0, 123, 255);
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnNext.ForeColor = Color.White;
            btnNext.Location = new Point(57, 410);
            btnNext.Margin = new Padding(3, 4, 3, 4);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(137, 53);
            btnNext.TabIndex = 5;
            btnNext.Text = "Volgende";
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += btnNext_Click;
            // 
            // lblScore
            // 
            lblScore.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblScore.Location = new Point(57, 467);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(343, 33);
            lblScore.TabIndex = 7;
            lblScore.Text = "Score: 0  |  Vraag 1/1";
            // 
            // lblTimer
            // 
            lblTimer.BackColor = Color.FromArgb(255, 235, 235);
            lblTimer.BorderStyle = BorderStyle.FixedSingle;
            lblTimer.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTimer.ForeColor = Color.DarkRed;
            lblTimer.Location = new Point(57, 347);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(114, 39);
            lblTimer.TabIndex = 8;
            lblTimer.Text = "10";
            lblTimer.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // questionTimer
            // 
            questionTimer.Interval = 1000;
            questionTimer.Tick += questionTimer_Tick;
            // 
            // lblQuizTimer
            // 
            lblQuizTimer.BackColor = Color.FromArgb(235, 245, 255);
            lblQuizTimer.BorderStyle = BorderStyle.FixedSingle;
            lblQuizTimer.Font = new Font("Segoe UI", 10F);
            lblQuizTimer.ForeColor = Color.MidnightBlue;
            lblQuizTimer.Location = new Point(229, 347);
            lblQuizTimer.Name = "lblQuizTimer";
            lblQuizTimer.Size = new Size(114, 39);
            lblQuizTimer.TabIndex = 9;
            lblQuizTimer.Text = "60";
            lblQuizTimer.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // quizTimer
            // 
            quizTimer.Interval = 1000;
            quizTimer.Tick += quizTimer_Tick;
            // 
            // btnStop
            // 
            btnStop.BackColor = Color.FromArgb(220, 53, 69);
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStop.ForeColor = Color.White;
            btnStop.Location = new Point(229, 410);
            btnStop.Margin = new Padding(3, 4, 3, 4);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(137, 53);
            btnStop.TabIndex = 6;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = false;
            btnStop.Click += btnStop_Click;
            //
            // btnSkip
            //
            btnSkip.BackColor = Color.FromArgb(255, 193, 7);
            btnSkip.FlatStyle = FlatStyle.Flat;
            btnSkip.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSkip.ForeColor = Color.Black;
            btnSkip.Location = new Point(391, 410);
            btnSkip.Margin = new Padding(3, 4, 3, 4);
            btnSkip.Name = "btnSkip";
            btnSkip.Size = new Size(137, 53);
            btnSkip.TabIndex = 10;
            btnSkip.Text = "Skip";
            btnSkip.UseVisualStyleBackColor = false;
            btnSkip.Click += btnSkip_Click;
            //
            // FormQuiz
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(686, 533);
            Controls.Add(lblQuestion);
            Controls.Add(rbAnswer1);
            Controls.Add(rbAnswer2);
            Controls.Add(rbAnswer3);
            Controls.Add(rbAnswer4);
            Controls.Add(btnNext);
            Controls.Add(btnStop);
            Controls.Add(btnSkip);
            Controls.Add(lblScore);
            Controls.Add(lblTimer);
            Controls.Add(lblQuizTimer);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormQuiz";
            Text = "Quiz";
            ResumeLayout(false);
        }
        private System.ComponentModel.IContainer components;
    }
}
