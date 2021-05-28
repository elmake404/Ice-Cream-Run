using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    private Vector3 _offset;
    private Vector3 _velocity;
    private Vector3 _CurentPos { get { return new Vector3(transform.position.x, _target.position.y, _target.position.z); } }
    [SerializeField]
    private Animator _animator;

    private bool _isFinish;
    [SerializeField]
    private float _delay = 0.2f;

    private void Start()
    {
        if (_animator != null)
            _animator.enabled = false;
        _offset = _CurentPos - transform.position;
    }
    private void FixedUpdate()
    {
        if (GameStage.IsGameFlowe|| _animator.enabled)
        {
            transform.position = Vector3.SmoothDamp(transform.position, _CurentPos - _offset, ref _velocity, _delay);
        }
    }
    public void StartAnimation()
    {
        if (_animator != null)
        {
            _animator.enabled = true;
            _isFinish = true;
            _animator.SetBool("End", true);
        }
    }
}
