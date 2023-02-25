using System;
using Cysharp.Threading.Tasks;

namespace Remagures.Model.MapSystem
{
    public sealed class MapExploringCycle
    {
        private readonly MapExplorer _mapExplorer;
        private bool _canExplore;

        public MapExploringCycle(MapExplorer mapExplorer)
            => _mapExplorer = mapExplorer ?? throw new ArgumentNullException(nameof(mapExplorer));

        public async void Activate()
            => await ExploringCycleTask();

        private async UniTask ExploringCycleTask()
        {
            while (true)
            {
                if (!_canExplore) 
                    continue;
            
                _mapExplorer.Explore();
                await ExploringCooldownTask();
                await UniTask.Yield();
            }
        }

        private async UniTask ExploringCooldownTask()
        {
            _canExplore = false;
            await UniTask.Delay(200);
            _canExplore = true;
        }
    }
}