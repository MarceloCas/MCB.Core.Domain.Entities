using MCB.Core.Domain.Entities.Abstractions;
using MCB.Core.Infra.CrossCutting.DesignPatterns.Validator.Abstractions;

namespace MCB.Core.Domain.Entities.DomainEntitiesBase.Validators.Interfaces;

public interface IDomainEntityBaseValidator
{
    // Id
    static readonly string DomainEntityBaseShouldHaveIdErrorCode = nameof(DomainEntityBaseShouldHaveIdErrorCode);
    static readonly string DomainEntityBaseShouldHaveIdMessage = nameof(DomainEntityBaseShouldHaveIdMessage);
    static readonly FluentValidation.Severity DomainEntityBaseShouldHaveIdSeverity = FluentValidation.Severity.Error;

    // TenantId
    static readonly string DomainEntityBaseShouldHaveTenantIdErrorCode = nameof(DomainEntityBaseShouldHaveTenantIdErrorCode);
    static readonly string DomainEntityBaseShouldHaveTenantIdMessage = nameof(DomainEntityBaseShouldHaveTenantIdMessage);
    static readonly FluentValidation.Severity DomainEntityBaseShouldHaveTenantIdSeverity = FluentValidation.Severity.Error;

    // CreatedBy
    static readonly string DomainEntityBaseShouldHaveCreatedByErrorCode = nameof(DomainEntityBaseShouldHaveCreatedByErrorCode);
    static readonly string DomainEntityBaseShouldHaveCreatedByMessage = nameof(DomainEntityBaseShouldHaveCreatedByMessage);
    static readonly FluentValidation.Severity DomainEntityBaseShouldHaveCreatedBySeverity = FluentValidation.Severity.Error;

    static readonly string DomainEntityBaseShouldHaveCreatedByWithValidLengthErrorCode = nameof(DomainEntityBaseShouldHaveCreatedByWithValidLengthErrorCode);
    static readonly string DomainEntityBaseShouldHaveCreatedByWithValidLengthMessage = nameof(DomainEntityBaseShouldHaveCreatedByWithValidLengthMessage);
    static readonly FluentValidation.Severity DomainEntityBaseShouldHaveCreatedByWithValidLengthSeverity = FluentValidation.Severity.Error;

    // CreatedAt
    static readonly string DomainEntityBaseShouldHaveCreatedAtErrorCode = nameof(DomainEntityBaseShouldHaveCreatedAtErrorCode);
    static readonly string DomainEntityBaseShouldHaveCreatedAtMessage = nameof(DomainEntityBaseShouldHaveCreatedAtMessage);
    static readonly FluentValidation.Severity DomainEntityBaseShouldHaveCreatedAtSeverity = FluentValidation.Severity.Error;

    static readonly string DomainEntityBaseShouldHaveCreatedAtWithValidLengthErrorCode = nameof(DomainEntityBaseShouldHaveCreatedAtWithValidLengthErrorCode);
    static readonly string DomainEntityBaseShouldHaveCreatedAtWithValidLengthMessage = nameof(DomainEntityBaseShouldHaveCreatedAtWithValidLengthMessage);
    static readonly FluentValidation.Severity DomainEntityBaseShouldHaveCreatedAtWithValidLengthSeverity = FluentValidation.Severity.Error;

    // LastUpdatedBy
    static readonly string DomainEntityBaseShouldHaveLastUpdatedByErrorCode = nameof(DomainEntityBaseShouldHaveLastUpdatedByErrorCode);
    static readonly string DomainEntityBaseShouldHaveLastUpdatedByMessage = nameof(DomainEntityBaseShouldHaveLastUpdatedByMessage);
    static readonly FluentValidation.Severity DomainEntityBaseShouldHaveLastUpdatedBySeverity = FluentValidation.Severity.Error;

    static readonly string DomainEntityBaseShouldHaveLastUpdatedByWithValidLengthErrorCode = nameof(DomainEntityBaseShouldHaveLastUpdatedByWithValidLengthErrorCode);
    static readonly string DomainEntityBaseShouldHaveLastUpdatedByWithValidLengthMessage = nameof(DomainEntityBaseShouldHaveLastUpdatedByWithValidLengthMessage);
    static readonly FluentValidation.Severity DomainEntityBaseShouldHaveLastUpdatedByWithValidLengthSeverity = FluentValidation.Severity.Error;

