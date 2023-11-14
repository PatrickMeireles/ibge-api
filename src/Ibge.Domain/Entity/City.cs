namespace Ibge.Domain.Entity;

public class City : BaseEntity
{
    public int Code { get; private set; }
    public string Name { get; private set; }
    public Guid StateId { get; private set; }
    public State? State { get; private set; }

    public City(int code, string name, State state)
    {
        Code = code;
        Name = name;
        StateId = state.Id;
        State = state;
    }

    public City(int code, string name, Guid stateId)
    {
        Code = code;
        Name = name;
        StateId = stateId;
    }
    public City(Guid id, int code, string name, Guid stateId) : base(id)
    {
        Code = code;
        Name = name;
        StateId = stateId;
    }

    public void Update(int code, string name, Guid stateId)
    {
        Code = code;
        Name = name;
        StateId = stateId;
    }
}
