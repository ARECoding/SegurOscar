using SegurOsCar.DTOs;
using FluentValidation;
namespace SegurOsCar.Validators
{
    public class ClientInsertValidator : AbstractValidator<ClientInsertDto>
    {
        public ClientInsertValidator()
        {
            RuleFor(client => client.Id)
                .NotEmpty()
                .WithMessage("The client ID is required.")
                .Matches(@"^[1-9][0-9]{2,7}$");
                
            RuleFor(client => client.Email)
                .NotEmpty()
                .Matches(@"^[A-Za-z]{1,5}[\._]{0,1}[0-9]{0,4}@[a-z]{5}\.(com|me|com\.ar)$");
        }
    }
}
