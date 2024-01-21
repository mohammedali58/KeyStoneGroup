using Domain.Dtos;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Courses.Commands
{
    public class ChooseCourseCommandHandler : IRequestHandler<ChooseCourseCommand, ContactDto>
    {
        private readonly IContactRepository _contactRepository;

        private readonly ICourseRepository _courseRepository;

        public ChooseCourseCommandHandler(IContactRepository contactRepository, ICourseRepository courseRepository)
        {
            _contactRepository = contactRepository;
            _courseRepository = courseRepository;
        }

        public async Task<ContactDto> Handle(ChooseCourseCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var course = await _courseRepository.GetCourseById(request.CourseId);
            var contactDto = await _contactRepository.AddCourse(course, request.ContactId, cancellationToken);
            return contactDto;
        }
    }
}
