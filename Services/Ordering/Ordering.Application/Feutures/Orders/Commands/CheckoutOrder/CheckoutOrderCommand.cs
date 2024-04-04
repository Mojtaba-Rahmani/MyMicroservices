

using MediatR;

namespace Ordering.Application.Feutures.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommand : IRequest<int>
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }

        // Address

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        // Payment

        public string BankName { get; set; }
        public string RefCode { get; set; }
        public int PaymentMethode { get; set; }
    }
}
