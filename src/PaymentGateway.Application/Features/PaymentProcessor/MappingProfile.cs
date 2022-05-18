namespace PaymentGateway.Application.Features.PaymentProcessor
{
    using AutoMapper;

    using PaymentGateway.Application.Features.PaymentProcessor.Commands;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Models.Payment, Models.Payment>().ReverseMap();
            CreateMap<Domain.Models.Source, Models.Source>().ReverseMap();
            CreateMap<ProcessPayment, Models.Payment>().ReverseMap();
            CreateMap<ProcessPayment, Domain.Models.Payment>().ReverseMap();
        }
    }
}
