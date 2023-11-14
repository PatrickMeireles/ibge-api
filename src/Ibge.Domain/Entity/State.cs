namespace Ibge.Domain.Entity;

public class State : BaseEntity
{
    public int Code { get; private set; }
    public string Name { get; private set; }
    public string Acronym { get; private set; }
    public ICollection<City> Cities { get; private set; } = new List<City>();

    public State(int code, string name, string acronym)
    {
        Code = code;
        Name = name;
        Acronym = acronym;
    }

    public State(Guid id, int code, string name, string acronym) : base(id)
    {
        Code = code;
        Name = name;
        Acronym = acronym;
    }

    public void Update(int code, string name, string acronym)
    {
        Code = code;
        Name = name;
        Acronym = acronym;
    }

    public void AddCity(City city)
    {
        Cities.Add(city);
    }
}
