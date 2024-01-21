using AutoMapper;
using Domain.Common.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CourseRepository(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CourseDto?> GetCourseById(Guid id)
        {
            var course =  await _context.Courses.
                Include(c => c.DeliveryMethod).
                Include(c => c.Language).
                FirstOrDefaultAsync(c => c.Id == id).
                ConfigureAwait(false);
            if(course is null)
            {
                throw new HttpException(StatusCodes.Status404NotFound, "course not found");
            }
            CourseDto courseDto = _mapper.Map<CourseDto>(course);
            return courseDto;  
        }

        public async Task<List<CourseDto>> GetCourses(string courseName, string category, int languageId, int deliveryMethodId, CancellationToken cToken)
        {
            var courses = await _context.Courses.
                Include(c => c.DeliveryMethod).
                Include(c => c.Language).
                Where(c => c.CourseName.ToLower().Contains(courseName.ToLower())).
                Where(c => c.Category.ToLower().Contains(category.ToLower())).
                Where(c => languageId == 0 ? c.Language!.Id >= 0  : c.Language!.Id == languageId ).
                Where(c => deliveryMethodId == 0 ? c.DeliveryMethod!.Id >= 0  : c.DeliveryMethod!.Id == deliveryMethodId ).
                Select(c => new CourseDto
                {
                    Id = c.Id,
                    CourseName = c.CourseName,   
                    InstituteName = c.InstituteName,
                    Category = c.Category,
                    DeliveryMethod =  c.DeliveryMethod!.MethodName,
                    Language = c.Language!.LanguageName,
                    Location = c.Location,
                    StartDate = c.StartDate
                })
                .ToListAsync(cToken);

            return courses;
        }

        public async Task<List<string>> SearchCoursesByName(string courseNameKeyword, CancellationToken cToken)
        {
            var coursesNames = await _context.Courses.
                Where(c => c.CourseName.ToLower().Contains(courseNameKeyword.ToLower())).
                Select(c => c.CourseName).
                ToListAsync(cToken);

            return coursesNames;
        }
    }
}
