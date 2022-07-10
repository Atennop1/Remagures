using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder 
{
    public class BehaviourTreeRunner : MonoBehaviour 
    {
        public BehaviourTree tree;
        private Context context;

        void Start() 
        {
            context = Context.CreateFromGameObject(gameObject);
            tree = tree.Clone();
            tree.Bind(context);
        }

        void FixedUpdate() 
        {
            if (tree != null)
                tree.Update();
        }
    }
}