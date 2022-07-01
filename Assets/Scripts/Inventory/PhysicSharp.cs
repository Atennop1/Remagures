using UnityEngine;

public class PhysicSharp : MonoBehaviour
{
    [SerializeField] private FloatValue _sharps;
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController player) && !other.isTrigger)
        {
            _sharps.Value++;
            Destroy(gameObject);
        }
    }
}
