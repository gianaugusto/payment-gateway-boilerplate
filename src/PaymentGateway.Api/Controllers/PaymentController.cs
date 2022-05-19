namespace PaymentGateway.Api.Controllers
{
    using Application.Features.PaymentProcessor.Queries;

    using MediatR;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using PaymentGateway.Application.Features.PaymentProcessor.Commands;

    [ApiController]
    [ApiVersion("1.0")]
    [Authorize("pay:read-write")]
    [Route("api/v{version:apiVersion}/merchants/{merchantId:guid}/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get All payments by merchant
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromRoute] Guid merchantId, CancellationToken cancellationToken)
        {
            var result = await this._mediator.Send(new GetPayments() { MerchantId = merchantId }, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Get payment by merchant id and payment id
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="paymentId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{paymentId:guid}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid merchantId, [FromRoute] Guid paymentId, CancellationToken cancellationToken)
        {
            var result = await this._mediator.Send(new GetPaymentById() { MerchantId = merchantId, PaymentId = paymentId }, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Submit new payment to be processed
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ProcessPayment model, CancellationToken cancellationToken)
        {
            var result = await this._mediator.Send(model, cancellationToken);

            return Created($"{result.Result.CustomerId}", result);
        }
    }
}
