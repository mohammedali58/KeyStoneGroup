using Domain.Dtos;
using MediatR;

namespace Application.Contacts.Queries.GetContactById
{
    public class GetContactByIdQuery : IRequest<ContactDto>
    {
        public int Id { get; set; } 
    }
}
