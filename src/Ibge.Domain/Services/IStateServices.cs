using Ardalis.Result;
using Ibge.Domain.Command.State;
using Ibge.Domain.DTO;
using Ibge.Domain.DTO.State;

namespace Ibge.Domain.Services;

public interface IStateServices
{
    Task<Result<Guid>> Create(CreateStateCommand request, CancellationToken cancellationToken);
    Task<Result> Update(UpdateStateCommand request, CancellationToken cancellationToken);
    Task<Result> Remove(RemoveStateCommand request, CancellationToken cancellationToken);
    Task<Result<StateResponseDto?>> GetById(Guid id, CancellationToken cancellationToken);
    Task<Result<PagedResponseDto<StateResponseDto?>>> Get(StateQueryParamsDto param, CancellationToken cancellationToken);
    Task<Result<Guid?>> GetIdByCode(int code, CancellationToken cancellationToken);
    Task<bool> AddFromFile(StateFromFileDto item, CancellationToken cancellationToken);
}