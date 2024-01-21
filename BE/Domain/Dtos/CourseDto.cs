using Domain.Entities;

namespace Domain.Dtos;
    public class CourseDto
    {
        public Guid Id { get; set; }

        public required string InstituteName { get; set; }

        public required string CourseName { get; set; }

        public required string Category { get; set; }

        public required string DeliveryMethod { get; set; }

        public required string Language { get; set; }

        public required string Location { get; set; }

        public DateTime StartDate { get; set; }
    }

