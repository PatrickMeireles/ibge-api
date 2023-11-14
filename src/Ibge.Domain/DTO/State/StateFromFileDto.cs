namespace Ibge.Domain.DTO.State;

public class StateFromFileDto : BaseFromFile
{
    public int Code { get; set; }
    public string Name { get; set; }
    public string Acronym { get; set; }

    public StateFromFileDto(Guid id, int code, string name, string acronym) : base(id)
    {
        Code = code;
        Name = name;
        Acronym = acronym;
    }

    public override string ToString()
    {
        var result = $"{Code} - {Acronym} - {Name}";
        return result;
    }
}