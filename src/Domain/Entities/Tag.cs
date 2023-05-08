namespace Todo_App.Domain.Entities;

public class Tag : BaseAuditableEntity
{
    public string Name { get; set; }

    public TodoItem Item { get; set; } = null!;
}