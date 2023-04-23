using Remagures.Model.MeatSystem;
using Remagures.View.MeatSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MeatCookingLoopFactory : SerializedMonoBehaviour
    {
        [SerializeField] private MeatCookingTimerView _meatCookingTimerView;
        [SerializeField] private IMeatCookerFactory _meatCookerFactory;
        private readonly ISystemUpdate _systemUpdate = new SystemUpdate();

        private void Update()
            => _systemUpdate.UpdateAll();
        
        private void Awake()
        {
            var loop = new MeatCookingLoop(_meatCookingTimerView, _meatCookerFactory.Create());
            _systemUpdate.Add(loop);
            loop.Activate();
        }
    }
}