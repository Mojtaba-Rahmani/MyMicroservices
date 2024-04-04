

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contrat.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Commen.Entities;

namespace Ordering.Application.Feutures.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<DeleteOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderForDelete = await _orderRepository.GetByIdAsync(request.Id);

            if (orderForDelete == null)
            {

                _logger.LogError("ordder not exists");
                throw new NotFoundException(nameof(Order), request.Id);
            }
                

            else
            {
                await _orderRepository.DeleteAsynk(orderForDelete);
                _logger.LogInformation($"order {orderForDelete.Id} is Successfully Deleted");
            }

           

            return Unit.Value;   
        }

    }
}
