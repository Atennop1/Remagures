using Remagures.Model.Flashing;
using Remagures.Tools;

namespace Remagures.Model.Attacks
{
    public sealed class Attack : IAttack
    {
        public int Damage { get; }

        public Attack(int damage)
            => Damage = damage.ThrowExceptionIfLessOrEqualsZero();

        public void ApplyTo(Target target)
        {
            target.Health.TakeDamage(Damage);
            target.Flashingable.Flash(FlashColorType.Damage, FlashColorType.BeforeFlash);
            
            /* TODO throw this to another place
            _flash = other.gameObject.transform.parent.GetComponent<Flasher>();

            if (_flash != null)
                Flash(other.gameObject, _flash.DamageColor, _flash.RegularColor); */
        }
    
        /*
        protected void Flash(GameObject gameObject, Color flashColor, Color afterFlashColor)
        {
            FlashObject(gameObject, flashColor, afterFlashColor);
            foreach (Transform child in gameObject.transform)
                FlashObject(child.gameObject, flashColor, afterFlashColor);
        }

        private void FlashObject(GameObject gameObj, Color flashColor, Color afterFlashColor)
        {
            var flash = gameObj.GetComponent<Flasher>();
            if (flash != null)
                flash.StartFlashCoroutine(flashColor, afterFlashColor);
        }
        */
    }
}
