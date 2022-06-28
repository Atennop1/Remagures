using UnityEngine;

public class Destroyable : MonoBehaviour
{
    [SerializeField] private GameObject _sharp;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Smash()
    {
        _animator.SetBool("smash", true);
        Instantiate(_sharp, transform.position, Quaternion.identity);
    }
}
