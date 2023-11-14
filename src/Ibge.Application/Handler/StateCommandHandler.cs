using Ardalis.Result;
using Ibge.Application.Adapter;
using Ibge.Application.Extensions;
using Ibge.Application.Validators.State;
using Ibge.Domain.Command.State;
using Ibge.Domain.Entity;
using Ibge.Domain.Repository;
using Ibge.Infrastructure.Data.Context;
using MediatR;
using System.Linq.Expressions;

namespace Ibge.Application.Handler;

public class StateCommandHandler : CommandHandler,
    IRequestHandler<CreateStateCommand, Result<Guid>>,
    IRequestHandler<UpdateStateCommand, Result>,
    IRequestHandler<RemoveStateCommand, Result>
{
    private readonly IStateRepository _repository;

    public StateCommandHandler(DatabaseContext context, IStateRepository repository) : base(context)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(CreateStateCommand request, CancellationToken cancellationToken)
    {
        var errors = await FluentValidationExtension<CreateStateValidator, CreateStateCommand>.GetValidationErrors(request, cancellationToken);

        if (errors.Any())
            return Result.Invalid(errors);

        Expression<Func<State, bool>> expression = c => c.Code == request.Code || c.Acronym.ToLower() == request.Acronym.ToLower();

        var query = (await _repository.GetAll(cancellationToken: cancellationToken)).Where(expression);

        if (query.Any())
            return Result.Invalid(ValidationErrorExtension.AddError("Conflict", "Already exist State with this same parameters"));

        var state = StateAdapter.Create(request);

        await _repository.Add(state, cancellationToken);

        await CommitAsync(cancellationToken);

        return Result.Success(state.Id);
    }

    public async Task<Result> Handle(UpdateStateCommand request, CancellationToken cancellationToken)
    {
        var errors = await FluentValidationExtension<UpdateStateValidator, UpdateStateCommand>.GetValidationErrors(request, cancellationToken);

        if (errors.Any())
            return Result.Invalid(errors);

        var exist = await _repository.GetById(request.Id, cancellationToken);

        if (exist == null)
            return Result.NotFound();

        Expression<Func<State, bool>> expression = c => (c.Code == request.Code || c.Acronym.ToLower() == request.Acronym.ToLower()) && c.Id != request.Id;

        var conflict = (await _repository.GetAll(cancellationToken: cancellationToken)).Where(expression);

        if (conflict.Any()) 
            return Result.Invalid(ValidationErrorExtension.AddError("Conflict", "Already exist State with this same parameters"));

        exist.Update(request.Code, request.Name, request.Acronym);

        await _repository.Update(exist);

        await CommitAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result> Handle(RemoveStateCommand request, CancellationToken cancellationToken)
    {
        var errors = await FluentValidationExtension<RemoveStateValidator, RemoveStateCommand>.GetValidationErrors(request, cancellationToken);

        if (errors.Any())
            return Result.Invalid(errors);

        var exist = await _repository.GetByIdWithCities(request.Id, cancellationToken);

        if (exist == null)
            return Result.NotFound();

        if (exist.Cities.Any())
            return Result.Invalid(ValidationErrorExtension.AddError("Business Error", "Cannot delete this register because there are Cities with this State"));

        await _repository.Remove(exist, cancellationToken);

        await CommitAsync(cancellationToken);

        return Result.Success();
    }
}
