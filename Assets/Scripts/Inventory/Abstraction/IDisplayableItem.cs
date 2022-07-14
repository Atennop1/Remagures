using UnityEngine;

public interface IDisplayableItem : IBaseItemComponent
{
    public AnimatorOverrideController OverrideController { get; }
}
