namespace MCB.Core.Domain.Entities.DomainEntitiesBase.Inputs;

public abstract record InputBase
{
    public Guid TenantId { get; }
    public string ExecutionUser { get; }
    public string SourcePlatform { get; }
    public Guid CorrelationId { get; }

    protected InputBase(Guid tenantId, string executionUser, string sourcePlatform, Guid correlationId)
    {
        TenantId = tenantId;
        ExecutionUser = executionUser;
        SourcePlatform = sourcePlatform;
        CorrelationId = correlationId;
    }
}
