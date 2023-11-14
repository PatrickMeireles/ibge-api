using Ibge.Infrastructure.Worker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibge.Test.Infrastructure.Worker;

[TestClass]
public class ChannelServiceTest
{
    [TestMethod]
    public void Should_Create_Channel_Return_Success()
    {
        var channel = new ChannelService<int>();

        Assert.IsNotNull(channel);
    }

    [TestMethod]
    public void Should_GetReader_Channel_Return_Success()
    {
        var channel = new ChannelService<int>();

        Assert.IsNotNull(channel.GetReader());
    }

    [TestMethod]
    public void Should_GetWriter_Channel_Return_Success()
    {
        var channel = new ChannelService<int>();

        Assert.IsNotNull(channel.GetWriter());
    }

}
