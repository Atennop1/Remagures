using System.Collections.Generic;

public interface IChoiceableItem : IBaseItemComponent
{
    public bool IsCurrent { get; }
    public void SetIsCurrent(IEnumerable<IReadOnlyCell> inventory);
    public void DisableIsCurrent();
}
