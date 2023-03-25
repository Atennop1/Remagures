using UnityEngine;
using System.Collections;

namespace BehaviorDesigner.Runtime
{
    // ScriptableObjects do not have coroutines like monobehaviours do. Therefore we must add the functionality ourselves by using the parent behavior component which is a monobehaviour.
    public class TaskCoroutine
    {
        private IEnumerator mCoroutineEnumerator;
        private Coroutine mCoroutine;
        public Coroutine Coroutine { get { return mCoroutine; } }
        private Behavior mParent;
        private string mCoroutineName;
        private bool mStop = false;
        public void Stop() { mStop = true; }

        public TaskCoroutine(Behavior parent, IEnumerator coroutine, string coroutineName)
        {
            mParent = parent;
            mCoroutineEnumerator = coroutine;
            mCoroutineName = coroutineName;
            mCoroutine = parent.StartCoroutine(RunCoroutine());
        }

        public IEnumerator RunCoroutine()
        {
            while (!mStop) {
                if (mCoroutineEnumerator != null && mCoroutineEnumerator.MoveNext()) {
                    yield return mCoroutineEnumerator.Current;
                } else {
                    break;
                }
            }
            mParent.TaskCoroutineEnded(this, mCoroutineName);
        }
    }
}