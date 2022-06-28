using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [field: SerializeField] public Animator PlayerAnimator { get; private set; }
    [field: SerializeField] public Animator HelmetAnimator { get; private set; }
    [field: SerializeField] public Animator ChestplateAnimator { get; private set; }
    [field: SerializeField] public Animator LegginsAnimator { get; private set; }

    public void ChangeAnim(string key, bool value) 
    {
        if (PlayerAnimator.runtimeAnimatorController && PlayerAnimator.gameObject.activeInHierarchy)
            PlayerAnimator.SetBool(key, value);

        if (ChestplateAnimator.runtimeAnimatorController && ChestplateAnimator.gameObject.activeInHierarchy)
            ChestplateAnimator.SetBool(key, value);

        if (HelmetAnimator.runtimeAnimatorController && HelmetAnimator.gameObject.activeInHierarchy)
            HelmetAnimator.SetBool(key, value);

        if (LegginsAnimator.runtimeAnimatorController && LegginsAnimator.gameObject.activeInHierarchy)
            LegginsAnimator.SetBool(key, value);
    }

    public void ChangeAnim(string key, float value) 
    {
        if (PlayerAnimator.runtimeAnimatorController && PlayerAnimator.gameObject.activeInHierarchy)
            PlayerAnimator.SetFloat(key, value);

        if (ChestplateAnimator.runtimeAnimatorController && ChestplateAnimator.gameObject.activeInHierarchy)
            ChestplateAnimator.SetFloat(key, value);

        if (HelmetAnimator.runtimeAnimatorController && HelmetAnimator.gameObject.activeInHierarchy)
            HelmetAnimator.SetFloat(key, value);

        if (LegginsAnimator.runtimeAnimatorController && LegginsAnimator.gameObject.activeInHierarchy)
            LegginsAnimator.SetFloat(key, value);
    }

    public void SetAnimFloat(Vector2 setVector)
    {
        ChangeAnim("moveX", setVector.x);
        ChangeAnim("moveY", setVector.y);
    }
}
