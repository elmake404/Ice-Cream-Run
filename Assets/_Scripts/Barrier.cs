using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField]
    private float _speedRotation;
    [SerializeField]
    private bool _isKnife;public bool IsKnife { get { return _isKnife; } }
    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * _speedRotation);
    }
}
