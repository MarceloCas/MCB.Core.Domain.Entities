using MCB.Core.Domain.Entities.Abstractions.Specifications;
using MCB.Core.Domain.Entities.DomainEntitiesBase.Specifications.Interfaces;

namespace MCB.Core.Domain.Entities.DomainEntitiesBase.Specifications;

public class InputBaseSpecifications
    : IInputBaseSpecifications
{
    public bool TenantIdShouldRequired(Guid tenantId)
    {
        return tenantId != Guid.Empty;
    }

    public bool ExecutionUserShouldRequired(string executionUser)
    {
        return !string.IsNullOrWhiteSpace(executionUser);
    }
    public bool ExecutionUserShouldValid(string executionUser)
    {
        return ExecutionUserShouldRequired(executionUser)
            && executionUser.Length <= IDomainEntitySpecifications.DOMAIN_ENTITY_CREATED_BY_MAX_LENGTH;
    }

    public bool SourcePlatformShouldRequired(string sourcePlatform)
    {
        return !string.IsNullOrWhiteSpace(sourcePlatform);
    }
    public bool SourcePlatformShouldValid(string sourcePlatform)
    {
        return SourcePlatformShouldRequired(sourcePlatform)
            && sourcePlatform.Length <= IDomainEntitySpecifications.DOMAIN_ENTITY_LAST_SOURCE_PLATFORM_MAX_LENGTH;
    }

    public bool CorrelationIdShouldRequired(Guid correlationId)
    {
        return correlationId != Guid.Empty;
    }
}
