using Infrastructure.Interfaces;
using MediatR;

namespace Application.Contacts.Commands.CreateContact
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, int>
    {
        private readonly IContactRepository _contactRepository;

        public CreateContactCommandHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<int> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            return await _contactRepository.CreateContact(request.FullName, request.Email, request.PhoneNumber, request.Address, cancellationToken);
        }
    }
}
