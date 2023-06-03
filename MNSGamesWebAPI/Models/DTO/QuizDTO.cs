namespace MNSGamesWebAPI.Models.DTO
{
    public class QuizDTO
    {
        public string QuizName { get; set; }
        public TimeSpan? Duration { get; set; }
        public int ThemeId { get; set; }
        public int AppUserId { get; set; }

        public QuizDTO() { }

        public QuizDTO(Quiz quiz)
        {
            QuizName = quiz.QuizName;
            Duration = quiz.Duration;
            ThemeId = quiz.ThemeId;
            AppUserId = quiz.AppUserId;
        }

        public Quiz ToQuiz()
        {
            return new Quiz
            {
                QuizName = QuizName,
                Duration = Duration,
                ThemeId = ThemeId,
                AppUserId = AppUserId
            };
        }
    }
}
