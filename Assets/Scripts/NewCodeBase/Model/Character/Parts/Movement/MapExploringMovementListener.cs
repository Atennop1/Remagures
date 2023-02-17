using System;
using Remagures.MapSystem;
using Remagures.Root;

namespace Remagures.Model.Character
{
    public class MapExploringMovementListener : IUpdatable
    {
        private readonly ICharacterMovement _characterMovement;
        private readonly MapExplorer _mapExplorer;

        public MapExploringMovementListener(ICharacterMovement characterMovement, MapExplorer mapExplorer)
        {
            _characterMovement = characterMovement ?? throw new ArgumentNullException(nameof(characterMovement));
            _mapExplorer = mapExplorer ?? throw new ArgumentNullException(nameof(mapExplorer));
        }

        public void Update()
        {
            if (_characterMovement.IsMoving)
                _mapExplorer.Explore();
        }
    }
}