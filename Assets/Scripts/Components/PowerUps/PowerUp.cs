using Remagures.SO.Other;
using UnityEngine;

namespace Remagures.Components.PowerUps
{
    public class PowerUp : MonoBehaviour
    {
        [field: SerializeField] public Signal PowerUpSignal { get; private set; }
    }
}
