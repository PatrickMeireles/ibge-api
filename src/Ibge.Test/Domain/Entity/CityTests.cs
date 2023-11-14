using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibge.Test.Domain.Entity;

[TestClass]
public class CityTests
{
    private readonly Fixture _fixture = new();

    [TestMethod]
    public void Should_Create_City()
    {
        var fake = _fixture.Create<Model.City>();

        var city = new Model.City(fake.Code, fake.Name, fake.StateId);

        Assert.IsNotNull(city);
        Assert.AreEqual(fake.Code, city.Code);
        Assert.AreEqual(fake.Name, city.Name);
        Assert.AreEqual(fake.StateId, city.StateId);
        Assert.AreNotEqual(Guid.Empty, city.Id);
        Assert.IsNotNull(city.CreatedAt);
        Assert.IsNull(city.UpdatedAt);
    }

    [TestMethod]
    public void Should_Create_City_With_State()
    {
        var fake = _fixture.Create<Model.City>();
        var fakeState = _fixture.Create<Model.State>();

        var city = new Model.City(fake.Code, fake.Name, fakeState);

        Assert.IsNotNull(city);
        Assert.AreEqual(fake.Code, city.Code);
        Assert.AreEqual(fake.Name, city.Name);
        Assert.AreEqual(fakeState.Id, city.StateId);
        Assert.AreNotEqual(Guid.Empty, city.Id);
        Assert.IsNotNull(city.CreatedAt);
        Assert.IsNull(city.UpdatedAt);
    }

    [TestMethod]
    public void Should_Update_City_With_Id()
    {
        var fake = _fixture.Create<Model.City>();

        var city = new Model.City(fake.Id, fake.Code, fake.Name, fake.StateId);

        Assert.IsNotNull(city);
        Assert.AreEqual(fake.Id, city.Id);
        Assert.AreEqual(fake.Code, city.Code);
        Assert.AreEqual(fake.Name, city.Name);
        Assert.AreEqual(fake.StateId, city.StateId);
        Assert.AreNotEqual(Guid.Empty, city.Id);
        Assert.IsNotNull(city.CreatedAt);
        Assert.IsNull(city.UpdatedAt);
    }
}
