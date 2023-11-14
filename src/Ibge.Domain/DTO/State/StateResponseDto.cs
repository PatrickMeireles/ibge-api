namespace Ibge.Domain.DTO.State;

public class StateResponseDto : BaseResponseDto
{
    public int Code { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Acronym { get; set; } = string.Empty;
}
