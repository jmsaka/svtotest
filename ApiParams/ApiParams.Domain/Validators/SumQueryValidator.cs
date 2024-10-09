using System.Text.RegularExpressions;

namespace ApiParams.Domain.Validators;

public class SumQueryValidator : AbstractValidator<SumQuery>
{
    public SumQueryValidator()
    {
        RuleFor(x => x.A)
            .NotNull().WithMessage("A cannot be null.")
            .NotEmpty().WithMessage("A cannot be empty.")
            .Must(BeAValidNumber).WithMessage("A must be a valid number.");

        RuleFor(x => x.B)
            .NotNull().WithMessage("B cannot be null.")
            .NotEmpty().WithMessage("B cannot be empty.")
            .Must(BeAValidNumber).WithMessage("B must be a valid number.");
    }

    private static bool BeAValidNumber(int value)
    {
        return Regex.IsMatch(value.ToString(), @"^-?\d+$");
    }
}
