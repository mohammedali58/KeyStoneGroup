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
    public class ContactRepository : IContactRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly  IMapper _mapper;

        public ContactRepository(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<ContactDto> AddCourse(CourseDto course, int contactId, CancellationToken cToken)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == contactId);

            if (contact is null)
            {
                throw new HttpException(StatusCodes.Status404NotFound, "contact not found");
            }
            await _context.SaveChangesAsync(cToken);

            ContactDto contactDto = new()
            {
                FullName = contact.FullName,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                Address = contact.Address,
                Course = course
            };

            return contactDto;

        }

        public async Task<int> CreateContact(string fullName, string email, string phoneNumber, string address, CancellationToken cToken)
        {
            Contact contact = new() { 
                FullName = fullName, 
                Email = email,
                PhoneNumber = phoneNumber, 
                Address = address,
                Course = null
            };
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync(cToken);
            return contact.Id;
        }

        public async Task<int> DeleteContactById(int id, CancellationToken cToken)
        {
            var contact = _context.Contacts.FirstOrDefault(x => x.Id == id);

            if (contact is null)
            {
                throw new HttpException(StatusCodes.Status404NotFound, "course not found");
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync(cToken);
            return contact.Id;
        }

        public async Task<ContactDto> GetContactById(int id, CancellationToken cToken)
        {
            var contact = await _context.Contacts.
                Include(c => c.Course).
                ThenInclude(c => c.DeliveryMethod).
                Include(c => c.Course).
                ThenInclude(c => c.Language).
                FirstOrDefaultAsync(c => c.Id == id).
                ConfigureAwait(false);

            if (contact is null)
            {
                throw new HttpException(StatusCodes.Status404NotFound, "course not found");
            }

            ContactDto contactDto = new()
            {
                FullName = contact.FullName,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                Address = contact.Address,
                Course = contact.Course == null ? null : new CourseDto()
                {
                    Id = contact.Course.Id,
                    CourseName = contact.Course.CourseName,
                    Category = contact.Course.Category,
                    InstituteName = contact.Course.InstituteName,
                    DeliveryMethod = contact.Course.DeliveryMethod!.MethodName,
                    Language = contact.Course.Language!.LanguageName,
                    Location = contact.Course.Location,
                    StartDate = contact.Course.StartDate,
                }
            };

            return contactDto;
        }

       
    }
}
