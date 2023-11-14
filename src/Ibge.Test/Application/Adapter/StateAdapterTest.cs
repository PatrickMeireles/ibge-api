using Ibge.Application.Adapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibge.Test.Application.Adapter;

[TestClass]
public class StateAdapterTest
{
    [TestMethod]
    public void Should_FromDomain_Return_Null()
    {
        var result = StateAdapter.FromDomain(null);

        Assert.IsNull(result);
    }
}
