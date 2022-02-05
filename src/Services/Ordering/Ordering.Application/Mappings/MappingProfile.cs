using AutoMapper;
using Ordering.Application.UseCases.Orders.Commands.CheckouOrder;
using Ordering.Application.UseCases.Orders.Commands.UpdateOrder;
using Ordering.Application.UseCases.Orders.Queries.GetOrdersList;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrdersVm>().ReverseMap();
            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}