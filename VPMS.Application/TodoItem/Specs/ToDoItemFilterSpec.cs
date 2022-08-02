using Ardalis.Specification;

namespace VPMS.Application.TodoItem.Specs;

public class ToDoItemFilterSpec : Specification<Domain.Entities.TodoItemEntities.TodoItem>
{
    public ToDoItemFilterSpec(ToDoItemFilter filter)
    {
        Query.OrderByDescending(o => o.CreatedOn);

        if (filter.Completed.HasValue) Query.Where(x => x.IsDone == filter.Completed.Value);

        if (!string.IsNullOrWhiteSpace(filter.Title)) Query.Search(x => x.Title, "%" + filter.Title + "%");
    }
}
