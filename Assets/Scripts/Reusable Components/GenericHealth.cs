using System.Collections;
using UnityEngine;
using System;

public class GenericHealth : MonoBehaviour
{
    [SerializeField] private int _fireMinTime;
    [SerializeField] private int _fireMaxTime;

    [field: SerializeField, Space] public LayerMask LayerMask { get; private set; }
    private Coroutine _magicDamageCoroutine;

    public virtual void Damage(float amountToDamage) { }

    public void StartFireCoroutine(GenericFlash flash, float damage, Action<float> damageAction)
    {
        if (gameObject.activeInHierarchy)
        {
            if (_magicDamageCoroutine != null)
                StopCoroutine(_magicDamageCoroutine);
            _magicDamageCoroutine = StartCoroutine(Fire(flash, damage, damageAction));
        }
    }

    private IEnumerator Fire(GenericFlash flash, float damage, Action<float> damageAction)
    {
        if (flash != null)
        {
            flash.SpriteRenderer.color = flash.FireColor;
            int randomTime = UnityEngine.Random.Range(_fireMinTime, _fireMaxTime + 1);

            for (int i = 0; i < randomTime; i++)
            {
                yield return new WaitForSeconds(1);
                damageAction((int)(damage / 2 + 0.5f));
                flash.StartFlashCoroutine(flash.DamageColor, i == randomTime - 1 ? flash.RegularColor : flash.FireColor);
            }
        }
    }
}