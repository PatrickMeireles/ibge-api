using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibge.Test.Domain.Entity;

[TestClass]
public class UserTests
{
    private readonly Fixture _fixture = new();

    [TestMethod]
    public void Should_Create_User()
    {
        var fake = _fixture.Create<Model.User>();

        var state = new Model.User(fake.Name, fake.Email, fake.Password, fake.IsAdmin);

        Assert.IsNotNull(state);
        Assert.AreEqual(fake.Email, state.Email);
        Assert.AreEqual(fake.Name, state.Name);
        Assert.AreEqual(fake.Password, state.Password);
        Assert.AreEqual(fake.IsAdmin, state.IsAdmin);
        Assert.AreNotEqual(Guid.Empty, state.Id);
        Assert.IsNotNull(state.CreatedAt);
        Assert.IsNull(state.UpdatedAt);
    }
}
