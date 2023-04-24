using Remagures.Model.Attacks;
using Remagures.View;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class SwordAttackFactory : SerializedMonoBehaviour, IAttackFactory
    {
        [SerializeField] private IAttackView _attackView;
        [SerializeField] private IAttackData _attackData;
        private IAttack _builtAttack;
        
        public IAttack Create()
        {
            if (_builtAttack != null)
                return _builtAttack;

            _builtAttack = new SwordAttack(_attackView, _attackData);
            return _builtAttack;
        }
    }
}