using Domain.Dtos;
using Domain.Entities;


namespace Infrastructure.Interfaces
{
    public interface IContactRepository
    {
        public Task<int> CreateContact(string fullName, string email, string phoneNumber, string address, CancellationToken cToken);

        public Task<int> DeleteContactById(int id, CancellationToken cToken);

        public Task<ContactDto> GetContactById(int id, CancellationToken cToken);

        public Task<ContactDto> AddCourse(CourseDto course, int contactId, CancellationToken cToken);
    }
}
