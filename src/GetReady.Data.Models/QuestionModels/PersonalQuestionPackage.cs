namespace GetReady.Data.Models.QuestionModels
{
    public class PersonalQuestionPackage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public string Comment { get; set; }

        public int Difficulty { get; set; }

        public float AnswerRate { get; set; }

        public int TimesBeingAnswered { get; set; }

        public string YourBestAnswer { get; set; }

        public int Order { get; set; }

        public int Column { get; set; }

        /// <summary>
        /// coma separated numbers(1-10)
        /// </summary>
        public string LastTenScores { get; set; }

        public int QuestionSheetId { get; set; }
        public QuestionSheet QuestionSheet { get; set; }
    }
}
