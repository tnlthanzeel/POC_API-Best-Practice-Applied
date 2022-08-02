using VPMS.SharedKernel.Interfaces;
using VPMS.SharedKernel.Models;

namespace VPMS.Domain.Entities.TodoItemEntities;

public class TodoItem : EntityBase, ICreatedAudit, IUpdatedAudit, IDeletedAudit
{
    public DateTimeOffset CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; private set; }

    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public bool IsDone { get; private set; } = false;
    public DateTimeOffset CompletedOn { get; private set; }

    public TodoItem ChangeTitle(string title)
    {
        Title = title;
        return this;
    }

    public void Delete()
    {
        IsDeleted = true;
    }
}
