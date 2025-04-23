using FluentValidation;

namespace CrispBlazor.Shared.Models
{
    public record StoredFile : BaseModel
    {
        public string Extension { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Key { get; set; } = default!;
    }

    internal sealed class StoredFileValidator : AbstractValidator<StoredFile>
    {
        public StoredFileValidator()
        {
            RuleFor(x => x.Extension)
                .NotEmpty()
                .WithMessage("Extension is required");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(x => x.Key)
                .NotEmpty()
                .WithMessage("Key is required");
        }
    }
}
