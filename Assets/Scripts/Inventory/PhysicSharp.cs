using UnityEngine;

public class PhysicSharp : MonoBehaviour
{
    [SerializeField] private FloatValue _sharps;
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            _sharps.Value++;
            Destroy(gameObject);
        }
    }
}
