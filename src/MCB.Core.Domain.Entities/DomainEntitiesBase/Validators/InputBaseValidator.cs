using MCB.Core.Infra.CrossCutting.DesignPatterns.Validator;
using FluentValidation;
using MCB.Core.Domain.Entities.DomainEntitiesBase.Inputs;
using MCB.Core.Domain.Entities.DomainEntitiesBase.Validators.Interfaces;
using MCB.Core.Domain.Entities.DomainEntitiesBase.Specifications.Interfaces;

namespace MCB.Core.Domain.Entities.DomainEntitiesBase.Validators;

public abstract class InputBaseValidator<TInputBase>
    : ValidatorBase<TInputBase>,
    IInputBaseValidator<TInputBase>
    where TInputBase : InputBase
{
    // Properties
    protected IInputBaseSpecifications InputBaseSpecifications { get; }

    // Constructors
    protected InputBaseValidator(IInputBaseSpecifications inputBaseSpecifications)
    {
        InputBaseSpecifications = inputBaseSpecifications;
    }

    // Protected Methods
    protected override void ConfigureFluentValidationConcreteValidator(FluentValidationValidatorWrapper fluentValidationValidatorWrapper)
    {
        fluentValidationValidatorWrapper.RuleFor(input => input.TenantId)
            .Must(InputBaseSpecifications.TenantIdShouldRequired)
            .WithErrorCode(IInputBaseValidator.InputBaseShouldHaveTenantIdErrorCode)
            .WithMessage(IInputBaseValidator.InputBaseShouldHaveTenantIdMessage)
            .WithSeverity(IInputBaseValidator.InputBaseShouldHaveTenantIdSeverity);

        fluentValidationValidatorWrapper.RuleFor(input => input.ExecutionUser)
            .Must(InputBaseSpecifications.ExecutionUserShouldRequired)
            .WithErrorCode(IInputBaseValidator.InputBaseShouldHaveExecutionUserErrorCode)
            .WithMessage(IInputBaseValidator.InputBaseShouldHaveExecutionUserMessage)
            .WithSeverity(IInputBaseValidator.InputBaseShouldHaveExecutionUserSeverity);

        fluentValidationValidatorWrapper.RuleFor(input => input.ExecutionUser)
            .Must(InputBaseSpecifications.ExecutionUserShouldValid)
            .When(inputBase => InputBaseSpecifications.ExecutionUserShouldRequired(inputBase.ExecutionUser))
            .WithErrorCode(IInputBaseValidator.InputBaseShouldHaveExecutionUserWithValidLengthErrorCode)
            .WithMessage(IInputBaseValidator.InputBaseShouldHaveExecutionUserWithValidLengthMessage)
            .WithSeverity(IInputBaseValidator.InputBaseShouldHaveExecutionUserWithValidLengthSeverity);

        fluentValidationValidatorWrapper.RuleFor(input => input.SourcePlatform)
            .Must(InputBaseSpecifications.SourcePlatformShouldRequired)
            .WithErrorCode(IInputBaseValidator.InputBaseShouldHaveSourcePlatformErrorCode)
            .WithMessage(IInputBaseValidator.InputBaseShouldHaveSourcePlatformMessage)
            .WithSeverity(IInputBaseValidator.InputBaseShouldHaveSourcePlatformSeverity);

        fluentValidationValidatorWrapper.RuleFor(input => input.SourcePlatform)
            .Must(InputBaseSpecifications.SourcePlatformShouldValid)
            .When(inputBase => InputBaseSpecifications.SourcePlatformShouldRequired(inputBase.SourcePlatform))
            .WithErrorCode(IInputBaseValidator.InputBaseShouldHaveSourcePlatformWithValidLengthErrorCode)
            .WithMessage(IInputBaseValidator.InputBaseShouldHaveSourcePlatformWithValidLengthMessage)
            .WithSeverity(IInputBaseValidator.InputBaseShouldHaveSourcePlatformWithValidLengthSeverity);

        ConfigureFluentValidationConcreteValidatorInternal(fluentValidationValidatorWrapper);
    }
    protected abstract void ConfigureFluentValidationConcreteValidatorInternal(FluentValidationValidatorWrapper fluentValidationValidatorWrapper);
}
