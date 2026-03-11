namespace KinGraph.SharedKernel;

public interface IDomainEvent : INotification
{
    DateTime DateOccurred { get; }
}
