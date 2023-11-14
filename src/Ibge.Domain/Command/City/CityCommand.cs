namespace Ibge.Domain.Command.City;

public class CityCommand
{
    public int Code { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid StateId { get; set; }
}
