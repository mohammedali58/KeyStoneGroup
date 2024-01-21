using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Globalization;
using System.Linq;
using Domain.Entities;
using Application.Courses.Queries.GetCourses;
using Domain.Dtos;
using Application.Courses.Commands;
using MediatR;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {

        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<CourseDto>> GetCourses([FromQuery] GetCoursesQuery command)
        {
           return await _mediator.Send(command).ConfigureAwait(false);
        }

     
        [HttpPost("choose")]
        public async Task<ContactDto> ChooseCourse (ChooseCourseCommand command)
        {
            return await _mediator.Send(command).ConfigureAwait(false);
        }
    }
}
