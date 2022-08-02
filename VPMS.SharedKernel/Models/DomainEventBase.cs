using MediatR;

namespace VPMS.SharedKernel.Models;

public abstract class DomainEventBase : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
