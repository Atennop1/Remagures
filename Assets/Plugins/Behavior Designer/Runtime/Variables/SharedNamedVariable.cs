using UnityEngine;

namespace BehaviorDesigner.Runtime
{
    [System.Serializable]
    public class NamedVariable : GenericVariable
    {
        [SerializeField]
        public string name = "";
    }

    [System.Serializable]
    public class SharedNamedVariable : SharedVariable<NamedVariable>
    {
        public SharedNamedVariable()
        {
            mValue = new NamedVariable();
        }

        public static implicit operator SharedNamedVariable(NamedVariable value) { return new SharedNamedVariable { mValue = value }; }
    }
}