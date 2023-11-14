namespace Ibge.Domain.Command.State;

public abstract class StateCommand
{
    public int Code { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Acronym { get; set; } = string.Empty;
}
