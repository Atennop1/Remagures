using UnityEngine;
using UnityEngine.Events;

public interface IMagicItem : IBaseItemComponent
{
    public GameObject Projectile { get; }
    public UnityEvent ThisEvent { get; }
}
