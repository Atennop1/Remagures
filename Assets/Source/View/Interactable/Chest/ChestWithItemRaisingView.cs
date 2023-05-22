using System;
using Remagures.Model.Character;
using Remagures.Model.InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.View.Interactable
{
    public sealed class ChestWithItemRaisingView : SerializedMonoBehaviour, IChestWithItemRaisingView
    {
        [SerializeField] private SpriteRenderer _raisedItemSpriteRenderer;
        
        private CharacterAnimations _characterAnimations;
        private const string RECEIVING_ANIMATOR_NAME = "receiving";
        
        public void Construct(CharacterAnimations characterAnimations)
            => _characterAnimations =characterAnimations ?? throw new ArgumentNullException(nameof(characterAnimations));

        public void EndDisplaying()
        {
            _characterAnimations.SetBool(RECEIVING_ANIMATOR_NAME, false);
            _raisedItemSpriteRenderer.sprite = null;
        }

        public void Display(IItem item)
        {
            _characterAnimations.SetBool(RECEIVING_ANIMATOR_NAME, true);
            _raisedItemSpriteRenderer.sprite = item.Sprite;
        }
    }
}