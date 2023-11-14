using System.Threading.Channels;

namespace Ibge.Infrastructure.Worker;

public class ChannelService<T> : IChannelService<T>
{
    private readonly Channel<T> _channel;

    public ChannelService()
    {
        var options = new UnboundedChannelOptions()
        {
            SingleReader = false
        };

        _channel = Channel.CreateUnbounded<T>(options);
    }

    public ChannelReader<T> GetReader()
    {
        return _channel.Reader;
    }

    public ChannelWriter<T> GetWriter()
    {
        return _channel.Writer;
    }
}
