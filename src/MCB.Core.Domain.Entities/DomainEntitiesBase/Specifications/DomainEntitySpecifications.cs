using MCB.Core.Domain.Entities.Abstractions.Specifications;
using MCB.Core.Infra.CrossCutting.Abstractions.DateTime;

namespace MCB.Core.Domain.Entities.DomainEntitiesBase.Specifications;

public class DomainEntitySpecifications
    : IDomainEntitySpecifications
{
    // Protected Properties
    protected IDateTimeProvider DateTimeProvider { get; }

    // Constructors
    public DomainEntitySpecifications(IDateTimeProvider dateTimeProvider)
    {
        DateTimeProvider = dateTimeProvider;
    }

    // Constructors
    public bool IdShouldRequired(Guid id)
    {
        return id != Guid.Empty;
    }

    public bool TenantIdShouldRequired(Guid tenantId)
    {
        return tenantId != Guid.Empty;
    }

    public bool CreatedAtShouldRequired(DateTime createdAt)
    {
        return createdAt != DateTime.MinValue;
    }
    public bool CreatedAtShouldValid(DateTime createdAt)
    {
        return CreatedAtShouldRequired(createdAt)
            && createdAt <= DateTimeProvider.GetDate();
    }

    public bool CreatedByShouldRequired(string createdBy)
    {
        return !string.IsNullOrWhiteSpace(createdBy);
    }
    public bool CreatedByShouldValid(string createdBy)
    {
        return CreatedByShouldRequired(createdBy)
            && createdBy.Length <= IDomainEntitySpecifications.DOMAIN_ENTITY_CREATED_BY_MAX_LENGTH;
    }

    public bool LastUpdatedAtShouldRequired(DateTime? lastUpdatedAt)
    {
        return lastUpdatedAt != null
            && lastUpdatedAt != DateTime.MinValue;
    }
    public bool LastUpdatedAtShouldValid(DateTime? lastUpdatedAt, DateTime createdAt)
    {
        return LastUpdatedAtShouldRequired(lastUpdatedAt)
            && lastUpdatedAt > createdAt
            && lastUpdatedAt <= DateTimeProvider.GetDate();
    }

    public bool LastUpdatedByShouldRequired(string? lastUpdatedBy)
    {
        return !string.IsNullOrWhiteSpace(lastUpdatedBy);
    }
    public bool LastUpdatedByShouldValid(string? lastUpdatedBy)
    {
        return LastUpdatedByShouldRequired(lastUpdatedBy)
            && lastUpdatedBy!.Length <= IDomainEntitySpecifications.DOMAIN_ENTITY_LAST_UPDATED_BY_MAX_LENGTH;
    }

    public bool LastSourcePlatformShouldRequired(string lastSourcePlatform)
    {
        return !string.IsNullOrWhiteSpace(lastSourcePlatform);
    }
    public bool LastSourcePlatformShouldValid(string lastSourcePlatform)
    {
        return LastSourcePlatformShouldRequired(lastSourcePlatform)
            && lastSourcePlatform.Length <= IDomainEntitySpecifications.DOMAIN_ENTITY_LAST_SOURCE_PLATFORM_MAX_LENGTH;
    }

    public bool RegistryVersionShouldRequired(DateTime registryVersion)
    {
        return registryVersion != DateTime.MinValue;
    }
    public bool RegistryVersionShouldValid(DateTime registryVersion)
    {
        return RegistryVersionShouldRequired(registryVersion)
            && registryVersion <= DateTimeProvider.GetDate();
    }
}
