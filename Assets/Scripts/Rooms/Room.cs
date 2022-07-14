using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] protected Enemy[] Enemies { get; private set; }
    [SerializeField] protected Destroyable[] Pots { get; private set; }
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player) && !other.isTrigger)
        {
            for (int i = 0; i < Enemies.Length; i++)
                ChangeActivation(Enemies[i], true);

            for (int i = 0; i < Pots.Length; i++)
                ChangeActivation(Pots[i], true);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player) && !other.isTrigger)
        {
            for (int i = 0; i < Enemies.Length; i++)
                ChangeActivation(Enemies[i], false);

            for (int i = 0; i < Pots.Length; i++)
                ChangeActivation(Pots[i], false);
        }
    }
    
    public void ChangeActivation(Component component, bool activation)
    {
        component.gameObject.SetActive(activation);
    }
}
