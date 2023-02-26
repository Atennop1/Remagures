using System;
using Cysharp.Threading.Tasks;

namespace Remagures.Model.MapSystem
{
    public sealed class MapExploringCycle
    {
        private readonly MapExplorer _mapExplorer;

        public MapExploringCycle(MapExplorer mapExplorer)
            => _mapExplorer = mapExplorer ?? throw new ArgumentNullException(nameof(mapExplorer));

        public async void Activate()
            => await ExploringCycleTask();

        private async UniTask ExploringCycleTask()
        {
            while (true)
            {
                _mapExplorer.Explore();
                await UniTask.Delay(200);
            }
        }
    }
}