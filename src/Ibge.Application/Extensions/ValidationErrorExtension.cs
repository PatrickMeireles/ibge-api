using Ardalis.Result;

namespace Ibge.Application.Extensions;

public static class ValidationErrorExtension
{
    public static List<ValidationError> AddError(string identifier, string message) =>
        new List<ValidationError> {
                    new ValidationError
                    {
                        Identifier = identifier,
                        ErrorMessage = message }
                    };

}
