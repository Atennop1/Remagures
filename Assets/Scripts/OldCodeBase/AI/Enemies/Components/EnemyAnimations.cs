using UnityEngine;

namespace Remagures.AI.Enemies
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimations : MonoBehaviour
    {
        public Animator Animator { get; private set; }

        private readonly int MOVE_X_ANIMATOR_NAME = Animator.StringToHash("moveX");
        private readonly int MOVE_Y_ANIMATOR_NAME = Animator.StringToHash("moveY");

        public void Awake()
        {
            Animator = GetComponent<Animator>();
        }
    
        public void SetAnimFloat(Vector2 setVector, Animator anim)
        {
            anim.SetFloat(MOVE_X_ANIMATOR_NAME, setVector.x);
            anim.SetFloat(MOVE_Y_ANIMATOR_NAME, setVector.y);
        }

        public void ChangeAnim(Vector2 direction, Animator anim)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                SetAnimFloat(direction.x > 0 ? Vector2.right : Vector2.left, anim);
            }
            else
            {
                SetAnimFloat(direction.y > 0 ? Vector2.up : Vector2.down, anim);
            }
        }
    }
}
