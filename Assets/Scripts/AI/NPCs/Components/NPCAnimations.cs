using UnityEngine;

namespace Remagures.AI
{
    public class NPCAnimations : MonoBehaviour
    {
        public Animator Animator { get; private set; }
    
        private readonly int MOVE_X_ANIMATOR_NAME = Animator.StringToHash("moveX");
        private readonly int MOVE_Y_ANIMATOR_NAME = Animator.StringToHash("moveY");

        public void Awake() => Animator = GetComponent<Animator>();

        public void UpdateAnim(Vector2 vector)
        {
            Animator.SetFloat(MOVE_X_ANIMATOR_NAME, vector.x);
            Animator.SetFloat(MOVE_Y_ANIMATOR_NAME, vector.y);
        }
    }
}
