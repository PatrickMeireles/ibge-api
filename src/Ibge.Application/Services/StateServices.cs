using Ardalis.Result;
using Ibge.Application.Adapter;
using Ibge.Domain.Command.State;
using Ibge.Domain.DTO;
using Ibge.Domain.DTO.State;
using Ibge.Domain.Entity;
using Ibge.Domain.Repository;
using Ibge.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Ibge.Application.Services;

public class StateServices : IStateServices
{
    private readonly IStateRepository _stateRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<StateServices> _logger;

    public StateServices(IStateRepository stateRepository, IMediator mediator, ILogger<StateServices> logger)
    {
        _stateRepository = stateRepository;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<bool> AddFromFile(StateFromFileDto item, CancellationToken cancellationToken)
    {
        var param = StateAdapter.FromFile(item);

        var result = await _mediator.Send(param, cancellationToken);

        if (!result.IsSuccess)
        {
            var errors = result.ValidationErrors.Select(c => new
            {
                error = $"{c.Identifier} - {c.ErrorMessage}"
            });

            _logger.LogWarning("Occurred an error try import State from File: {state} from requisition: {requisitionId} - Error: {error}", item.ToString(), item.Id, string.Join(',', errors.Select(c => c.error)));
            return false;
        }

        _logger.LogInformation("State: {state} from requisition: {requisitionId} was created with success.", item.ToString(), item.Id);
        return true;
    }

    public async Task<Result<PagedResponseDto<StateResponseDto?>>> Get(StateQueryParamsDto param, CancellationToken cancellationToken)
    {
        Expression<Func<State, bool>> expression = c =>
                (string.IsNullOrWhiteSpace(param.Name) || c.Name.Contains(param.Name)) &&
                (string.IsNullOrWhiteSpace(param.Acronym) || c.Acronym.Contains(param.Acronym)) &&
                (param.Id == null || c.Id == param.Id) &&
                (param.Code == null || c.Code == param.Code);

        var paginated = await _stateRepository.Get(expression, param.Page, param.Size, cancellationToken);

        var parse = paginated.Select(StateAdapter.FromDomain);

        var result = new PagedResponseDto<StateResponseDto?>(parse, paginated.CurrentPage, paginated.TotalPages, paginated.PageSize, paginated.TotalCount);

        return Result.Success(result);
    }

    public async Task<Result<Guid?>> GetIdByCode(int code, CancellationToken cancellationToken)
    {
        var result = await _stateRepository.GetIdByCode(code, cancellationToken);

        if (result is null)
            return Result.NotFound();

        return Result.Success(result);
    }


    public async Task<Result<StateResponseDto?>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _stateRepository.GetById(id, cancellationToken);

        if (result is null)
            return Result.NotFound();

        return Result.Success(StateAdapter.FromDomain(result));
    }

    public async Task<Result<Guid>> Create(CreateStateCommand request, CancellationToken cancellationToken) =>
        await _mediator.Send(request, cancellationToken);

    public async Task<Result> Update(UpdateStateCommand request, CancellationToken cancellationToken) =>
        await _mediator.Send(request, cancellationToken);

    public async Task<Result> Remove(RemoveStateCommand request, CancellationToken cancellationToken) =>
        await _mediator.Send(request, cancellationToken);
}
