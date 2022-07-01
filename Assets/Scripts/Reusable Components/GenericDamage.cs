using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class GenericDamage : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] protected float _damage;
    [SerializeField] private string _otherTag;

    private GenericFlash _enemyFlash;
    private bool _isStuned;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<GenericHealth>(out GenericHealth temp) && temp.LayerMask == (temp.LayerMask | (1 << gameObject.layer)) && other.isTrigger)
        {
            if (temp != null && (temp as PlayerHealth) != null)
            {   
                (temp as PlayerHealth).Damage(_damage, other.gameObject.transform.parent.gameObject.GetComponent<PlayerController>());
                _enemyFlash = other.gameObject.transform.parent.GetComponent<GenericFlash>();

                if (_enemyFlash != null && !_isStuned)
                    Flash(true, other, _enemyFlash.DamageColor, _enemyFlash.RegularColor);
            }
        }
    }
    
    protected void Flash(bool isPlayer, Collider2D other, Color flashColor, Color afterFlashColor)
    {
        StartCoroutine(Stun());
        GameObject newObject = null;
        if (isPlayer)
            newObject = other.gameObject.transform.parent.gameObject;
        else
            newObject = other.gameObject;

        FlashObject(newObject, flashColor, afterFlashColor);
        foreach (Transform child in newObject.transform)
            FlashObject(child.gameObject, flashColor, afterFlashColor);
    }

    private void FlashObject(GameObject obj, Color flashColor, Color afterFlashColor)
    {
        GenericFlash flash = obj.GetComponent<GenericFlash>();
        if (flash != null)
            flash.StartFlashCoroutine(flashColor, afterFlashColor);
    }

    private IEnumerator Stun()
    {
        _isStuned = true;
        yield return null;
        _isStuned = false;
    }
}
