using System;
using UnityEngine;

namespace Remagures.Tools
{
    [Serializable]
    public class SerializableSprite
    {
        private int _textureWidth;
        private int _textureHeight;
        private byte[] _textureBytes;

        [NonSerialized] private Sprite _builtSprite;

        public SerializableSprite(Texture2D texture)
        {
            _textureHeight = texture.height;
            _textureWidth = texture.width;
            _textureBytes = texture.EncodeToPNG();
        }

        public Sprite Get()
        {
            if (_builtSprite != null)
                return _builtSprite;

            var texture = new Texture2D(_textureWidth, _textureHeight);
            texture.LoadImage(_textureBytes);
            
            _builtSprite = Sprite.Create(texture, new Rect(0f, 0f, _textureWidth, _textureHeight), Vector2.one);
            return _builtSprite;
        }
    }
}