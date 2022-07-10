using UnityEngine;

[HideInInspector]
public enum NPCState
{
    Walk,
    Stay,
    Talk,
    Wait
}

[RequireComponent(typeof(NPCAnimations))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class NPC : Interactable
{
    [Header("NPC Stuff")]
    [SerializeField] private Signal _dialogSignal;
    [SerializeField] private DialogValue _dialogValue;
    [SerializeField] private DialogDatabase _thisDatabase;
    [field: SerializeField] protected Transform Player { get; private set; }

    protected Rigidbody2D MyRigidbody { get; private set; }
    protected NPCAnimations Animations { get; private set; }
    protected NPCState CurrentState { get; private set; }

    public virtual void Start()
    {
        Animations = GetComponent<NPCAnimations>();
        MyRigidbody = GetComponent<Rigidbody2D>();
    }

    public override void Interact()
    {
        if (CurrentState != NPCState.Talk && PlayerInRange)
        {
            ChangeState(NPCState.Talk);
            _dialogValue.NPCDatabase = _thisDatabase;
            _dialogSignal.Invoke();
            Context.Invoke();
        }
    }

    public void EndTalk()
    {
        if (CurrentState == NPCState.Talk)
        {
            ChangeState(NPCState.Wait);
            Context.Invoke();
        }
    }

    public override void TriggerEnter()
    {
        Animations.Animator.SetBool("isStaying", true);
        ChangeState(NPCState.Wait);
    }

    public override void TriggerExit()
    {
        Animations.Animator.SetBool("isStaying", false);
        ChangeState(NPCState.Walk);
    }

    public override bool CanTriggerEnter(Collider2D collision)
    {
        return collision.TryGetComponent<PlayerController>(out PlayerController player) && !collision.isTrigger && CurrentState != NPCState.Talk;
    }

    public override bool CanTriggerExit(Collider2D collision)
    {
        return collision.TryGetComponent<PlayerController>(out PlayerController player) && !collision.isTrigger;
    }
    
    protected void ChangeState(NPCState newState)
    {
        if (newState != CurrentState)
            CurrentState = newState;
    }
}
