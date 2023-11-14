namespace Ibge.Domain.DTO;

public abstract class BaseFromFile
{
    public Guid Id { get; set; }

    public int CountTry { get; private set; }

    protected BaseFromFile(Guid id)
    {
        Id = id;
    }

    public void AddTry()
    {
        CountTry++;
    }
}
