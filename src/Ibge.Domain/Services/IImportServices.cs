using Microsoft.AspNetCore.Http;

namespace Ibge.Domain.Services;

public interface IImportServices
{
    Task ProccessFile(Guid id, IFormFile file, CancellationToken cancellationToken);
}
