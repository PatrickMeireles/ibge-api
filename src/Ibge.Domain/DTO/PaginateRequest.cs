namespace Ibge.Domain.DTO;

public class PaginateRequest
{
    const int _minPage = 1;
    const int _maxSize = 50;

    public int Page { get; set; }

    public int Size { get; set; }

    public PaginateRequest(int page, int size)
    {
        Page = page;
        Size = size;

        if (Page < _minPage)
            Page = _minPage;

        if (Size > _maxSize)
            Size = _maxSize;
    }
}
