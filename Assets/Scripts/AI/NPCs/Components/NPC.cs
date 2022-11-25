using Remagures.DialogSystem.Core;
using Remagures.Interactable;
using Remagures.SO.Other;
using UnityEngine;
using UnityEngine.Serialization;

namespace Remagures.AI.NPCs.Components
{
    [RequireComponent(typeof(NPCAnimations))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public abstract class NPC : Interactable.Abstraction.Interactable
    {
        [Header("NPC Stuff")]
        [SerializeField] private Signal _dialogSignal;
        [SerializeField] private DialogValue _dialogValue;
        [FormerlySerializedAs("_thisDatabase")] [SerializeField] private DialogDatabase _dialogDatabase;
        [field: SerializeField] protected Transform Player { get; private set; }
        [SerializeField] private UIActivityChanger _uiActivityChanger;

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
            _dialogValue.NPCDatabase = _dialogDatabase;
            _dialogSignal.Invoke();
            Context.Invoke();
            _uiActivityChanger.TurnOff();
        }

        public void EndTalk()
        {
            if (CurrentState != NPCState.Talk) 
                return;
        
            ChangeState(NPCState.Wait);
            Context.Invoke();
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
        {
            return collision.TryGetComponent<Player.Player>(out _) && !collision.isTrigger && CurrentState != NPCState.Talk;
        }

        protected override bool CanTriggerExit(Collider2D collision)
        {
            return collision.TryGetComponent<Player.Player>(out _) && !collision.isTrigger;
        }
    
        protected void ChangeState(NPCState newState)
        {
            if (newState != CurrentState)
                CurrentState = newState;
        }
    }
}
