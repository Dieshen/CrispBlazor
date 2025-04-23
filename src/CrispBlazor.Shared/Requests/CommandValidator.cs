using FluentValidation;

namespace CrispBlazor.Shared.Requests
{
    internal abstract class CommandValidator<TCommand> : AbstractValidator<TCommand>
        where TCommand : Command
    {
    }

    internal abstract class CommandValidator<TCommand, TResponse> : AbstractValidator<TCommand>
        where TCommand : Command<TResponse>
    {
    }

    internal abstract class CreateCommandValidator<TCommand> : CommandValidator<TCommand, Guid>
        where TCommand : CreateCommand
    {
    }

    internal abstract class ModifyCommandValidator<TCommand> : CommandValidator<TCommand>
        where TCommand : ModifyCommand
    {
        public ModifyCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required.");
        }
    }

    internal abstract class DeleteCommandValidator<TCommand> : CommandValidator<TCommand>
        where TCommand : DeleteCommand
    {
        public DeleteCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required.");
        }
    }

    internal abstract class ArchiveCommandValidator<TCommand> : CommandValidator<TCommand>
        where TCommand : ArchiveCommand
    {
        public ArchiveCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required.");
        }
    }
}
