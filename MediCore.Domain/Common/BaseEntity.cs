namespace MediCore.Domain.Common;

    public abstract class BaseEntity : IEntity
{
    public int Id { get; protected set; }

    public Guid PublicId { get; private set; } = Guid.NewGuid();
}

