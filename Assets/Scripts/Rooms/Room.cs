using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] protected Enemy[] _enemies;
    [SerializeField] protected Destroyable[] _pots;
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            for (int i = 0; i < _enemies.Length; i++)
                ChangeActivation(_enemies[i], true);

            for (int i = 0; i < _pots.Length; i++)
                ChangeActivation(_pots[i], true);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            for (int i = 0; i < _enemies.Length; i++)
                ChangeActivation(_enemies[i], false);

            for (int i = 0; i < _pots.Length; i++)
                ChangeActivation(_pots[i], false);
        }
    }
    
    public void ChangeActivation(Component component, bool activation)
    {
        component.gameObject.SetActive(activation);
    }
}
