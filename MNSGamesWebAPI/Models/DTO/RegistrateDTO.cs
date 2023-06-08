namespace MNSGamesWebAPI.Models.DTO
{
    public class RegistrateDTO
    {
        public int QuizId { get; set; }
        public int AppUserId { get; set; }
        public DateTime RegistrationDate { get; set; }


        public RegistrateDTO() { }

        public RegistrateDTO(QuizDTO quizDTO, AppUserDTO appUserDTO)
        {
            QuizId = quizDTO.Id;
            AppUserId = appUserDTO.Id;
            RegistrationDate = DateTime.Now;
        }

        public RegistrateDTO(Registrate registrate)
        {
            QuizId = registrate.QuizId;
            AppUserId = registrate.AppUserId;
            RegistrationDate = registrate.RegistrationDate;
        }

        public Registrate ToRegistrate()
        {
            return new Registrate
            {
                QuizId = QuizId,
                AppUserId = AppUserId,
                RegistrationDate = RegistrationDate,
            };
        }
    }
}
