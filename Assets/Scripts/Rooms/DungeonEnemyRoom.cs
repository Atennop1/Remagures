using UnityEngine;

public class DungeonEnemyRoom : DungeonRoom
{
    [SerializeField] private Door[] _doors;
    
    public void CheckEnemies()
    {
        foreach (Enemy enemy in Enemies)
        {
            if (enemy.gameObject.activeInHierarchy)
                return;
        }
        
        OpenDoors();
    }

    public void CloseDoors()
    {
        foreach(Door door in _doors)
            door.CloseDoor();
    }
    
    public void OpenDoors()
    {
        foreach (Door door in _doors)
            door.Interact();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player) && !other.isTrigger)
        {
            for (int i = 0; i < Enemies.Length; i++)
                ChangeActivation(Enemies[i], true);
            for (int i = 0; i < Pots.Length; i++)
                ChangeActivation(Pots[i], true);
            CloseDoors();
        }
    }
}
