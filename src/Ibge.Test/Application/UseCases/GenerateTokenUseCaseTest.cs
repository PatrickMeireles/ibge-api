using Ibge.Application.Configuration;
using Ibge.Application.UseCases;
using Ibge.Domain.Entity;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibge.Test.Application.UseCases;

[TestClass]
public class GenerateTokenUseCaseTest
{
    private readonly Fixture _fixture = new();

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Should_UseCase_Throw_Exception()
    {
        var user = _fixture.Create<User>();
        var fakeJWtOptions = new JwtOptions
        {
            Key = string.Empty
        };
        var mockJwtOptions = Options.Create(fakeJWtOptions);

        GenerateTokenUseCase _useCase = new(mockJwtOptions);

        _useCase.Action(user);
    }

    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void Should_Return_Token(bool isAdmin)
    {
        var user = new User("teste", "teste@teste.com", "jsai0jdsajd", isAdmin);
        var fakeJWtOptions = _fixture.Create<JwtOptions>();

        var mockJwtOptions = Options.Create(fakeJWtOptions);

        GenerateTokenUseCase _useCase = new(mockJwtOptions);

        var result = _useCase.Action(user);

        Assert.IsNotNull(result);
    }
}
