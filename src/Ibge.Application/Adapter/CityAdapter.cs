using Ibge.Domain.Command.City;
using Ibge.Domain.DTO.City;
using Ibge.Domain.Entity;

namespace Ibge.Application.Adapter;

public static class CityAdapter
{
    public static City Create(CreateCityCommand param) =>
        new(param.Code, param.Name, param.StateId);

    public static CityResponseDto? FromDomain(City? param) =>
        param == null ? null : new()
        {
            Code = param.Code,
            CreatedAt = param.CreatedAt,
            Id = param.Id,
            Name = param.Name,
            StateId = param.StateId,
            UpdatedAt = param.UpdatedAt
        };

    public static CreateCityCommand FromFile(CityFromFileDto param, Guid StateId) =>
        new()
        {
            Code = param.Code,
            Name = param.Name,
            StateId = StateId,
        };
}
