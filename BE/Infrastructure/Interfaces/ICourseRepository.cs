using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface ICourseRepository
    {
        public Task<CourseDto?> GetCourseById(Guid id);

        public Task<List<CourseDto>> GetCourses(string courseName, string category, int languageId, int deliveryMethodId, CancellationToken cToken);

        public Task<List<string>> SearchCoursesByName(string courseNameKeyword, CancellationToken cToken);

    }
}
