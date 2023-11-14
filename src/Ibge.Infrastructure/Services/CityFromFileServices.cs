using Ibge.Domain.DTO.City;
using Ibge.Domain.Services;
using Ibge.Infrastructure.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ibge.Infrastructure.Services;

public class CityFromFileServices : BackgroundService
{
    private readonly IChannelService<CityFromFileDto> _channelService;
    private readonly IServiceScopeFactory _scopeFactory;

    public CityFromFileServices(IChannelService<CityFromFileDto> channelService, IServiceScopeFactory scopeFactory)
    {
        _channelService = channelService;
        _scopeFactory = scopeFactory;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {

            var reader = await _channelService.GetReader().ReadAsync(stoppingToken);

            using var scope = _scopeFactory.CreateScope();
            var service = scope.ServiceProvider.GetService<ICityServices>();

            if (service is null)
                return;

            try
            {
                var result = await service.AddFromFile(reader, stoppingToken);
                await Retry(reader, result, stoppingToken);
            }
            catch
            {
                await Retry(reader, false, stoppingToken);
            }
        }
    }

    public async Task Retry(CityFromFileDto reader, bool result, CancellationToken stoppingToken)
    {
        if (!result && reader.CountTry <= 3)
        {
            reader.AddTry();
            await _channelService.GetWriter().WriteAsync(reader, stoppingToken);
        }

    }
}
