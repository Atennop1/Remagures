using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public enum TaskStatus
    {
        Inactive,
        Failure,
        Success,
        Running
    }

    public abstract class Task
    {
        // OnAwake is called once when the behavior tree is enabled. Think of it as a constructor
        public virtual void OnAwake() { }

        // OnStart is called immediately before execution. It is used to setup any variables that need to be reset from the previous run
        public virtual void OnStart() { }

        // OnUpdate runs the actual task
        public virtual TaskStatus OnUpdate() { return TaskStatus.Success; }

        // OnLateUpdate gets called during Unity's LateUpdate callback
        public virtual void OnLateUpdate() { }

        // OnFixedUpdate gets called during Unity's FixedUpdate callback
        public virtual void OnFixedUpdate() { }

        // OnEnd is called after execution on a success or failure
        public virtual void OnEnd() { }

        // OnPause is called when the behavior is paused and resumed
        public virtual void OnPause(bool paused) { }

        // OnConditionalAbort is called when the task is aborted from a conditional abort
        public virtual void OnConditionalAbort() { }

        // Allows for the tasks to be arranged in a priority
        public virtual float GetPriority() { return 0; }

        // Allows for the tasks to be executed according to Utility AI Theory
        public virtual float GetUtility() { return 0; }

        // OnBehaviorRestart is called after the behavior tree restarts
        public virtual void OnBehaviorRestart() { }

        // OnBehaviorComplete is called after the behavior tree finishes executing
        public virtual void OnBehaviorComplete() { }

        // OnReset is called by the inspector to reset the public properties
        public virtual void OnReset() { }

        // Allow OnDrawGizmos to be called from the tasks
        public virtual void OnDrawGizmos() { }

        // Support coroutines within the task
        protected void StartCoroutine(string methodName) { Owner.StartTaskCoroutine(this, methodName); }
        protected Coroutine StartCoroutine(System.Collections.IEnumerator routine) { return Owner.StartCoroutine(routine); }
        protected Coroutine StartCoroutine(string methodName, object value) { return Owner.StartTaskCoroutine(this, methodName, value); }
        protected void StopCoroutine(string methodName) { Owner.StopTaskCoroutine(methodName); }
        protected void StopCoroutine(System.Collections.IEnumerator routine) { Owner.StopCoroutine(routine); }
        protected void StopAllCoroutines() { Owner.StopAllTaskCoroutines(); }

        // Support collision/trigger callbacks:
        public virtual void OnCollisionEnter(Collision collision) { }
        public virtual void OnCollisionExit(Collision collision) { }
        public virtual void OnTriggerEnter(Collider other) { }
        public virtual void OnTriggerExit(Collider other) { }
        public virtual void OnCollisionEnter2D(Collision2D collision) { }
        public virtual void OnCollisionExit2D(Collision2D collision) { }
        public virtual void OnTriggerEnter2D(Collider2D other) { }
        public virtual void OnTriggerExit2D(Collider2D other) { }
        public virtual void OnControllerColliderHit(ControllerColliderHit hit) { }
        public virtual void OnAnimatorIK() { }

        // MonoBehaviour components:
        public GameObject GameObject { set { gameObject = value; } }
        protected GameObject gameObject;
        public Transform Transform { set { transform = value; } }
        protected Transform transform;

        protected T GetComponent<T>() where T : Component
        {
            return gameObject.GetComponent<T>();
        }

        protected Component GetComponent(System.Type type)
        {
            return gameObject.GetComponent(type);
        }

        // Return the inputted GameObject if it is not null, otherwise return the current GameObject component
        protected GameObject GetDefaultGameObject(GameObject go)
        {
            if (go == null) {
                return gameObject;
            }
            return go;
        }

        // NodeData contains properties used by the editor
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
        [SerializeField]
        private NodeData nodeData = null;
        public NodeData NodeData { get { return nodeData; } set { nodeData = value; } }
#endif

        // Keep a reference to the behavior that owns this task
        [SerializeField]
        private Behavior owner = null;
        public Behavior Owner { get { return owner; } set { owner = value; } }

        // The unique id of the task
        [SerializeField]
        private int id = -1;
        public int ID { get { return id; } set { id = value; } }

        [SerializeField]
        private string friendlyName = "";
        public virtual string FriendlyName { get { return friendlyName; } set { friendlyName = value; } }

        [SerializeField]
        private bool instant = true;
        public bool IsInstant { get { return instant; } set { instant = value; } }

        private int referenceID = -1;
        public int ReferenceID { get { return referenceID; } set { referenceID = value; } }

        private bool disabled;
        public bool Disabled { get { return disabled; } set { disabled = value; } }
    }
}