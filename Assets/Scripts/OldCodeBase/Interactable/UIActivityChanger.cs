using System.Collections.Generic;
using UnityEngine;

namespace Remagures.Interactable
{
    public class UIActivityChanger : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _canvases;

        public void TurnOn()
        {
            foreach (var canvas in _canvases)
                canvas.SetActive(true);
        }
        
        public void TurnOff()
        {
            foreach (var canvas in _canvases)
                canvas.SetActive(false);
        }
    }
}