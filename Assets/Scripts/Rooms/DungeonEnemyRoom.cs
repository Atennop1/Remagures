using UnityEngine;

public class DungeonEnemyRoom : DungeonRoom
{
    public Door[] _doors;
    
    public void CheckEnemies()
    {
        foreach (Enemy enemy in _enemies)
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
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            for (int i = 0; i < _enemies.Length; i++)
                ChangeActivation(_enemies[i], true);
            for (int i = 0; i < _pots.Length; i++)
                ChangeActivation(_pots[i], true);
            CloseDoors();
        }
    }
}
