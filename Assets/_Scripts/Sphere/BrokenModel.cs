using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenModel : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer[] _models;
    [SerializeField]
    private Rigidbody[] _rbBroken;
    public void СhooseСolor(Material color)
    {
        for (int i = 0; i < _models.Length; i++)
        {
            _models[i].material = color;
        }
    }
    public void Push(Vector3 direction)
    {
        float forse=300;
        for (int i = 0; i < _rbBroken.Length; i++)
        {
            _rbBroken[i].gameObject.SetActive(true);
            _rbBroken[i].AddForce(direction * forse, ForceMode.Acceleration);
            forse *= 2;
        }
    }
}
