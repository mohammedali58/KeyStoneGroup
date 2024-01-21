using Domain.Dtos;
using MediatR;

namespace Application.Courses.Commands
{
    public class ChooseCourseCommand : IRequest<ContactDto>
    {
        public int ContactId { get; set; }

        public Guid CourseId { get; set; }  
    }
}
