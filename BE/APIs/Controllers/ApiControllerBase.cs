//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace APIs.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public abstract class ApiControllerBase : ControllerBase
//    {
//        private ISender _mediator = null!;

//        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
//    }
//}
