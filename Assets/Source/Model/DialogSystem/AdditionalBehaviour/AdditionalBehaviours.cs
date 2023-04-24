using System.Collections.Generic;

namespace Remagures.Model.DialogSystem
{
    public sealed class AdditionalBehaviours : IAdditionalBehaviour
    {
        private readonly List<IAdditionalBehaviour> _additionalBehaviours;

        public AdditionalBehaviours(List<IAdditionalBehaviour> additionalBehaviours)
            => _additionalBehaviours = additionalBehaviours;
        
        public void Use()
            => _additionalBehaviours.ForEach(behaviour => behaviour.Use());
    }
}