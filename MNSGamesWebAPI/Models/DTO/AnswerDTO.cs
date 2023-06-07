namespace MNSGamesWebAPI.Models.DTO
{
    public class AnswerDTO
    {
        public int Id { get; set; }
        public string LabelAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public int? Points { get; set; }
        public int QuestionId { get; set; }

        public AnswerDTO() { }

        public AnswerDTO(Answer answer)
        {
            Id = answer.Id;
            LabelAnswer = answer.LabelAnswer;
            IsCorrect = answer.IsCorrect;
            Points = answer.Points;
            QuestionId = answer.QuestionId;
        }

        public Answer ToAnswer()
        {
            return new Answer
            {
                Id = Id,
                LabelAnswer = LabelAnswer,
                IsCorrect = IsCorrect,
                Points = Points,
                QuestionId = QuestionId
            };
        }
    }
}
