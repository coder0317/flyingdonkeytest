using Microsoft.AspNetCore.Mvc;
using Todo_App.Application.Tags.Commands.AddTag;
using Todo_App.Application.Tags.Commands.DeleteTag;
using Todo_App.Application.TodoLists.Queries.GetTodos;

namespace Todo_App.WebUI.Controllers;

public class TagsController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<List<TagDto>>> CreateTag(AddTagCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteTagCommand(id));

        return NoContent();
    }
}