using UnityEngine;

public class NPCAnimations : MonoBehaviour
{
    public Animator Animator { get; private set; }

    public void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void UpdateAnim(Vector2 vector)
    {
        Animator.SetFloat("moveX", vector.x);
        Animator.SetFloat("moveY", vector.y);
    }
}
