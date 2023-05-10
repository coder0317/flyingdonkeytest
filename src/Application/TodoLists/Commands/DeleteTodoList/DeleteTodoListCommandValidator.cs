using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Todo_App.Application.Common.Interfaces;

namespace Todo_App.Application.TodoLists.Commands.DeleteTodoList;

public class DeleteTodoListCommandValidator : AbstractValidator<DeleteTodoListCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoListCommandValidator()
    {
        RuleFor(p => p.Id)
            .GreaterThan(0)
            .WithMessage("Id should be greater than 0.")
            .MustAsync(ValidTodoList).WithMessage("To do list is invalid."); ;
    }

    public async Task<bool> ValidTodoList(int id, CancellationToken cancellationToken)
    {
        var todoList = await _context.TodoLists.SingleOrDefaultAsync(t => t.Id == id);
        return todoList != null ? true : false;
    }
}