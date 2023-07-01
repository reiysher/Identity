namespace Identity.Domain.Common;

public interface IEntity
{
    IReadOnlyCollection<INotification>? DomainEvents { get; }
    void AddDomainEvent(INotification eventItem);
    void RemoveDomainEvent(INotification eventItem);
    void ClearDomainEvents();
}
