using System;
using UnityEngine;

namespace Remagures.SO
{
    public partial class Quest
    {
        [Serializable]
        public struct QuestData
        {
            [field: SerializeField] public string Name { get; private set; }
            [field: SerializeField] public string Description { get; private set; }
            [field: SerializeField] public Sprite QuestSprite { get; private set; }
        }
    }
}