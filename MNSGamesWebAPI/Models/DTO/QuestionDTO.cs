namespace MNSGamesWebAPI.Models.DTO
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string LabelQuestion { get; set; }
        public TimeSpan? Duration { get; set; }
        public int QuizId { get; set; }

        public QuestionDTO() { }

        public QuestionDTO(Question question)
        {
            Id = question.Id;
            LabelQuestion = question.LabelQuestion;
            Duration = question.Duration;
            QuizId = question.QuizId;
        }

        public Question ToQuestion()
        {
            return new Question
            {
                Id = Id,
                LabelQuestion = LabelQuestion,
                Duration = Duration,
                QuizId = QuizId
            };
        }
    }
}
