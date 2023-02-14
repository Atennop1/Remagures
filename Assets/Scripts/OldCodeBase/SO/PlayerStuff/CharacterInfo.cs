using Remagures.Model;
using Remagures.Model.InventorySystem;
using Remagures.RuneSystem;
using UnityEngine;

namespace Remagures.SO
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New ClassStat", menuName = "Player Stuff/ClassStat")]
    public class CharacterInfo : ScriptableObject
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
            => Unity.Notifications.Android.AndroidNotificationCenter.CancelNotification(1);

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
    
        public void SetupRunes(IRuneItem item, Mana counter)
        {
            FireRunActive = item.RuneType == RuneType.Fire;
            ManaRuneActive = item.RuneType == RuneType.Mana;
            ShieldRuneActive = item.RuneType == RuneType.Shield;

            InitRunes(counter);
        }

        private void InitRunes(Mana counter)
        {
            ShieldRuneCoefficient = ShieldRuneActive ? 1.3f : 1f;
            counter.SetupManaRune(ManaRuneActive);
        }
    }
}
