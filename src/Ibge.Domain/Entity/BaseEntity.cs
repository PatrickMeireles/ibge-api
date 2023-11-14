namespace Ibge.Domain.Entity;

public abstract class BaseEntity
{
    public Guid Id { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public DateTimeOffset? UpdatedAt { get; private set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }

    protected BaseEntity(Guid id)
    {
        Id = id;
    }

    public void SetCreatedAt(DateTimeOffset createdAt) =>
        this.CreatedAt = createdAt;

    public void SetUpdatedAt(DateTimeOffset updatedAt) =>
        this.UpdatedAt = updatedAt;
}
