using AutoMapper;
using MediatR;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Application.TodoLists.Queries.GetTodos;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.Tags.Commands.AddTag;

public class AddTagCommand : IRequest<List<TagDto>>
{
    public List<TagDto> Tags { get; set; }
}

public class AddTagCommandHandler : IRequestHandler<AddTagCommand, List<TagDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddTagCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TagDto>> Handle(AddTagCommand request, CancellationToken cancellationToken)
    {
        List<Tag> tags = new();

        if (request.Tags.Count >= 1)
        {
            request.Tags.ForEach((tag) => tags.Add(new Tag 
            {
                ItemId = tag.ItemId,
                Name = tag.Name
            }));
        }

        var mappedTags = _mapper.Map<List<Tag>>(tags);

        await _context.Tags.AddRangeAsync(mappedTags);
        await _context.SaveChangesAsync(cancellationToken);

        return request.Tags;
    }
}