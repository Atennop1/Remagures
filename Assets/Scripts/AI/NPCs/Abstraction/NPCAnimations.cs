using UnityEngine;

public class NPCAnimations : MonoBehaviour
{
    public Animator animator { get; private set; }

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateAnim(Vector2 vector)
    {
        animator.SetFloat("moveX", vector.x);
        animator.SetFloat("moveY", vector.y);
    }
}
