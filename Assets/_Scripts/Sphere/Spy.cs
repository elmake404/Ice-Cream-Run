using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spy : MonoBehaviour
{
    [SerializeField]
    private SphereLife _spher;
    private Transform _target
    { get { return _spher.transform; } }
    private Vector3 _offset;
    [SerializeField]
    private float _speedOfMovementInARow = 50f;
    private void Awake()
    {
        _spher.Died += Death;
    }
    void LateUpdate()
    {
        if (transform.parent != _target)
        {
            Vector3 newPos = transform.position;
            newPos.z = _target.position.z - _offset.z;
            transform.position = (newPos);
            MoveSpher();
        }
    }
    private void MoveSpher()
    {
        Vector3 pos = transform.localPosition;

        if ((transform.position.y > _target.position.y || (Mathf.Abs(transform.position.y - _target.position.y) <= 0.01))
            && Mathf.Abs(transform.position.x - _target.position.x) > 0.01)
        {
            pos.x = _target.position.x;
        }
        else if (Mathf.Abs(transform.position.y - _target.position.y) > 0.01)
        {
            pos.y = _target.position.y;
        }
        else
        {
            _offset = Vector3.MoveTowards(_offset, Vector3.zero, _speedOfMovementInARow * Time.deltaTime);
        }

        transform.position = Vector3.MoveTowards(transform.position, pos, _speedOfMovementInARow * Time.deltaTime);
    }
    private void Death() => Destroy(gameObject);
    public void OffsetRecord()
    {
        transform.SetParent(null);
        _offset.z = (_target.position - transform.position).z;
    }
}
