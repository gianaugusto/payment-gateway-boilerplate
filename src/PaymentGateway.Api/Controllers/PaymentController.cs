namespace PaymentGateway.Api.Controllers
{
    using Application.Features.PaymentProcessor.Models;
    using MediatR;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Application.Features.PaymentProcessor.Commands;
using PaymentGateway.CrossCutting;

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize("pay:read-write")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProcessPayment model, CancellationToken cancellationToken)
        {
            var result = await this._mediator.Send(model, cancellationToken);

            return Ok(result);
        }
    }
}
