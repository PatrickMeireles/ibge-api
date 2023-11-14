namespace Ibge.Domain.DTO.City;

public class CityResponseDto : BaseResponseDto
{
    public int Code { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid StateId { get; set; }
}
