using Remagures.DialogSystem;
using Remagures.Interactable;
using Remagures.Root;
using UnityEngine;

namespace Remagures.AI.NPCs
{
    [RequireComponent(typeof(NPCAnimations))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public abstract class NPC : Interactable.Interactable
    {
        [Header("NPC Stuff")] [SerializeField] private DialogView _dialogView;
        [SerializeField] private DialogsListRoot _dialogsListRoot;
        [SerializeField] private UIActivityChanger _uiActivityChanger;
        [field: SerializeField] protected Transform Player { get; private set; }

        protected Rigidbody2D Rigidbody { get; private set; }
        protected NPCAnimations Animations { get; private set; }
        protected NPCState CurrentState { get; private set; }
    
        private readonly int IS_STAYING_ANIMATOR_NAME = Animator.StringToHash("isStaying");

        public virtual void Start()
        {
            Animations = GetComponent<NPCAnimations>();
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        public override void Interact()
        {
            if (CurrentState == NPCState.Talk || !PlayerInRange) 
                return;
        
            ChangeState(NPCState.Talk);
            _dialogView?.Activate(_dialogsListRoot?.BuiltDialogList.CurrentDialog);
            
            ContextClue.ChangeContext();
            _uiActivityChanger.TurnOff();
        }

        public void EndTalk()
        {
            if (CurrentState != NPCState.Talk) 
                return;
        
            ChangeState(NPCState.Wait);
            ContextClue.ChangeContext();
        }

        protected override void TriggerEnter()
        {
            Animations.Animator.SetBool(IS_STAYING_ANIMATOR_NAME, true);
            ChangeState(NPCState.Wait);
        }

        protected override void TriggerExit()
        {
            Animations.Animator.SetBool(IS_STAYING_ANIMATOR_NAME, false);
            ChangeState(NPCState.Walk);
        }

        protected override bool CanTriggerEnter(Collider2D collision)
            => collision.TryGetComponent<Player.Player>(out _) && !collision.isTrigger && CurrentState != NPCState.Talk;

        protected override bool CanTriggerExit(Collider2D collision)
            => collision.TryGetComponent<Player.Player>(out _) && !collision.isTrigger;

        protected void ChangeState(NPCState newState)
        {
            if (newState != CurrentState)
                CurrentState = newState;
        }
    }
}
