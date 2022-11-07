﻿using MCB.Core.Domain.Entities.Abstractions;
using MCB.Core.Domain.Entities.Abstractions.DomainEvents;

namespace MCB.Core.Domain.Entities.DomainEntitiesBase.Events;

public abstract record DomainEventBase
    : IDomainEvent
{
    // Public Methods
    public Guid Id { get; }
    public DateTimeOffset Timestamp { get; }
    public string DomainEventType { get; }
    public IAggregationRoot AggregationRoot { get; }

    // Constructors
    protected DomainEventBase(Guid id, DateTimeOffset timestamp, string domainEventType, IAggregationRoot aggregationRoot)
    {
        Id = id;
        Timestamp = timestamp;
        DomainEventType = domainEventType;
        AggregationRoot = aggregationRoot;
    }
}
