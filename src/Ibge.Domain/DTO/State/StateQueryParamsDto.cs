namespace Ibge.Domain.DTO.State;

public class StateQueryParamsDto : PaginateRequest
{
    public Guid? Id { get; set; }
    public int? Code { get; set; }
    public string? Name { get; set; }
    public string? Acronym { get; set; }


    public StateQueryParamsDto(Guid? id, int? code, string? name, string? acronym, int page = 1, int size = 20): base(page, size)
    {
        Id = id;
        Code = code;
        Name = name;
        Acronym = acronym;
        Page = page;
        Size = size;
    }
}