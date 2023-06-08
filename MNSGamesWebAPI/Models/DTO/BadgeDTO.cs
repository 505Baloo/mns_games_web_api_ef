namespace MNSGamesWebAPI.Models.DTO
{
    public class BadgeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Descript { get; set; }
        public string? ObtainingConditions { get; set; }


        public BadgeDTO() { }

        public BadgeDTO(Badge badge)
        {
            Id = badge.Id;
            Title = badge.Title;
            Descript = badge.Descript;
            ObtainingConditions = badge.ObtainingConditions;
        }

        public Badge ToBadge()
        {
            return new Badge
            {
                Id = Id,
                Title = Title,
                Descript = Descript,
                ObtainingConditions = ObtainingConditions
            };
        }

    }
}
