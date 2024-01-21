namespace Domain.Entities
{
    public class CourseTxtInput
    {

        public Guid Id { get; set; }

        public required string InstituteName { get; set; }

        public required string CourseName { get; set; }

        public required string Category { get; set; }

        public int DeliveryMethodId { get; set; }

        public int LanguageId { get; set; }

        public required string Location { get; set; }

        public DateTime StartDate { get; set; }

    }
}
