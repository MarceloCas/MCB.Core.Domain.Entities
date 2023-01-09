using MCB.Core.Domain.Entities.Abstractions;
using MCB.Core.Domain.Entities.Abstractions.DomainEvents;

namespace MCB.Core.Domain.Entities.DomainEntitiesBase.Events;

public abstract record DomainEventBase
    : IDomainEvent
{
    // Public Methods
    public Guid CorrelationId { get; }
    public Guid Id { get; }
    public Guid TenantId { get; }
    public DateTime Timestamp { get; }
    public string ExecutionUser { get; }
    public string SourcePlatform { get; }
    public string DomainEventType { get; }
    public IAggregationRoot AggregationRoot { get; }

    // Constructors
    protected DomainEventBase(
        Guid correlationId,
        Guid id, 
        Guid tenantId,
        DateTime timestamp, 
        string executionUser,
        string sourcePlatform,
        string domainEventType, 
        IAggregationRoot aggregationRoot
    )
    {
        CorrelationId = correlationId;
        Id = id;
        TenantId = tenantId;
        Timestamp = timestamp;
        ExecutionUser = executionUser;
        SourcePlatform = sourcePlatform;
        DomainEventType = domainEventType;
        AggregationRoot = aggregationRoot;
    }
}
