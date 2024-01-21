namespace Domain.Entities
{
    public class Language : BaseEntity
    {
        public required string LanguageName { get; set; }

        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
    }
}
