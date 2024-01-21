namespace Domain.Entities
{
    public class Course
    {
        public Guid Id { get; set; }

        public required string InstituteName { get; set; }

        public required string CourseName { get; set; }

        public required string Category { get; set; }

        public DeliveryMethod? DeliveryMethod { get; set; }

        public Language? Language { get; set; }

        public required string Location { get; set; }

        public DateTime StartDate { get; set; }

        public ICollection<Contact> Contacts { get; set; } = new HashSet<Contact>();
    }
}
