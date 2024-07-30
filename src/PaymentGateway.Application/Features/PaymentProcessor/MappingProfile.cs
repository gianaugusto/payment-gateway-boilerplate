namespace PaymentGateway.Application.Features.PaymentProcessor
{
    using AutoMapper;

    using Models.PaymentIssuer;

    using PaymentGateway.Application.Features.PaymentProcessor.Commands;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Models.Payment, Models.Payment>().ReverseMap();
            CreateMap<Domain.Models.Source, Models.Source>().ReverseMap();
            CreateMap<ProcessPayment, Models.Payment>().ReverseMap();
            CreateMap<ProcessPayment, Domain.Models.Payment>().ReverseMap();
            CreateMap<ProcessPayment, Request>().ReverseMap();

            CreateMap<Domain.Models.Payment, Response>()
                .ForMember(u => u.Id, opt => opt.MapFrom(x => x.IssuerPaymentId))
                .ForMember(u => u.Status, opt => opt.Ignore())
                .ForMember(u => u.Source, opt => opt.MapFrom(x => x.Source))
                .ReverseMap();
        }
    }
}
