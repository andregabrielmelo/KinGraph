using Ardalis.Specification.EntityFrameworkCore;

namespace KinGraph.Infrastructure.Data;

// inherit from Ardalis.Specification type
public class EntityFrameworkRepository<T>(ApplicationDatabaseContext dbContext)
    : RepositoryBase<T>(dbContext),
        IRepository<T>
    where T : class, IAggregateRoot { }
