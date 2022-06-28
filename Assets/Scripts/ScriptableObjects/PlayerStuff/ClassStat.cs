using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New ClassStat", menuName = "Player Stuff/ClassStat")]
public class ClassStat : ScriptableObject
{
    [field: SerializeField] public float ArmorCoefficient { get; private set; } 
    [field: SerializeField] public float ShieldRuneCoefficient { get; private set; } 
    [field: SerializeField] public float SwordDamageCoefficient { get; private set; }  

    [field: SerializeField, Space] public float BowDamageCoefficient { get; private set; }  
    [field: SerializeField] public float BowReloadTime { get; private set; } 

    [field: SerializeField, Space] public float MagicDamageCoefficient { get; private set; }  
    [field: SerializeField] public float MagicCostCoefficient { get; private set; }  

    [field: SerializeField, Space] public bool FireRunActive { get; private set; } 
    [field: SerializeField] public bool ManaRuneActive { get; private set; } 
    [field: SerializeField] public bool ShieldRuneActive { get; private set; } 

    public void DeleteNotification()
    {
        Unity.Notifications.Android.AndroidNotificationCenter.CancelNotification(1);
    }

    public void WarriorSet()
    {
        ArmorCoefficient = 1.5f;
        SwordDamageCoefficient = 1.5f;

        BowReloadTime = 0.5f;
        BowDamageCoefficient = 1;

        MagicDamageCoefficient = 1;
        MagicCostCoefficient = 1;
    }

    public void ArcherSet()
    {
        BowDamageCoefficient = 1.5f;
        BowReloadTime = 0.3f;

        ArmorCoefficient = 1;
        SwordDamageCoefficient = 1;

        MagicDamageCoefficient = 1;
        MagicCostCoefficient = 1;
    }

    public void MagicianSet()
    {
        MagicDamageCoefficient = 1.5f;
        MagicCostCoefficient = 0.7f;

        BowReloadTime = 0.5f;
        ArmorCoefficient = 1;
        
        SwordDamageCoefficient = 1;
        BowDamageCoefficient = 1;
    }

    public void ClearRunes()
    {
        FireRunActive = false;
        ManaRuneActive = false;
        ShieldRuneActive = false;

        ShieldRuneCoefficient = 1;
    }
    
    public void SetupRunes(RuneInventoryItem item, MagicManager manager)
    {
        FireRunActive = item.RuneItemData.RuneType == RuneType.Fire;
        ManaRuneActive = item.RuneItemData.RuneType == RuneType.Mana;
        ShieldRuneActive = item.RuneItemData.RuneType == RuneType.Shield;

        InitRunes(manager);
    }

    private void InitRunes(MagicManager manager)
    {
        if (ShieldRuneActive)
            ShieldRuneCoefficient = 1.3f;
        else
            ShieldRuneCoefficient = 1f;

        if (ManaRuneActive)
            manager.ManaRuneCoroutine = manager.StartCoroutine(manager.MagicRuneCoroutine());
        else if (manager.ManaRuneCoroutine != null)
            manager.StopCoroutine(manager.ManaRuneCoroutine);    
    }
}
