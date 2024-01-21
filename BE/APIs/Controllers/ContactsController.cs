using Application.Contacts.Commands.CreateContact;
using Application.Contacts.Queries.DeleteContactById;
using Application.Contacts.Queries.GetContactById;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ContactDto> GetContact([FromQuery] GetContactByIdQuery query)
        {
            return await _mediator.Send(query).ConfigureAwait(false);
        }

        [HttpDelete]
        public async Task<int> DeleteContact([FromQuery] DeleteContactByIdQuery query)
        {
            return await _mediator.Send<int>(query).ConfigureAwait(false);
        }

        [HttpPost]
        public async Task<int> CreateNewContact(CreateContactCommand command)
        {
            return await _mediator.Send(command).ConfigureAwait(false);
        }
    }
}
