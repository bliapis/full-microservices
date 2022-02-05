using FluentValidation;

namespace Ordering.Application.UseCases.Orders.Commands.CheckouOrder
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty()
                    .WithMessage("{UserName} id required.")
                .NotNull()
                .MaximumLength(50)
                    .WithMessage("{UserName} must not exceed 50 characters.");

            RuleFor(p => p.EmailAddress)
                .NotEmpty().WithMessage("{EmailAddress} is required.");

            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} is required.")
                .GreaterThan(0).WithMessage("{TotalPrice} must be greater than zero.");
        }
    }
}