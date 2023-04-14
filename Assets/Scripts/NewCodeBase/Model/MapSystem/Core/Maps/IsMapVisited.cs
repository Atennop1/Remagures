using System;
using System.Collections.Generic;
using System.Linq;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.Model.MapSystem
{
    public sealed class IsMapVisited : IIsMapVisited
    {
        private readonly IReadOnlyList<SceneData> _scenes;

        public IsMapVisited(IReadOnlyList<SceneData> scenes)
            => _scenes = scenes ?? throw new ArgumentNullException(nameof(scenes));

        public bool Get()
            => _scenes.Count == 0 || _scenes.Any(scene => PlayerPrefs.HasKey("Visited" + scene.Path));
    }
}