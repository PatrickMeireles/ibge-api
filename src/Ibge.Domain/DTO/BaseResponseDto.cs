namespace Ibge.Domain.DTO;

public abstract class BaseResponseDto
{
    public Guid Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}
