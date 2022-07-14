using UnityEngine.Events;

public interface IUsableItem : IBaseItemComponent
{
    public UnityEvent ThisEvent { get; }
    public void Use();
}
