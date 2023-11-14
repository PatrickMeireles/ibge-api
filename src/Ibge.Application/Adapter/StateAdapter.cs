using Ibge.Domain.Command.State;
using Ibge.Domain.DTO.State;
using Ibge.Domain.Entity;

namespace Ibge.Application.Adapter;

public static class StateAdapter
{
    public static State Create(CreateStateCommand param) =>
        new(param.Code, param.Name, param.Acronym);

    public static StateResponseDto? FromDomain(State? param) => param is null ? null :
        new StateResponseDto()
        {
            Acronym = param.Acronym,
            Code = param.Code,
            Name = param.Name,
            CreatedAt = param.CreatedAt,
            Id = param.Id,
            UpdatedAt = param.UpdatedAt
        };

    public static CreateStateCommand FromFile(StateFromFileDto param) =>
        new CreateStateCommand()
        {
            Acronym = param.Acronym,
            Code = param.Code,
            Name = param.Name
        };
}
