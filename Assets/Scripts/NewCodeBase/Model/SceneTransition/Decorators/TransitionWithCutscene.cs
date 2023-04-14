using System;
using Remagures.Model.CutscenesSystem;

namespace Remagures.Model.SceneTransition
{
    public sealed class TransitionWithCutscene : ISceneTransition
    {
        private readonly ISceneTransition _transition;
        private readonly ICutscene _cutscene;

        public TransitionWithCutscene(ISceneTransition transition, ICutscene cutscene)
        {
            _transition = transition ?? throw new ArgumentNullException(nameof(transition));
            _cutscene = cutscene ?? throw new ArgumentNullException(nameof(cutscene));
        }

        public void Activate()
        {
            _transition.Activate();
            _cutscene.Start();
        }
    }
}