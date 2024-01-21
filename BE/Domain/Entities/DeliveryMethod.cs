namespace Domain.Entities
{
    public class DeliveryMethod : BaseEntity
    {
        public required string MethodName { get; set; }

        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
    }
}
