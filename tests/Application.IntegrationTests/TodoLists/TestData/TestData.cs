namespace Todo_App.Application.IntegrationTests.TodoLists.TestData;

public class TestData
{
    #region Todo List
    public static object[] TodoListIdsPassingData =
    {
        new object[] { 1090 }
    };

    public static object[] TodoListIdsFailingData =
    {
        new object[] { 100 },
        new object[] { 101 }
    };

    public static object[] CreateTodoListPassingData =
    {
        new object[] { "Todo 1" },
        new object[] { "Todo 2" },
        new object[] { "Todo 3" },
        new object[] { "Todo 4" },
        new object[] { "Todo 5" },
        new object[] { "Todo 6" },
        new object[] { "Todo 7" },
        new object[] { "Todo 8" },
        new object[] { "Todo 9" },
        new object[] { "Todo 10" }
    };

    public static object[] CreateTodoListFailingData =
    {
        new object[] { "" },
        new object[] { null },
        new object[] { "" },
        new object[] { null },
        new object[] { "" },
        new object[] { null },
        new object[] { "" },
        new object[] { null },
        new object[] { "Test" },
        new object[] { "Test" }
    };
    #endregion

    #region Todo Item
    #endregion

    #region Tags
    #endregion
}