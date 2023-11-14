using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibge.Test.Domain.Entity;

[TestClass]
public class StateTests
{
    private readonly Fixture _fixture = new();

    [TestMethod]
    public void Should_Create_State()
    {
        var fake = _fixture.Create<Model.State>();

        var state = new Model.State(fake.Code, fake.Name, fake.Acronym);

        Assert.IsNotNull(state);
        Assert.AreEqual(fake.Code, state.Code);
        Assert.AreEqual(fake.Name, state.Name);
        Assert.AreEqual(fake.Acronym, state.Acronym);
        Assert.AreNotEqual(Guid.Empty, state.Id);
        Assert.IsNotNull(state.CreatedAt);
        Assert.IsNull(state.UpdatedAt);
    }

    [TestMethod]
    public void Should_Update_State_With_Id()
    {
        var fake = _fixture.Create<Model.State>();

        var state = new Model.State(fake.Id, fake.Code, fake.Name, fake.Acronym);

        Assert.IsNotNull(state);
        Assert.AreEqual(fake.Id, state.Id);
        Assert.AreEqual(fake.Code, state.Code);
        Assert.AreEqual(fake.Name, state.Name);
        Assert.AreEqual(fake.Acronym, state.Acronym);
        Assert.AreNotEqual(Guid.Empty, state.Id);
        Assert.IsNotNull(state.CreatedAt);
        Assert.IsNull(state.UpdatedAt);
    }

    [TestMethod]
    public void Should_Set_CreatedAt()
    {
        var state = _fixture.Create<Model.State>();

        state.SetCreatedAt(DateTime.Now);

        Assert.IsNotNull(state);
        Assert.IsNotNull(state.CreatedAt);
    }

    [TestMethod]
    public void Should_Set_UpdatedAt()
    {
        var state = _fixture.Create<Model.State>();

        state.SetUpdatedAt(DateTime.Now);

        Assert.IsNotNull(state);
        Assert.IsNotNull(state.UpdatedAt);
    }
}
