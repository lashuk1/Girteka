using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Girteka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase : Controller
    {
        private ISender? _mediator;

#pragma warning disable CS8603 // Possible null reference return.
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    }
}
