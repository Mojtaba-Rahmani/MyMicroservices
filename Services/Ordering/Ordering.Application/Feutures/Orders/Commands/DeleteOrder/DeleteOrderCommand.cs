

using MediatR;

namespace Ordering.Application.Feutures.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest
    {
        public int Id { get; set; }
       
    }
}
