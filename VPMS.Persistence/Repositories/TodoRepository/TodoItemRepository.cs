using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VPMS.Application.PersistanceInterfaces;
using VPMS.Domain.Entities.TodoItemEntities;
using VPMS.SharedKernel.Models;

namespace VPMS.Persistence.Repositories.TodoRepository
{
    public sealed class TodoItemRepository : BaseRepository, ITodoItemRepository
    {
        private readonly DbSet<TodoItem> _table;

        public TodoItemRepository(VPMSDbContext dbContext) : base(dbContext)
        {
            _table = _dbContext.Todo;
        }

        public TodoItem Add(TodoItem toDoItem)
        {
            _table.Add(toDoItem);

            return toDoItem;
        }

        public async Task<TodoItem?> GetById(Guid id, CancellationToken token, bool asTracking = false)
        {
            var query = asTracking ? _table.AsTracking() : _table;

            var entity = await query.FirstOrDefaultAsync(f => f.Id == id, cancellationToken: token);

            return entity;
        }

        public async Task<TodoItem?> GetByIdSpec(ISpecification<TodoItem> specification, CancellationToken token, bool asTracking = false)
        {
            var query = asTracking ? _table.AsTracking() : _table;

            var entity = await query.WithSpecification(specification).FirstOrDefaultAsync(cancellationToken: token);

            return entity;
        }

        public async Task<(IReadOnlyList<TodoItem> list, int totalRecocrds)> GetListBySpec(Paginator paginator, ISpecification<TodoItem> specification, CancellationToken token)
        {
            var query = _table.WithSpecification(specification);

            var totalRecocrds = await query.CountAsync(cancellationToken: token);

            var entity = await query
                                .Skip((paginator.PageNumber - 1) * paginator.PageSize)
                                .Take(paginator.PageSize)
                                .ToListAsync(cancellationToken: token);

            return (entity, totalRecocrds);
        }
    }
}
