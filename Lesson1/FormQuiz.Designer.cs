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

        private void InitializeComponent()
        {
            this.lblQuestion = new Label();
            this.rbAnswer1 = new RadioButton();
            this.rbAnswer2 = new RadioButton();
            this.rbAnswer3 = new RadioButton();
            this.rbAnswer4 = new RadioButton();
            this.btnNext = new Button();
            this.lblScore = new Label();
            this.SuspendLayout();

            // lblQuestion
            this.lblQuestion.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblQuestion.Location = new Point(20, 20);
            this.lblQuestion.Size = new Size(500, 60);
            this.lblQuestion.Text = "Hier komt de vraag";

            // rbAnswer1
            this.rbAnswer1.Location = new Point(40, 100);
            this.rbAnswer1.Size = new Size(400, 30);
            this.rbAnswer1.Text = "Antwoord 1";

            // rbAnswer2
            this.rbAnswer2.Location = new Point(40, 140);
            this.rbAnswer2.Size = new Size(400, 30);
            this.rbAnswer2.Text = "Antwoord 2";

            // rbAnswer3
            this.rbAnswer3.Location = new Point(40, 180);
            this.rbAnswer3.Size = new Size(400, 30);
            this.rbAnswer3.Text = "Antwoord 3";

            // rbAnswer4
            this.rbAnswer4.Location = new Point(40, 220);
            this.rbAnswer4.Size = new Size(400, 30);
            this.rbAnswer4.Text = "Antwoord 4";

            // btnNext
            this.btnNext.Location = new Point(40, 270);
            this.btnNext.Size = new Size(120, 40);
            this.btnNext.Text = "Volgende";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);

            // lblScore
            this.lblScore.Location = new Point(200, 270);
            this.lblScore.Size = new Size(200, 40);
            this.lblScore.Text = "Score: 0/0";

            // FormQuiz
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(600, 350);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.rbAnswer1);
            this.Controls.Add(this.rbAnswer2);
            this.Controls.Add(this.rbAnswer3);
            this.Controls.Add(this.rbAnswer4);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.lblScore);
            this.Text = "Quiz";
            this.ResumeLayout(false);
        }
    }
}
