﻿using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.DialogSystem
{
    public class TextWriterView : MonoBehaviour
    {
        [SerializeField] private Text _continueText;
        [SerializeField] private Text _dialogText;
        
        public void DisplayText(string text)
            => _dialogText.text = text;

        public void DisplayStartOfTyping()
        {
            _dialogText.text = "";
            _continueText.text = "Нажмите, чтобы пролистать";
        }

        public void DisplayEndOfTyping()
            => _continueText.text = "Нажмите, чтобы продолжить";
    }
}