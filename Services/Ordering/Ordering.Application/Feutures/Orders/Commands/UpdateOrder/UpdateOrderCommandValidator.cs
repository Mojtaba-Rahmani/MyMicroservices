
using FluentValidation;

namespace Ordering.Application.Feutures.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator:AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(a => a.UserName)
                .NotEmpty().WithMessage("{UserName} is requierd")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters");

            RuleFor(a => a.EmailAddress)
                .NotEmpty().WithMessage("{EmailAddress} is requierd");

            RuleFor(a => a.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} is requierd")
                .GreaterThan(0).WithMessage("{TotalPrice} should be Greater than 0 ");
        }
    }
}
