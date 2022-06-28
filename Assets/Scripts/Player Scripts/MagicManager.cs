using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MagicManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private InventoryManagerUnique _uniqueManager;
    [SerializeField] private Slider _magicSlider;

    [Header("Values")]
    [SerializeField] private ClassStat _classStat;
    [SerializeField] private FloatValue _maxMagic;
    [SerializeField] private FloatValue _currentMagic;
    
    public Coroutine ManaRuneCoroutine;
    public float MagicCost { get; private set; }

    public void Start()
    {
        _magicSlider.maxValue = _maxMagic.Value;
        ManaRuneCoroutine = StartCoroutine(MagicRuneCoroutine());
        UpdateValue();
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

    private void UpdateValue()
    {
        _magicSlider.value = _currentMagic.Value;
    }
    
    public void AddMagic()
    {
        _magicSlider.value++;
        _currentMagic.Value++;
        if (_currentMagic.Value > _maxMagic.Value)
        {
            _magicSlider.value = _maxMagic.Value;
            _currentMagic.Value = _maxMagic.Value;
        }
    }

    public void DecreseMagic()
    {
        _magicSlider.value -= MagicCost;
        ReduceMagic(MagicCost);
        if (_magicSlider.value < 0)
        {
            _magicSlider.value = 0;
            _currentMagic.Value = 0;
        }
    }

    public void ReduceMagic(float magicCost)
    {
        _currentMagic.Value -= magicCost;
    }

    public IEnumerator MagicRuneCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(60);
            _currentMagic.Value += (int)(_maxMagic.Value / 10);

            if (_currentMagic.Value > _maxMagic.Value)
                _currentMagic.Value = _maxMagic.Value;

            UpdateValue();
        }
    }
}
