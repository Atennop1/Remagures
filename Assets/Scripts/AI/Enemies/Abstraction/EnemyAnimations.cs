using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimations : MonoBehaviour
{
    public Animator Animator { get; private set; }

    public void Awake()
    {
        Animator = GetComponent<Animator>();
    }
    
    public void SetAnimFloat(Vector2 setVector, Animator anim)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    public void ChangeAnim(Vector2 direction, Animator anim)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
                SetAnimFloat(Vector2.right, anim);

            else if (direction.x < 0)
                SetAnimFloat(Vector2.left, anim);
        }
        else
        {
            if (direction.y > 0)
                SetAnimFloat(Vector2.up, anim);

            else if (direction.y < 0)
                SetAnimFloat(Vector2.down, anim);
        }
    }
}
