using AutoMapper;
using Ordering.Application.Feutures.Orders.Commands.CheckoutOrder;
using Ordering.Application.Feutures.Orders.Commands.UpdateOrder;
using Ordering.Application.Feutures.Orders.Queries.GetOrdersList;
using Ordering.Domain.Commen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
