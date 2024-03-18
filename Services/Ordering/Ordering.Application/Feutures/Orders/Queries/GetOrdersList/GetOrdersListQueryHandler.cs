

using AutoMapper;
using MediatR;
using Ordering.Application.Contrat.Persistence;

namespace Ordering.Application.Feutures.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrdersVm>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper = null)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<List<OrdersVm>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var ordersList = await _orderRepository.GetOrdersByUserName(request.UserName);

            return _mapper.Map<List<OrdersVm>>(ordersList);
        }
    }
}
