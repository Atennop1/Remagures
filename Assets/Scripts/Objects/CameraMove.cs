using System.Collections;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector2 _maxPos;
    [SerializeField] private Vector2 _minPos;
    private Animator _anim;

    public void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void Update()
    {
        if (transform.position != _target.position)
        {
            Vector3 targetPosition = new Vector3(_target.position.x, _target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, _minPos.x, _maxPos.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, _minPos.y, _maxPos.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, 1f);
        }
    }

    public void ScreenKick()
    {
        _anim.SetBool("Kick", true);
        StartCoroutine(ScreenKickCoroutine());
    }

    public IEnumerator ScreenKickCoroutine()
    {
        yield return null;
        _anim.SetBool("Kick", false);
    }
}
