using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    private List<SpherData> _spherDatas = new List<SpherData>();
    private List<GameObject> _ethers = new List<GameObject>();
    public bool IsOnGround { get { return _ethers.Count > 0; } }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if (!_ethers.Contains(other.gameObject)) _ethers.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_ethers.Contains(other.gameObject))
        {
            _ethers.Remove(other.gameObject);
        }
    }
    public int RowNumber
    {
        get; private set;
    }
    public Transform GetRowLastPrent() => _spherDatas.Count > 0 ? _spherDatas[_spherDatas.Count - 1].transform : transform;
    public Transform GetRowParent(int number) => number <= 0 ? transform : _spherDatas[number - 1].transform;
    public Vector3 GetLocalPosition(float radius)
    {
        if (_spherDatas.Count > 0)
        {
            Vector3 posSpher = transform.position;
            for (int i = 0; i < _spherDatas.Count; i++)
            {
                posSpher.y += _spherDatas[i].Radius * 2;
            }
            posSpher.y += radius;

            return _spherDatas[_spherDatas.Count - 1].transform.InverseTransformPoint(posSpher);
        }
        else
        {
            Vector3 posSpher = transform.position;
            posSpher.y += radius;

            return transform.InverseTransformPoint(posSpher);
        }
    }
    public List<SpherData> GetHigherSpheres(SpherData spher)
    {
        List<SpherData> sphers = new List<SpherData>();

        if (_spherDatas.Contains(spher))
        {
            int numberSpher = _spherDatas.IndexOf(spher);

            for (int i = numberSpher; i < _spherDatas.Count; i++)
            {
                sphers.Add(_spherDatas[i]);
            }
            _spherDatas.RemoveRange(numberSpher, _spherDatas.Count - numberSpher);
        }
        else
            sphers.Add(spher);

        return sphers;
    }
    public SpherData GetFirstSphere() => _spherDatas.Count > 0 ? _spherDatas[0] : null;
    public int GetCountSpher()
        => _spherDatas.Count;
    public int IndexOf(SpherData spher) => _spherDatas.IndexOf(spher);
    public void UpdateSpherPosition()
    {
        Vector3 posSpher = transform.position;
        Transform ParentSphere = transform;

        for (int i = 0; i < _spherDatas.Count; i++)
        {
            posSpher.y += _spherDatas[i].Radius;
            _spherDatas[i].transform.localPosition = ParentSphere.InverseTransformPoint(posSpher);
            ParentSphere = _spherDatas[i].transform;
            posSpher.y += _spherDatas[i].Radius;
        }
    }
    public void AddSpher(List<SpherData> spherDatas)
    {
        for (int i = 0; i < spherDatas.Count; i++)
        {
            spherDatas[i].RowNumber = RowNumber;
        }
        _spherDatas.AddRange(spherDatas);
    }
    public void RemoveSpher(SpherData spher)
    {
        if (!_spherDatas.Contains(spher)) return;

        int index = _spherDatas.IndexOf(spher);
        _spherDatas.Remove(spher);
        spher.transform.SetParent(null);
        for (int i = index; i < _spherDatas.Count; i++)
        {
            _spherDatas[i].transform.SetParent(GetRowParent(i));
        }
    }
    public bool ConteinsSpher(SpherData spher) => _spherDatas.Contains(spher);
    public void InitializationNumber(int number) => RowNumber = number;
    public void MixedSpher(Vector3 position)
    {
        Vector3 posSpher = position;
        Transform ParentSphere = transform;

        for (int i = 0; i < _spherDatas.Count; i++)
        {
            posSpher.y += _spherDatas[i].Radius - 0.1f;
            _spherDatas[i].transform.localPosition = ParentSphere.InverseTransformPoint(posSpher);
            ParentSphere = _spherDatas[i].transform;
            posSpher.y += _spherDatas[i].Radius - 0.1f;
        }

    }
}
