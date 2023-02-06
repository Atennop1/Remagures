using UnityEngine;

namespace Remagures.Interactable
{
    public class Destroyable : MonoBehaviour
    {
        [SerializeField] private GameObject _sharp;
        private Animator _animator;
    
        private readonly int SMASH_ANIMATOR_NAME = Animator.StringToHash("smash");

        private void Start()
            => _animator = GetComponent<Animator>();

        public void Smash()
        {
            _animator.SetBool(SMASH_ANIMATOR_NAME, true);
            Instantiate(_sharp, transform.position, Quaternion.identity);
        }
    }
}
