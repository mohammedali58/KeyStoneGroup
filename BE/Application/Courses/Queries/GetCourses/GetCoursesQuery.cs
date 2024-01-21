
using Domain.Dtos;
using MediatR;

namespace Application.Courses.Queries.GetCourses
{
    public class GetCoursesQuery : IRequest<List<CourseDto>>
    {
        public string CourseName { get; set; } = string.Empty;

        public int LanguageId { get; set; }

        public int DeliveryMethodId { get; set; }

        public string Category { get; set; } = string.Empty;
    }
}
