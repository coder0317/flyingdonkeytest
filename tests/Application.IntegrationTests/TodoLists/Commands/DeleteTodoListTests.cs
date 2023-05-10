using FluentAssertions;
using NUnit.Framework;
using Todo_App.Application.Common.Exceptions;
using Todo_App.Application.TodoLists.Commands.DeleteTodoList;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.IntegrationTests.TodoLists.Commands;

using static Testing;

public class DeleteTodoListTests : BaseTestFixture
{
    [TestCaseSource(typeof(TestData.TestData), nameof(TestData.TestData.TodoListIdsPassingData))]
    //[TestCaseSource(typeof(TestData.TestData), nameof(TestData.TestData.TodoListIdsFailingData))]
    public async Task ShouldRequireValidTodoListId(int id)
    {
        var command = new DeleteTodoListCommand(id);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [TestCaseSource(typeof(TestData.TestData), nameof(TestData.TestData.TodoListIdsPassingData))]
    //[TestCaseSource(typeof(TestData.TestData), nameof(TestData.TestData.TodoListIdsFailingData))]
    public async Task ShouldDeleteTodoList(int id)
    {
        var listId = await SendAsync(new DeleteTodoListCommand(id));

        await SendAsync(new DeleteTodoListCommand(listId));

        var list = await FindAsync<TodoList>(listId);

        list.Should().BeNull();
    }
}
