using FluentValidation;
using MCB.Core.Domain.Entities.Abstractions;
using MCB.Core.Domain.Entities.DomainEntitiesBase.Validators.Interfaces;
using MCB.Core.Infra.CrossCutting.DesignPatterns.Validator;

namespace MCB.Core.Domain.Entities.DomainEntitiesBase.Validators;

public abstract class DomainEntityBaseValidator<TDomainEntityBase>
    : ValidatorBase<TDomainEntityBase>,
    IDomainEntityBaseValidator<TDomainEntityBase>
    where TDomainEntityBase : IDomainEntity
{
    // Protected Methods
    protected override void ConfigureFluentValidationConcreteValidator(FluentValidationValidatorWrapper fluentValidationValidatorWrapper)
    {
        // Id
        fluentValidationValidatorWrapper.RuleFor(input => input.Id)
            .Must(Id => Id != Guid.Empty)
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveIdErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveIdMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveIdSeverity);

        // TenantId
        fluentValidationValidatorWrapper.RuleFor(input => input.TenantId)
            .Must(tenantId => tenantId != Guid.Empty)
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveTenantIdErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveTenantIdMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveTenantIdSeverity);

        // CreatedBy
        fluentValidationValidatorWrapper.RuleFor(input => input.AuditableInfo.CreatedBy)
            .Must(CreatedBy => !string.IsNullOrWhiteSpace(CreatedBy))
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedByErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedByMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedBySeverity);

        fluentValidationValidatorWrapper.RuleFor(input => input.AuditableInfo.CreatedBy)
            .Must(CreatedBy => CreatedBy?.Length <= 150)
            .When(DomainEntityBase => !string.IsNullOrWhiteSpace(DomainEntityBase.AuditableInfo.CreatedBy))
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedByWithValidLengthErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedByWithValidLengthMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedByWithValidLengthSeverity);

        // CreatedAt
        fluentValidationValidatorWrapper.RuleFor(input => input.AuditableInfo.CreatedAt)
            .Must(CreatedAt => CreatedAt != default)
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedAtErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedAtMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedAtSeverity);

        fluentValidationValidatorWrapper.RuleFor(input => input.AuditableInfo.CreatedAt)
            .Must(CreatedAt => CreatedAt > default(DateTime) && CreatedAt < DateTime.UtcNow)
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedAtWithValidLengthErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedAtWithValidLengthMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedAtWithValidLengthSeverity);

        // LastUpdatedBy
        fluentValidationValidatorWrapper.RuleFor(input => input.AuditableInfo.LastUpdatedBy)
            .Must(LastUpdatedBy => LastUpdatedBy?.Length <= 150)
            .When(DomainEntityBase => !string.IsNullOrWhiteSpace(DomainEntityBase.AuditableInfo.LastUpdatedBy))
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastUpdatedByWithValidLengthErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastUpdatedByWithValidLengthMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastUpdatedByWithValidLengthSeverity);

        // LastUpdatedAt
        fluentValidationValidatorWrapper.RuleFor(input => input.AuditableInfo.LastUpdatedAt)
            .Must(LastUpdatedAt => LastUpdatedAt > default(DateTime) && LastUpdatedAt < DateTime.UtcNow)
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastUpdatedAtWithValidLengthErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastUpdatedAtWithValidLengthMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastUpdatedAtWithValidLengthSeverity);

        // LastLastSourcePlatform
        fluentValidationValidatorWrapper.RuleFor(input => input.AuditableInfo.LastSourcePlatform)
            .Must(LastSourcePlatform => !string.IsNullOrWhiteSpace(LastSourcePlatform))
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastSourcePlatformErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastSourcePlatformMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastSourcePlatformSeverity);

        fluentValidationValidatorWrapper.RuleFor(input => input.AuditableInfo.LastSourcePlatform)
            .Must(LastSourcePlatform => LastSourcePlatform?.Length <= 150)
            .When(DomainEntityBase => !string.IsNullOrWhiteSpace(DomainEntityBase.AuditableInfo.LastSourcePlatform))
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastSourcePlatformWithValidLengthErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastSourcePlatformWithValidLengthMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastSourcePlatformWithValidLengthSeverity);

        // RegistryVersion
        fluentValidationValidatorWrapper.RuleFor(input => input.RegistryVersion)
            .Must(RegistryVersion => RegistryVersion != default)
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveRegistryVersionErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveRegistryVersionMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveRegistryVersionSeverity);

        fluentValidationValidatorWrapper.RuleFor(input => input.RegistryVersion)
            .Must(RegistryVersion => RegistryVersion > default(DateTime) && RegistryVersion < DateTime.UtcNow)
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveRegistryVersionWithValidLengthErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveRegistryVersionWithValidLengthMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveRegistryVersionWithValidLengthSeverity);

        // CreatedBy and CreatedAt combination
        fluentValidationValidatorWrapper.RuleFor(input => input)
            .Must(input => !string.IsNullOrWhiteSpace(input.AuditableInfo.CreatedBy) && input.AuditableInfo.CreatedAt > default(DateTime))
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedByAndCreatedAtErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedByAndCreatedAtMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedByAndCreatedAtSeverity);

        // LastUpdatedBy and LastUpdatedAt combination
        fluentValidationValidatorWrapper.RuleFor(input => input)
            .Must(input => !string.IsNullOrWhiteSpace(input.AuditableInfo.LastUpdatedBy) && input.AuditableInfo.LastUpdatedAt > default(DateTime))
            .When(input => !string.IsNullOrWhiteSpace(input.AuditableInfo.LastUpdatedBy) || input.AuditableInfo.LastUpdatedAt > default(DateTime))
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastUpdatedByAndLastUpdatedAtErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastUpdatedByAndLastUpdatedAtMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastUpdatedByAndLastUpdatedAtSeverity);

        ConfigureFluentValidationConcreteValidatorInternal(fluentValidationValidatorWrapper);
    }
    protected abstract void ConfigureFluentValidationConcreteValidatorInternal(FluentValidationValidatorWrapper fluentValidationValidatorWrapper);
}
