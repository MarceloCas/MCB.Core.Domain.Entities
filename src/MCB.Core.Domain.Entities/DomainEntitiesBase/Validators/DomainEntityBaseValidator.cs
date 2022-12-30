using FluentValidation;
using MCB.Core.Domain.Entities.Abstractions;
using MCB.Core.Domain.Entities.Abstractions.Specifications;
using MCB.Core.Domain.Entities.DomainEntitiesBase.Validators.Interfaces;
using MCB.Core.Infra.CrossCutting.Abstractions.DateTime;
using MCB.Core.Infra.CrossCutting.DesignPatterns.Validator;

namespace MCB.Core.Domain.Entities.DomainEntitiesBase.Validators;

public abstract class DomainEntityBaseValidator<TDomainEntityBase>
    : ValidatorBase<TDomainEntityBase>,
    IDomainEntityBaseValidator<TDomainEntityBase>
    where TDomainEntityBase : IDomainEntity
{
    // Properties
    protected IDateTimeProvider DateTimeProvider { get; }
    protected IDomainEntitySpecifications DomainEntitySpecifications { get; }

    // Constructors
    protected DomainEntityBaseValidator(
        IDateTimeProvider dateTimeProvider,
        IDomainEntitySpecifications domainEntitySpecifications
    )
    {
        DateTimeProvider = dateTimeProvider;
        DomainEntitySpecifications = domainEntitySpecifications;
    }

    // Protected Methods
    protected override void ConfigureFluentValidationConcreteValidator(FluentValidationValidatorWrapper fluentValidationValidatorWrapper)
    {
        // Id
        fluentValidationValidatorWrapper.RuleFor(domainEntityBase => domainEntityBase.Id)
            .Must(DomainEntitySpecifications.IdShouldRequired)
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveIdErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveIdMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveIdSeverity);

        // TenantId
        fluentValidationValidatorWrapper.RuleFor(domainEntityBase => domainEntityBase.TenantId)
            .Must(DomainEntitySpecifications.TenantIdShouldRequired)
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveTenantIdErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveTenantIdMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveTenantIdSeverity);

        // CreatedBy
        fluentValidationValidatorWrapper.RuleFor(domainEntityBase => domainEntityBase.AuditableInfo.CreatedBy)
            .Must(DomainEntitySpecifications.CreatedByShouldRequired)
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedByErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedByMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedBySeverity);

        fluentValidationValidatorWrapper.RuleFor(domainEntityBase => domainEntityBase.AuditableInfo.CreatedBy)
            .Must(DomainEntitySpecifications.CreatedByShouldValid)
            .When(domainEntityBase => DomainEntitySpecifications.CreatedByShouldRequired(domainEntityBase.AuditableInfo.CreatedBy))
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedByWithValidLengthErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedByWithValidLengthMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedByWithValidLengthSeverity);

        // CreatedAt
        fluentValidationValidatorWrapper.RuleFor(domainEntityBase => domainEntityBase.AuditableInfo.CreatedAt)
            .Must(DomainEntitySpecifications.CreatedAtShouldRequired)
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedAtErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedAtMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedAtSeverity);

        fluentValidationValidatorWrapper.RuleFor(domainEntityBase => domainEntityBase.AuditableInfo.CreatedAt)
            .Must(DomainEntitySpecifications.CreatedAtShouldValid)
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedAtWithValidLengthErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedAtWithValidLengthMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveCreatedAtWithValidLengthSeverity);

        // LastUpdatedBy
        fluentValidationValidatorWrapper.RuleFor(domainEntityBase => domainEntityBase.AuditableInfo.LastUpdatedBy)
            .Must(DomainEntitySpecifications.LastUpdatedByShouldValid)
            .When(domainEntityBase => !string.IsNullOrWhiteSpace(domainEntityBase.AuditableInfo.LastUpdatedBy))
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastUpdatedByWithValidLengthErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastUpdatedByWithValidLengthMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastUpdatedByWithValidLengthSeverity);

        // LastUpdatedAt
        fluentValidationValidatorWrapper.RuleFor(domainEntityBase => domainEntityBase)
            .Must(domainEntityBase => DomainEntitySpecifications.LastUpdatedAtShouldValid(domainEntityBase.AuditableInfo.LastUpdatedAt, domainEntityBase.AuditableInfo.CreatedAt))
            .When(domainEntityBase => DomainEntitySpecifications.LastUpdatedAtShouldRequired(domainEntityBase.AuditableInfo.LastUpdatedAt))
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastUpdatedAtWithValidLengthErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastUpdatedAtWithValidLengthMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastUpdatedAtWithValidLengthSeverity);

        // LastLastSourcePlatform
        fluentValidationValidatorWrapper.RuleFor(domainEntityBase => domainEntityBase.AuditableInfo.LastSourcePlatform)
            .Must(DomainEntitySpecifications.LastSourcePlatformShouldRequired)
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastSourcePlatformErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastSourcePlatformMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastSourcePlatformSeverity);

        fluentValidationValidatorWrapper.RuleFor(domainEntityBase => domainEntityBase.AuditableInfo.LastSourcePlatform)
            .Must(DomainEntitySpecifications.LastSourcePlatformShouldValid)
            .When(domainEntityBase => DomainEntitySpecifications.LastSourcePlatformShouldRequired(domainEntityBase.AuditableInfo.LastSourcePlatform))
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastSourcePlatformWithValidLengthErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastSourcePlatformWithValidLengthMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveLastSourcePlatformWithValidLengthSeverity);

        // RegistryVersion
        fluentValidationValidatorWrapper.RuleFor(domainEntityBase => domainEntityBase.RegistryVersion)
            .Must(DomainEntitySpecifications.RegistryVersionShouldRequired)
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveRegistryVersionErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveRegistryVersionMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveRegistryVersionSeverity);

        fluentValidationValidatorWrapper.RuleFor(domainEntityBase => domainEntityBase.RegistryVersion)
            .Must(DomainEntitySpecifications.RegistryVersionShouldValid)
            .When(domainEntityBase => DomainEntitySpecifications.RegistryVersionShouldRequired(domainEntityBase.RegistryVersion))
            .WithErrorCode(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveRegistryVersionWithValidLengthErrorCode)
            .WithMessage(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveRegistryVersionWithValidLengthMessage)
            .WithSeverity(IDomainEntityBaseValidator.DomainEntityBaseShouldHaveRegistryVersionWithValidLengthSeverity);

        ConfigureFluentValidationConcreteValidatorInternal(fluentValidationValidatorWrapper);
    }
    protected abstract void ConfigureFluentValidationConcreteValidatorInternal(FluentValidationValidatorWrapper fluentValidationValidatorWrapper);
}
