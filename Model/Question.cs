using System;

    public class Question
    {
        public string QuestionText { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public int CorrectAnswer { get; set; }
        public string CorrectString { get; set; }

        public Question(string questionText, string option1, string option2, string option3, string option4, int correctAnswer)
        {
            QuestionText = questionText;
            Option1 = option1;
            Option2 = option2;
            Option3 = option3;
            Option4 = option4;
            CorrectAnswer = correctAnswer;
            // assign CorrectString to correctanswer
            switch (correctAnswer)
            {
                case 1:
                    CorrectString = option1;
                    break;
                case 2:
                    CorrectString = option2;
                    break;
                case 3:
                    CorrectString = option3;
                    break;
                case 4:
                    CorrectString = option4;
                    break;
                default:
                    break;
            }
        }
    }

