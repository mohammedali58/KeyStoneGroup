using Domain.Entities;
using MediatR;

namespace Application.Contacts.Commands.CreateContact
{
    public class CreateContactCommand : IRequest<int>
    {
        public required string FullName { get; set; }

        public required string Email { get; set; }

        public required string PhoneNumber { get; set; }

        public required string Address { get; set; }

    }
}
