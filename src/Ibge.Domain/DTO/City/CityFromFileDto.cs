namespace Ibge.Domain.DTO.City;

public class CityFromFileDto : BaseFromFile
{
    public int Code { get; set; }
    public string Name { get; set; }
    public int StateCode { get; set; }

    public CityFromFileDto(Guid id, int code, string name, int stateCode) : base(id)
    {
        Code = code;
        Name = name;
        StateCode = stateCode;
    }

    public override string ToString()
    {
        return $"{Code} - {Name} - {StateCode}";
    }
}
