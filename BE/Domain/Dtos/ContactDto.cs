using Domain.Entities;

namespace Domain.Dtos
{
    public class ContactDto
    {
        public required string FullName { get; set; }

        public required string Email { get; set; }

        public required string PhoneNumber { get; set; }

        public required string Address { get; set; }

        public CourseDto? Course { get; set; }
    }
}
