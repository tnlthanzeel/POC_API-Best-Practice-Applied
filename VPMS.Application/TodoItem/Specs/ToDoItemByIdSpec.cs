using Ardalis.Specification;

namespace VPMS.Application.TodoItem.Specs;

public sealed class ToDoItemByIdSpec : Specification<Domain.Entities.TodoItemEntities.TodoItem>, ISingleResultSpecification
{
    public ToDoItemByIdSpec(Guid id)
    {
        Query.Where(w => w.Id == id);
    }
}
