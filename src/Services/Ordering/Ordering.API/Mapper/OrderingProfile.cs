using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.UseCases.Orders.Commands.CheckouOrder;

namespace Ordering.API.Mapper
{
    public class OrderingProfile : Profile
    {
        public OrderingProfile()
        {
            CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
        }
    }
}