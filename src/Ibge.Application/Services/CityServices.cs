using Ardalis.Result;
using Ibge.Application.Adapter;
using Ibge.Domain.Command.City;
using Ibge.Domain.DTO;
using Ibge.Domain.DTO.City;
using Ibge.Domain.Entity;
using Ibge.Domain.Repository;
using Ibge.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Ibge.Application.Services;

public class CityServices : ICityServices
{
    private readonly ICityRepository _cityRepository;
    private readonly IStateServices _stateServices;
    private readonly ILogger<CityServices> _logger;
    private readonly IMediator _mediator;
    public CityServices(ICityRepository cityRepository, IStateServices stateServices, ILogger<CityServices> logger, IMediator mediator)
    {
        _cityRepository = cityRepository;
        _stateServices = stateServices;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<Result<CityResponseDto?>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.GetById(id, cancellationToken);

        if (city == null)
            return Result.NotFound();

        return Result.Success(CityAdapter.FromDomain(city));
    }

    public async Task<Result<PagedResponseDto<CityResponseDto?>>> Get(CityQueryParamsDto param, CancellationToken cancellationToken)
    {
        Expression<Func<City, bool>> expression = c =>
            (param.Id == null || c.Id == param.Id) &&
            (param.Code == null || c.Code == param.Code) &&
            (string.IsNullOrWhiteSpace(param.Name) || c.Name.Contains(param.Name)) &&
            (param.StateId == null || c.StateId == param.StateId);

        var paginated = await _cityRepository.Get(expression, param.Page, param.Size, cancellationToken);

        var parse = paginated.Select(CityAdapter.FromDomain);

        var result = new PagedResponseDto<CityResponseDto?>(parse, paginated.CurrentPage, paginated.TotalPages, paginated.PageSize, paginated.TotalCount);

        return result;
    }

    public async Task<bool> AddFromFile(CityFromFileDto item, CancellationToken cancellationToken)
    {
        var state = await _stateServices.GetIdByCode(item.StateCode, cancellationToken);

        if (!state.IsSuccess || state.Value == null)
        {
            var error = $"Cannot Find State with Code: {item.StateCode}";
            _logger.LogWarning("Occurred an error try import City from File: {state} from requisition: {requisitionId} - Error: {error}", item.ToString(), item.Id, error);

            return false;
        }

        var data = CityAdapter.FromFile(item, state.Value.Value);

        var result = await _mediator.Send(data, cancellationToken);

        if (!result.IsSuccess)
        {
            var errors = result.ValidationErrors.Select(c => new
            {
                error = $"{c.Identifier} - {c.ErrorMessage}"
            });

            _logger.LogWarning("Occurred an error try import City from File: {state} from requisition: {requisitionId} - Error: {error}", item.ToString(), item.Id, string.Join(',', errors.Select(c => c.error)));
            return false;
        }

        _logger.LogInformation("City: {city} from requisition: {requisitionId} was created with success.", item.ToString(), item.Id);
        return true;
    }

    public async Task<Result<Guid>> Create(CreateCityCommand request, CancellationToken cancellationToken) =>
        await _mediator.Send(request, cancellationToken);

    public async Task<Result> Update(UpdateCityCommand request, CancellationToken cancellationToken) =>
        await _mediator.Send(request, cancellationToken);

    public async Task<Result> Remove(RemoveCityCommand request, CancellationToken cancellationToken) =>
        await _mediator.Send(request, cancellationToken);
}
