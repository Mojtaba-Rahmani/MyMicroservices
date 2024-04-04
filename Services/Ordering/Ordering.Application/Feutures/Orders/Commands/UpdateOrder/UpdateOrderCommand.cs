using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Feutures.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand :IRequest
    {
        public int Id { get; set; }
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
