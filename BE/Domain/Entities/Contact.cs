using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Contact : BaseEntity
    {
        public required string FullName { get; set; }

        public required string Email { get; set; }

        public required string PhoneNumber { get; set; }

        public required string Address { get; set; }

        public Course? Course { get; set; }
    }
}
