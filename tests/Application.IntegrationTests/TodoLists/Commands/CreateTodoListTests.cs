using FluentAssertions;
using NUnit.Framework;
using Todo_App.Application.Common.Exceptions;
using Todo_App.Application.TodoLists.Commands.CreateTodoList;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.IntegrationTests.TodoLists.Commands;

using static Testing;

public class CreateTodoListTests : BaseTestFixture
{
    //[TestCaseSource(typeof(TestData.TestData), nameof(TestData.TestData.CreateTodoListPassingData))]
    [TestCaseSource(typeof(TestData.TestData), nameof(TestData.TestData.CreateTodoListFailingData))]
    public async Task ShouldRequireMinimumFields(string todo)
    {
        var command = new CreateTodoListCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [TestCaseSource(typeof(TestData.TestData), nameof(TestData.TestData.CreateTodoListPassingData))]
    //[TestCaseSource(typeof(TestData.TestData), nameof(TestData.TestData.CreateTodoListFailingData))]
    public async Task ShouldRequireUniqueTitle(string todo)
    {
        await SendAsync(new CreateTodoListCommand
        {
            Title = todo
        });

        var command = new CreateTodoListCommand
        {
            Title = todo
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [TestCaseSource(typeof(TestData.TestData), nameof(TestData.TestData.CreateTodoListPassingData))]
    //[TestCaseSource(typeof(TestData.TestData), nameof(TestData.TestData.CreateTodoListFailingData))]
    public async Task ShouldCreateTodoList(string todo)
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new CreateTodoListCommand
        {
            Title = todo
        };

        var id = await SendAsync(command);

        var list = await FindAsync<TodoList>(id);

        list.Should().NotBeNull();
        list!.Title.Should().Be(command.Title);
        list.CreatedBy.Should().Be(userId);
        list.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
