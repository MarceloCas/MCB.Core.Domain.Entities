namespace MCB.Core.Domain.Entities.DomainEntitiesBase.Specifications.Interfaces;

public interface IInputBaseSpecifications
{
    bool TenantIdShouldRequired(Guid tenantId);

    bool ExecutionUserShouldRequired(string executionUser);
    bool ExecutionUserShouldValid(string executionUser);

    bool SourcePlatformShouldRequired(string sourcePlatform);
    bool SourcePlatformShouldValid(string sourcePlatform);

    bool CorrelationIdShouldRequired(Guid correlationId);
}
