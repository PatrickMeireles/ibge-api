namespace Ibge.Domain.DTO;

public class PagedResponseDto<T>
{
    public IEnumerable<T> Data { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public PagedResponseDto(IEnumerable<T> data, int currentPage, int totalPages, int pageSize, int totalCount)
    {
        Data = data;
        CurrentPage = currentPage;
        TotalPages = totalPages;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
}
