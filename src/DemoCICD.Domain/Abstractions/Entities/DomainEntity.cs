namespace DemoCICD.Domain.Abstractions.Entities;
public abstract class DomainEntity<TKey>
{
    public virtual TKey Id { get; set; }

    /// <summary>
    /// True if domain entity has an Identity
    /// </summary>
    /// <returns></returns>
    public bool IsTransient() => Id.Equals(default(TKey));
}
