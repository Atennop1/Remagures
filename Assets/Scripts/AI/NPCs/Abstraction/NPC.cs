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
            _dialogSignal.Raise();
            Context.Raise();
        }
    }

    public void EndTalk()
    {
        if (CurrentState == NPCState.Talk)
        {
            ChangeState(NPCState.Wait);
            Context.Raise();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && CurrentState != NPCState.Talk)
        {
            base.OnTriggerEnter2D(collision);
            Animations.animator.SetBool("isStaying", true);
            ChangeState(NPCState.Wait);
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            base.OnTriggerExit2D(collision);
            Animations.animator.SetBool("isStaying", false);
            ChangeState(NPCState.Walk);
        }
    }
    
    protected void ChangeState(NPCState newState)
    {
        if (newState != CurrentState)
            CurrentState = newState;
    }
}
