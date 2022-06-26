using Microsoft.EntityFrameworkCore;
using Sonar.UserTracksManagement.Application.Database;
using Sonar.UserTracksManagement.Application.Tools;
using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Application.Repositories;

public class TagRepository : ITagRepository
{
    private readonly UserTracksManagementDatabaseContext _databaseContext;

    public TagRepository(
        UserTracksManagementDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Tag> AddAsync(
        string tagName, 
        CancellationToken cancellationToken)
    {
        CheckName(tagName);
        var tag = await _databaseContext.Tags
            .FirstOrDefaultAsync(
                tag => tag.Name == tagName, 
                cancellationToken: cancellationToken);
        
        if (tag is not null)
        {
            return tag;
        }

        tag = new Tag(tagName);
        await _databaseContext.Tags
            .AddAsync(tag, cancellationToken);
        await _databaseContext
            .SaveChangesAsync(cancellationToken);
        return tag;
    }

    public async Task<Tag> GetAsync(
        string tagName, 
        CancellationToken cancellationToken)
    {
        CheckName(tagName);
        var tag = await _databaseContext.Tags
            .FirstOrDefaultAsync(
                tag => tag.Name == tagName, 
                cancellationToken: cancellationToken);
        
        if (tag is null)
        {
            throw new NotFoundArgumentsException("Couldn't find tag with given name");
        }

        return tag;
    }

    private static void CheckName(
        string tagName)
    {
        if (string.IsNullOrWhiteSpace(tagName))
        {
            throw new InvalidArgumentsException("Tag name can't be empty or contain only whitespaces");
        }
    }
}