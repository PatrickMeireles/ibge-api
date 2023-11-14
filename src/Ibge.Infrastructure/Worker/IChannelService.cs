using System.Threading.Channels;

namespace Ibge.Infrastructure.Worker;

public interface IChannelService<T>
{
    ChannelWriter<T> GetWriter();
    ChannelReader<T> GetReader();
}
