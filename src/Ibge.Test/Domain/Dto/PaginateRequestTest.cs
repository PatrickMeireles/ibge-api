using Ibge.Domain.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibge.Test.Domain.Dto;

    [TestClass]
public class PaginateRequestTest
{
    [TestMethod]
    [DataRow(1, 20)]
    [DataRow(1, 200)]
    [DataRow(-1, 20)]
    [DataRow(-1, 300)]
    public void Should_Create_Instance(int page, int size)
    {
        var result = new PaginateRequest(page, size);

        Assert.IsNotNull(result);
    }
}
