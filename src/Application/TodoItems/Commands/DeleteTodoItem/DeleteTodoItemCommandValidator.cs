using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Todo_App.Application.Common.Interfaces;

namespace Todo_App.Application.TodoItems.Commands.DeleteTodoItem;

public class DeleteTodoItemCommandValidator : AbstractValidator<DeleteTodoItemCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoItemCommandValidator()
    {
        RuleFor(p => p.Id)
            .GreaterThan(0)
            .WithMessage("Id should be greater than 0.")
            .MustAsync(ValidTodoItem).WithMessage("To do item is invalid."); ;
    }

    public async Task<bool> ValidTodoItem(int id, CancellationToken cancellationToken)
    {
        var todoList = await _context.TodoItems.SingleOrDefaultAsync(t => t.Id == id);
        return todoList != null ? true : false;
    }
}