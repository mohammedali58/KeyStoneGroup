using Domain.Dtos;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Contacts.Queries.GetContactById
{
    public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, ContactDto>
    {
        private readonly IContactRepository _contactRepository;

        public GetContactByIdQueryHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<ContactDto> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
           ArgumentNullException.ThrowIfNull(request, nameof(request));

            ContactDto contactDto = await _contactRepository.GetContactById(request.Id, cancellationToken);
            return contactDto;
        }
    }
}
