using Ibge.Domain.DTO.City;
using Ibge.Domain.DTO.State;
using Ibge.Domain.Services;
using Ibge.Infrastructure.Worker;
using Microsoft.AspNetCore.Http;
using NPOI.XSSF.UserModel;

namespace Ibge.Application.Services;

public class ImportServices : IImportServices
{
    private readonly IChannelService<StateFromFileDto> _channelState;
    private readonly IChannelService<CityFromFileDto> _channelCity;

    public ImportServices(IChannelService<StateFromFileDto> channelState, IChannelService<CityFromFileDto> channelCity)
    {
        _channelState = channelState;
        _channelCity = channelCity;
    }

    public async Task ProccessFile(Guid id, IFormFile file, CancellationToken cancellationToken)
    {
        using var fs = file.OpenReadStream();
        using var wb = new XSSFWorkbook(fs);
        var sheets = wb.NumberOfSheets;

        if (sheets <= 0)
            return;

        var sheet = (XSSFSheet)wb.GetSheetAt(0);
        var citiesSheet = (XSSFSheet)wb.GetSheetAt(1);

        GetStateRows(id, sheet, cancellationToken).GetAwaiter();

        await GetCitiesRows(id, citiesSheet, cancellationToken);
    }

    public async Task GetStateRows(Guid id, XSSFSheet sheet, CancellationToken cancellationToken)
    {
        for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
        {
            var row = sheet.GetRow(rowIndex);

            if (row != null)
            {
                var code = (int)(row.GetCell(0)?.NumericCellValue ?? 0);
                var acronym = row.GetCell(1)?.StringCellValue ?? string.Empty;
                var name = row.GetCell(2)?.StringCellValue ?? string.Empty;

                var data = new StateFromFileDto(id, code, name, acronym);

                await _channelState.GetWriter().WriteAsync(data, cancellationToken);
            }
        }
    }

    public async Task GetCitiesRows(Guid id, XSSFSheet sheet, CancellationToken cancellationToken)
    {
        for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
        {
            var row = sheet.GetRow(rowIndex);

            if (row != null)
            {
                var code = (int)(row.GetCell(0)?.NumericCellValue ?? 0);
                var name = row.GetCell(1)?.StringCellValue ?? string.Empty;
                var stateCode = (int)(row.GetCell(2)?.NumericCellValue ?? 0);

                var data = new CityFromFileDto(id, code, name, stateCode);

                await _channelCity.GetWriter().WriteAsync(data, cancellationToken);
            }
        }
    }
}
