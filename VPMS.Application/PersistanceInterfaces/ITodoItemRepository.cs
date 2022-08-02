using Ardalis.Specification;
using VPMS.SharedKernel.Models;

namespace VPMS.Application.PersistanceInterfaces;

public interface ITodoItemRepository : IBaseRepository
{
    public Domain.Entities.TodoItemEntities.TodoItem Add(Domain.Entities.TodoItemEntities.TodoItem toDoItem);

    /// <summary>
    /// Gets a requested item by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="asTracking">This is only supported by entity framework data providers which supports change tracking</param>
    /// <returns>Return the entity or null</returns>
    Task<Domain.Entities.TodoItemEntities.TodoItem?> GetById(Guid id, CancellationToken token, bool asTracking = false);

    /// <summary>
    /// Get an entity by i
    /// </summary>
    /// <param name="specification">A specification to be applied when querying the entity</param>
    /// <param name="token"></param>
    /// <param name="asTracking"></param>
    /// <returns>Returns a Todoitem or null</returns>
    Task<Domain.Entities.TodoItemEntities.TodoItem?> GetByIdSpec(ISpecification<Domain.Entities.TodoItemEntities.TodoItem> specification, CancellationToken token, bool asTracking = false);

    /// <summary>
    /// Gets a list of paginated result
    /// </summary>
    /// <param name="specification">A specification to be applied when querying the entity</param>
    /// <param name="filter">The number or recrds to skip and page</param>
    /// <returns>Returns a list or an empty list if no records found</returns>
    Task<(IReadOnlyList<Domain.Entities.TodoItemEntities.TodoItem> list, int totalRecocrds)> GetListBySpec(Paginator paginator, ISpecification<Domain.Entities.TodoItemEntities.TodoItem> specification, CancellationToken token);
}
