using Ardalis.Result;
using Ibge.Domain.Command.City;
using Ibge.Domain.DTO;
using Ibge.Domain.DTO.City;

namespace Ibge.Domain.Services;

public interface ICityServices
{
    Task<Result<Guid>> Create(CreateCityCommand request, CancellationToken cancellationToken);
    Task<Result> Update(UpdateCityCommand request, CancellationToken cancellationToken);
    Task<Result> Remove(RemoveCityCommand request, CancellationToken cancellationToken);
    Task<Result<CityResponseDto?>> GetById(Guid id, CancellationToken cancellationToken);
    Task<Result<PagedResponseDto<CityResponseDto?>>> Get(CityQueryParamsDto param, CancellationToken cancellationToken);
    Task<bool> AddFromFile(CityFromFileDto item, CancellationToken cancellationToken);
}
