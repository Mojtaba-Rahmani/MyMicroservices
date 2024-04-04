
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contrat.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Commen.Entities;

namespace Ordering.Application.Feutures.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderForUpdate = await _orderRepository.GetByIdAsync(request.Id);

            if (orderForUpdate == null)
            {
                _logger.LogError("order is not exists");
                throw new NotFoundException(nameof(Order), request.Id);
            }
                

            else
            {
                _mapper.Map(request, orderForUpdate, typeof(UpdateOrderCommand), typeof(Order));

                await _orderRepository.UpdateAsync(orderForUpdate);

                _logger.LogInformation($"order {orderForUpdate.Id} is Successfully updated");
            }
           

            return Unit.Value;
        }
    }
}
