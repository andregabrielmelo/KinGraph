namespace KinGraph.SharedKernel;

public interface IDomainEventHandler<T> : INotificationHandler<T>
    where T : IDomainEvent { }
