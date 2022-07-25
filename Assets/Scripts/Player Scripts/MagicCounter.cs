using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCounter : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private ClassStat _classStat;
    [SerializeField] private FloatValue _maxMagic;
    [SerializeField] private FloatValue _currentMagic;

    [Space]
    [SerializeField] private MagicView _view;
    
    public float MagicCost { get; private set; }
    private Coroutine _manaRuneCoroutine;

    public void Start()
    {
        _manaRuneCoroutine = StartCoroutine(MagicRuneCoroutine());
        _view.UpdateValue(_currentMagic.Value);
    }

    public void SetupProjectile(Projectile currentProjectile)
    {
        if ((currentProjectile as Arrow) == null)
            MagicCost = Mathf.RoundToInt(currentProjectile.MagicCost * _classStat.MagicCostCoefficient);
        else
            MagicCost = currentProjectile.MagicCost;

        if (MagicCost <= 0)
            MagicCost = 1;
    }

    public void AddMagic()
    {
        _currentMagic.Value++;

        if (_currentMagic.Value > _maxMagic.Value)
            _currentMagic.Value = _maxMagic.Value;

        _view.UpdateValue(_currentMagic.Value);
    }

    public void DecreseMagic()
    {
        _currentMagic.Value -= MagicCost;

        if (_currentMagic.Value < 0)
            _currentMagic.Value = 0;

        _view.UpdateValue(_currentMagic.Value);
    }

    public void SetupManaRune(bool active)
    {
        if (active)
            _manaRuneCoroutine = StartCoroutine(MagicRuneCoroutine());
            
        else if (_manaRuneCoroutine != null)
            StopCoroutine(_manaRuneCoroutine);   
    }

    private IEnumerator MagicRuneCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(60);
            _currentMagic.Value += (int)(_maxMagic.Value / 10);

            if (_currentMagic.Value > _maxMagic.Value)
                _currentMagic.Value = _maxMagic.Value;

            _view.UpdateValue(_currentMagic.Value);
        }
    }
}
