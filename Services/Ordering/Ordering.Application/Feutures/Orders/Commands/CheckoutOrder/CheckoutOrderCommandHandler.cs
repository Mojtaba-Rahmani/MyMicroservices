using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contrat.Infrastructure;
using Ordering.Application.Contrat.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Commen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Feutures.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger _loger;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper , IEmailService emailService, ILogger loger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
            _loger = loger;
        }
        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var newOrder =  await _orderRepository.AddAsync(orderEntity);

            _loger.LogInformation($"order {newOrder.Id} is Successfuly created");
            // Send Email

            SendEmail(newOrder);

            return newOrder.Id;

        }

        private async Task SendEmail(Order order)
        {
            try
            {
              await  _emailService.SendEmail(new Email
                {
                    To = "test@test.com",
                    Subject = "New Order has Created",
                    Body =  "This is body Of Email"
                });
            }
            catch (Exception e)
            {

                _loger.LogError("Email has not Send");
            }
        }

    }
}
