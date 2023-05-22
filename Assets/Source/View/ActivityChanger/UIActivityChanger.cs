using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.View
{
    public sealed class UIActivityChanger : SerializedMonoBehaviour, IUIActivityChanger
    {
        [SerializeField] private List<Canvas> _canvases;

        public void TurnOn()
        {
            foreach (var canvas in _canvases)
                canvas.gameObject.SetActive(true);
        }
        
        public void TurnOff()
        {
            foreach (var canvas in _canvases)
                canvas.gameObject.SetActive(false);
        }
    }
}