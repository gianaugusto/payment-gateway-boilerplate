namespace PaymentGateway.Application.Features.PaymentProcessor.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
using AutoMapper;

    using Commands;

    using CrossCutting;

    using Domain.Interfaces;

    using MediatR;

    using Models;

    using PaymentGateway.Application.Core.Handlers;

    public class Handler : BaseHandler,
        IRequestHandler<ProcessPayment, Response<Payment>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public Handler(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<Response<Payment>> Handle(ProcessPayment request, CancellationToken cancellationToken)
        {
            try
            {
                var model = _mapper.Map<Domain.Models.Payment>(request);

                await _paymentRepository.InsertAsync(model, cancellationToken);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return new Response<Payment>(null);
        }

    }
}
