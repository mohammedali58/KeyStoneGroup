using MediatR;
namespace Application.Contacts.Queries.DeleteContactById
{
    public class DeleteContactByIdQuery : IRequest<int>
    {
        public int Id { get; set; }
    }
}
