using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Courses.Queries.GetCourses
{
    public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, List<CourseDto>>
    {
        private readonly ICourseRepository _courseRepository;

        public GetCoursesQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<List<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var courseName = request.CourseName;
            var category = request.Category;
            var languageId = request.LanguageId;
            var deliveryMethodId = request.DeliveryMethodId;

            var courses = await _courseRepository.GetCourses(courseName, category, languageId, deliveryMethodId, cancellationToken);

            return courses;
        }
    }
}
