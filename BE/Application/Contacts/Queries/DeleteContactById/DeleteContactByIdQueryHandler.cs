using Infrastructure.Interfaces;
using MediatR;

namespace Application.Contacts.Queries.DeleteContactById
{
    public class DeleteContactByIdQueryHandler : IRequestHandler<DeleteContactByIdQuery, int>
    {
        private readonly IContactRepository _contactRepository;

        public DeleteContactByIdQueryHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<int> Handle(DeleteContactByIdQuery request, CancellationToken cancellationToken)
        {
            return await _contactRepository.DeleteContactById(request.Id, cancellationToken);
            
        }
    }
}
