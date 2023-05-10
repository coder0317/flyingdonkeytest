using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Todo_App.Application.Common.Exceptions;
using Todo_App.Application.TodoItems.Commands.CreateTodoItem;
using Todo_App.Application.TodoItems.Commands.DeleteTodoItem;
using Todo_App.Application.TodoLists.Commands.CreateTodoList;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.IntegrationTests.TodoItems.Commands;

using static Testing;

public class DeleteTodoItemTests : BaseTestFixture
{
    [TestCaseSource(typeof(TestData.TestData), nameof(TestData.TestData.TodoIemIdsPassingData))]
    //[TestCaseSource(typeof(TestData.TestData), nameof(TestData.TestData.TodoItemIdsFailingData))]
    public async Task ShouldRequireValidTodoItemId(int id)
    {
        var command = new DeleteTodoItemCommand(id);

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [TestCaseSource(typeof(TestData.TestData), nameof(TestData.TestData.TodoIemIdsPassingData))]
    //[TestCaseSource(typeof(TestData.TestData), nameof(TestData.TestData.TodoItemIdsFailingData))]
    public async Task ShouldSoftDeleteTodoItem(int id)
    {
        var itemId = await SendAsync(new DeleteTodoItemCommand(id));

        var item = await FindAsync<TodoItem>(itemId);

        item.Should().NotBeNull();
        item.IsSoftDeleted.Should().BeTrue();
    }
}
