using MCB.Core.Infra.CrossCutting.DesignPatterns.Validator;
using FluentValidation;
using MCB.Core.Domain.Entities.DomainEntitiesBase.Inputs;
using MCB.Core.Domain.Entities.DomainEntitiesBase.Validators.Interfaces;

namespace MCB.Core.Domain.Entities.DomainEntitiesBase.Validators;

public abstract class InputBaseValidator<TInputBase>
    : ValidatorBase<TInputBase>,
    IInputBaseValidator<TInputBase>
    where TInputBase : InputBase
{
    // Protected Methods
    protected override void ConfigureFluentValidationConcreteValidator(FluentValidationValidatorWrapper fluentValidationValidatorWrapper)
    {
        fluentValidationValidatorWrapper.RuleFor(input => input.TenantId)
            .Must(tenantId => tenantId != Guid.Empty)
            .WithErrorCode(IInputBaseValidator.InputBaseShouldHaveTenantIdErrorCode)
            .WithMessage(IInputBaseValidator.InputBaseShouldHaveTenantIdMessage)
            .WithSeverity(IInputBaseValidator.InputBaseShouldHaveTenantIdSeverity);

        fluentValidationValidatorWrapper.RuleFor(input => input.ExecutionUser)
            .Must(executionUser => !string.IsNullOrWhiteSpace(executionUser))
            .WithErrorCode(IInputBaseValidator.InputBaseShouldHaveExecutionUserErrorCode)
            .WithMessage(IInputBaseValidator.InputBaseShouldHaveExecutionUserMessage)
            .WithSeverity(IInputBaseValidator.InputBaseShouldHaveExecutionUserSeverity);

        fluentValidationValidatorWrapper.RuleFor(input => input.ExecutionUser)
            .Must(executionUser => executionUser?.Length <= 150)
            .When(inputBase => !string.IsNullOrWhiteSpace(inputBase.ExecutionUser))
            .WithErrorCode(IInputBaseValidator.InputBaseShouldHaveExecutionUserWithValidLengthErrorCode)
            .WithMessage(IInputBaseValidator.InputBaseShouldHaveExecutionUserWithValidLengthMessage)
            .WithSeverity(IInputBaseValidator.InputBaseShouldHaveExecutionUserWithValidLengthSeverity);

        fluentValidationValidatorWrapper.RuleFor(input => input.SourcePlatform)
            .Must(sourcePlatform => !string.IsNullOrWhiteSpace(sourcePlatform))
            .WithErrorCode(IInputBaseValidator.InputBaseShouldHaveSourcePlatformErrorCode)
            .WithMessage(IInputBaseValidator.InputBaseShouldHaveSourcePlatformMessage)
            .WithSeverity(IInputBaseValidator.InputBaseShouldHaveSourcePlatformSeverity);

        fluentValidationValidatorWrapper.RuleFor(input => input.SourcePlatform)
            .Must(sourcePlatform => sourcePlatform?.Length <= 150)
            .When(inputBase => !string.IsNullOrWhiteSpace(inputBase.SourcePlatform))
            .WithErrorCode(IInputBaseValidator.InputBaseShouldHaveSourcePlatformWithValidLengthErrorCode)
            .WithMessage(IInputBaseValidator.InputBaseShouldHaveSourcePlatformWithValidLengthMessage)
            .WithSeverity(IInputBaseValidator.InputBaseShouldHaveSourcePlatformWithValidLengthSeverity);

        ConfigureFluentValidationConcreteValidatorInternal(fluentValidationValidatorWrapper);
    }
    protected abstract void ConfigureFluentValidationConcreteValidatorInternal(FluentValidationValidatorWrapper fluentValidationValidatorWrapper);
}
