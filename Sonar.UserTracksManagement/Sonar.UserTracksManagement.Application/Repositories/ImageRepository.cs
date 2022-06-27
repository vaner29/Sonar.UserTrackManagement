using Sonar.UserTracksManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Sonar.UserTracksManagement.Application.Database;
using Sonar.UserTracksManagement.Application.Tools;

namespace Sonar.UserTracksManagement.Application.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly UserTracksManagementDatabaseContext _databaseContext;

    public ImageRepository(UserTracksManagementDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task SaveImageAsync(Image image, Stream fileStream, CancellationToken cancellationToken)
    {
        var dirName = Path.GetDirectoryName(image.Path);
        if (dirName == null)
        {
            throw new Exception();
        }
        if (!Directory.Exists(dirName))
        {
            Directory.CreateDirectory(dirName);
        }
        var buffer = new byte[1024];
        using (var ms = new MemoryStream())
        {
            int read;
            while ((read = await fileStream.ReadAsync(buffer, cancellationToken)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
            await File.WriteAllBytesAsync(image.Path, ms.ToArray(), cancellationToken);
        }

        await _databaseContext.Images.AddAsync(image, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<Image> GetImageAsync(Guid imageId, CancellationToken cancellationToken)
    {
        if (imageId.Equals(Guid.Empty))
        {
            throw new InvalidArgumentsException("Guid can't be empty");
        }
        
        var image = await _databaseContext.Images.FirstOrDefaultAsync(
            image => image.Id.Equals(imageId), 
            cancellationToken);

        if (image is null)
        {
            throw new NotFoundArgumentsException("Couldn't find image with given ID");
        }

        return image;
    }

    public async Task<byte[]> GetImageContentAsync(Image image, CancellationToken cancellationToken)
    {
        if (!File.Exists(Path.Combine(image.Path)))
        {
            throw new NotFoundArgumentsException("Couldn't find image with given name");
        }

        using var ms = new MemoryStream();
        var buffer = new byte[1024];
        int read;
        var stream = File.OpenRead(image.Path);
        while ((read = await stream.ReadAsync(buffer, cancellationToken)) > 0)
        {
            ms.Write(buffer, 0, read);
        }
        stream.Close();
        return ms.ToArray();
    }
}