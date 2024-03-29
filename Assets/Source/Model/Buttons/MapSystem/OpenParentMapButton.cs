﻿using System;
using Remagures.Model.MapSystem;

namespace Remagures.Model.Buttons
{
    public sealed class OpenParentMapButton : IButton
    {
        private readonly ParentMapOpener _parentMapOpener;

        public OpenParentMapButton(ParentMapOpener parentMapOpener) 
            => _parentMapOpener = parentMapOpener ?? throw new ArgumentNullException(nameof(parentMapOpener));

        public void Press()
        {
            if (_parentMapOpener.CanOpen())
                _parentMapOpener.Open();
        }
    }
}