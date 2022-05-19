namespace PaymentGateway.Application.Features.PaymentProcessor.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using Commands;

    using CrossCutting;

    using Domain.Interfaces;

    using MediatR;

    using Microsoft.EntityFrameworkCore;

    using Models;
    using Models.PaymentIssuer;

    using PaymentGateway.Application.Core.Handlers;
    using PaymentGateway.Application.Features.PaymentProcessor.Gateways.Interfaces;
    using PaymentGateway.Application.Features.PaymentProcessor.Queries;
    using PaymentGateway.CrossCutting.Exceptions;
    using PaymentGateway.CrossCutting.Extensions;

    public class Handler : BaseHandler,
        IRequestHandler<ProcessPayment, Response<Payment>>,
        IRequestHandler<GetPaymentById, Response<Payment>>,
        IRequestHandler<GetPayments, Response<IEnumerable<Payment>>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IBankApiClient _bankApiClient;

        public Handler(IPaymentRepository paymentRepository, IMapper mapper, IBankApiClient bankApiClient)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _bankApiClient = bankApiClient;
        }

        public async Task<Response<Payment>> Handle(ProcessPayment request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Domain.Models.Payment>(request);

            var issuerRequest = _mapper.Map<Request>(request);

            var bankPaymentRequest = await _bankApiClient.ProcessPaymentAsync(issuerRequest, cancellationToken);

            Domain.Models.Payment modelWithBankResponse = _mapper.Map(bankPaymentRequest, model);

            if (!request.Source.SaveCardInfo)
            {
                modelWithBankResponse.ClearCardInfo();
            }

            await _paymentRepository.InsertAsync(modelWithBankResponse, cancellationToken);

            return new Response<Payment>(_mapper.Map<Payment>(modelWithBankResponse));
        }

        public Task<Response<Payment>> Handle(GetPaymentById request, CancellationToken cancellationToken)
        {
            var response = _paymentRepository
                .Query()
                .FirstOrDefault(_ => _.MerchantId == request.MerchantId);

            Guard.Against<NotFoundException>(response.IsNull());

            return Task.FromResult(new Response<Payment>(_mapper.Map<Payment>(response)));
        }
        
        public Task<Response<IEnumerable<Payment>>> Handle(GetPayments request, CancellationToken cancellationToken)
        {
            var response = _paymentRepository
                .Query()
                .Where(_ => _.MerchantId == request.MerchantId);

            Guard.Against<NotFoundException>(response.IsNull());

            return Task.FromResult(new Response<IEnumerable<Payment>>(_mapper.Map< IEnumerable<Payment>>(response)));
        }
    }
}
