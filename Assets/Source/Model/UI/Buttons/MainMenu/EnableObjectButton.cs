using System;
using UnityEngine;

namespace Remagures.Model.UI
{
    public sealed class EnableObjectButton : IButton
    {
        private readonly GameObject _gameObject;

        public EnableObjectButton(GameObject gameObject) 
            => _gameObject = gameObject ?? throw new ArgumentNullException(nameof(gameObject));

        public void Press()
            => _gameObject.SetActive(true);
    }
}