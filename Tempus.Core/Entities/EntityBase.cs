namespace Tempus.Core.Entities;

/// <summary>
/// Entity that is stored in database. Provides auto-generated ID that is accessable after object creation.
/// </summary>
public class EntityBase
{
    /// <summary>
    /// ID of the entity. Auto-generated.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
}