    // LastUpdatedAt
    static readonly string DomainEntityBaseShouldHaveLastUpdatedAtErrorCode = nameof(DomainEntityBaseShouldHaveLastUpdatedAtErrorCode);
    static readonly string DomainEntityBaseShouldHaveLastUpdatedAtMessage = nameof(DomainEntityBaseShouldHaveLastUpdatedAtMessage);
    static readonly FluentValidation.Severity DomainEntityBaseShouldHaveLastUpdatedAtSeverity = FluentValidation.Severity.Error;

    static readonly string DomainEntityBaseShouldHaveLastUpdatedAtWithValidLengthErrorCode = nameof(DomainEntityBaseShouldHaveLastUpdatedAtWithValidLengthErrorCode);
    static readonly string DomainEntityBaseShouldHaveLastUpdatedAtWithValidLengthMessage = nameof(DomainEntityBaseShouldHaveLastUpdatedAtWithValidLengthMessage);
    static readonly FluentValidation.Severity DomainEntityBaseShouldHaveLastUpdatedAtWithValidLengthSeverity = FluentValidation.Severity.Error;

    // LastSourcePlatform
    static readonly string DomainEntityBaseShouldHaveLastSourcePlatformErrorCode = nameof(DomainEntityBaseShouldHaveLastSourcePlatformErrorCode);
    static readonly string DomainEntityBaseShouldHaveLastSourcePlatformMessage = nameof(DomainEntityBaseShouldHaveLastSourcePlatformMessage);
    static readonly FluentValidation.Severity DomainEntityBaseShouldHaveLastSourcePlatformSeverity = FluentValidation.Severity.Error;

    static readonly string DomainEntityBaseShouldHaveLastSourcePlatformWithValidLengthErrorCode = nameof(DomainEntityBaseShouldHaveLastSourcePlatformWithValidLengthErrorCode);
    static readonly string DomainEntityBaseShouldHaveLastSourcePlatformWithValidLengthMessage = nameof(DomainEntityBaseShouldHaveLastSourcePlatformWithValidLengthMessage);
    static readonly FluentValidation.Severity DomainEntityBaseShouldHaveLastSourcePlatformWithValidLengthSeverity = FluentValidation.Severity.Error;

    // RegistryVersion
    static readonly string DomainEntityBaseShouldHaveRegistryVersionErrorCode = nameof(DomainEntityBaseShouldHaveRegistryVersionErrorCode);
    static readonly string DomainEntityBaseShouldHaveRegistryVersionMessage = nameof(DomainEntityBaseShouldHaveRegistryVersionMessage);
    static readonly FluentValidation.Severity DomainEntityBaseShouldHaveRegistryVersionSeverity = FluentValidation.Severity.Error;

    static readonly string DomainEntityBaseShouldHaveRegistryVersionWithValidLengthErrorCode = nameof(DomainEntityBaseShouldHaveRegistryVersionWithValidLengthErrorCode);
    static readonly string DomainEntityBaseShouldHaveRegistryVersionWithValidLengthMessage = nameof(DomainEntityBaseShouldHaveRegistryVersionWithValidLengthMessage);
    static readonly FluentValidation.Severity DomainEntityBaseShouldHaveRegistryVersionWithValidLengthSeverity = FluentValidation.Severity.Error;

    // CreatedBy and CreatedAt combination
    static readonly string DomainEntityBaseShouldHaveCreatedByAndCreatedAtErrorCode = nameof(DomainEntityBaseShouldHaveCreatedByAndCreatedAtErrorCode);
    static readonly string DomainEntityBaseShouldHaveCreatedByAndCreatedAtMessage = nameof(DomainEntityBaseShouldHaveCreatedByAndCreatedAtMessage);
    static readonly FluentValidation.Severity DomainEntityBaseShouldHaveCreatedByAndCreatedAtSeverity = FluentValidation.Severity.Error;

    // LastUpdatedBy and LastUpdatedAt combination
    static readonly string DomainEntityBaseShouldHaveLastUpdatedByAndLastUpdatedAtErrorCode = nameof(DomainEntityBaseShouldHaveLastUpdatedByAndLastUpdatedAtErrorCode);
    static readonly string DomainEntityBaseShouldHaveLastUpdatedByAndLastUpdatedAtMessage = nameof(DomainEntityBaseShouldHaveLastUpdatedByAndLastUpdatedAtMessage);
    static readonly FluentValidation.Severity DomainEntityBaseShouldHaveLastUpdatedByAndLastUpdatedAtSeverity = FluentValidation.Severity.Error;
}

public interface IDomainEntityBaseValidator<TDomainEntityBase>
    : IValidator<TDomainEntityBase>
    where TDomainEntityBase : IDomainEntity
{

}
