using MCB.Core.Domain.Entities.Abstractions.DomainEvents;
using MCB.Core.Infra.CrossCutting.Abstractions.DateTime;

namespace MCB.Core.Domain.Entities.DomainEntitiesBase.Events.Factories;

public abstract class DomainEventFactoryBase
{
    // Properties
    protected IDateTimeProvider DateTimeProvider { get; }

    // Constructors
    protected DomainEventFactoryBase(IDateTimeProvider dateTimeProvider)
    {
        DateTimeProvider = dateTimeProvider;
    }

    // Protected Methods
    protected (Guid Id, DateTime Timestamp, string DomainEventType) GetBaseEventFields<TDomainEvent>()
        where TDomainEvent : IDomainEvent
    {
        return (
            Id: Guid.NewGuid(),
            Timestamp: DateTimeProvider.GetDate().UtcDateTime,
            DomainEventType: typeof(TDomainEvent).FullName ?? string.Empty
        );
    }
}
