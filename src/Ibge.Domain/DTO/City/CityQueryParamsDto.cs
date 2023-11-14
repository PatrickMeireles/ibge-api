namespace Ibge.Domain.DTO.City;

public class CityQueryParamsDto : PaginateRequest
{
    public Guid? Id { get; set; }
    public int? Code { get; set; }
    public string? Name { get; set; }
    public Guid? StateId { get; set; }

    public CityQueryParamsDto(Guid? id, int? code, string? name, Guid? stateId, int page = 1, int size = 20) : base(page, size)
    {
        Id = id;
        Code = code;
        Name = name;
        StateId = stateId;
        Page = page;
        Size = size;
    }
}
