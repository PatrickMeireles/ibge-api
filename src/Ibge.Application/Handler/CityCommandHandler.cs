using Ardalis.Result;
using Ibge.Application.Adapter;
using Ibge.Application.Extensions;
using Ibge.Application.Validators.City;
using Ibge.Domain.Command.City;
using Ibge.Domain.Entity;
using Ibge.Domain.Repository;
using Ibge.Infrastructure.Data.Context;
using MediatR;
using System.Linq.Expressions;

namespace Ibge.Application.Handler;

public class CityCommandHandler : CommandHandler,
    IRequestHandler<CreateCityCommand, Result<Guid>>,
    IRequestHandler<UpdateCityCommand, Result>,
    IRequestHandler<RemoveCityCommand, Result>
{
    private readonly ICityRepository _cityRepository;
    private readonly IStateRepository _stateRepository;

    public CityCommandHandler(
        DatabaseContext context,
        ICityRepository cityRepository,
        IStateRepository stateRepository) : base(context)
    {
        _cityRepository = cityRepository;
        _stateRepository = stateRepository;
    }

    public async Task<Result<Guid>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        var errors = await FluentValidationExtension<CreateCityValidator, CreateCityCommand>.GetValidationErrors(request, cancellationToken);

        if (errors.Any())
            return Result.Invalid(errors);

        var state = await _stateRepository.GetById(request.StateId, cancellationToken);

        if (state == null)
            return Result.Invalid(ValidationErrorExtension.AddError("Business Error", "State Not Found"));

        var existCity = (await _cityRepository.GetAll(cancellationToken: cancellationToken)).Where(c => c.Code == request.Code);

        if (existCity.Any())
            return Result.Invalid(ValidationErrorExtension.AddError("Conflict", "Already exist State with this same parameters"));

        var city = CityAdapter.Create(request);

        await _cityRepository.Add(city, cancellationToken);

        await CommitAsync(cancellationToken);
        return Result.Success(city.Id);
    }

    public async Task<Result> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
    {
        var errors = await FluentValidationExtension<UpdateCityValidator, UpdateCityCommand>.GetValidationErrors(request, cancellationToken);

        if (errors.Any())
            return Result.Invalid(errors);

        var state = await _stateRepository.GetById(request.StateId, cancellationToken);

        if (state == null)
            return Result.Invalid(ValidationErrorExtension.AddError("Business Error", "State Not Found"));

        var exist = await _cityRepository.GetById(request.Id, cancellationToken);

        if (exist == null)
            return Result.NotFound();

        Expression<Func<City, bool>> expression = c => (c.Code == request.Code && c.StateId == request.StateId) && c.Id != request.Id;

        var conflict = (await _cityRepository.GetAll(cancellationToken: cancellationToken)).Where(expression);

        var has = conflict.Any();

        if (conflict.Any())
            return Result.Invalid(ValidationErrorExtension.AddError("Conflict", "Already exist State with this same parameters"));

        exist.Update(request.Code, request.Name, request.StateId);

        await _cityRepository.Update(exist);

        await CommitAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> Handle(RemoveCityCommand request, CancellationToken cancellationToken)
    {
        var errors = await FluentValidationExtension<RemoveCityValidator, RemoveCityCommand>.GetValidationErrors(request, cancellationToken);

        if (errors.Any())
            return Result.Invalid(errors);

        var exist = await _cityRepository.GetById(request.Id, cancellationToken);

        if (exist == null)
            return Result.NotFound();

        await _cityRepository.Remove(exist, cancellationToken);

        await CommitAsync(cancellationToken);

        return Result.Success();
    }
}
