using AutoMapper;
using MediatR;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Application.TodoLists.Queries.GetTodos;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.Tags.Commands.AddTag;

public class AddTagCommand : IRequest<int>
{
    public int ItemId { get; set; }
    public string Name { get; set; }
}

public class AddTagCommandHandler : IRequestHandler<AddTagCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddTagCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(AddTagCommand request, CancellationToken cancellationToken)
    {
        var tagInfo = new Tag
        {
            ItemId = request.ItemId,
            Name = request.Name
        };

        await _context.Tags.AddAsync(tagInfo);
        await _context.SaveChangesAsync(cancellationToken);

        return tagInfo.Id;
    }
}