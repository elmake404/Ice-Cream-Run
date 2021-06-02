using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnSpherData
{
    public int[] Count;
}
[System.Serializable]
public struct SpawnAdditionalSpherData
{
    public int Row, NumberColor;
    public float ZPosition;
}
public class SpawnerSpher : MonoBehaviour
{
    [SerializeField]
    private SpawnSpherData _startSpawn;
    //[SerializeField]
    //private List<SpawnAdditionalSpherData> _additionalSphere;
    [SerializeField]
    private SpherData _spherDataPrefabs;
    [SerializeField]
    private TrafficInspector _trafficInspector;

    private bool _startGame;
    private void Start()
    {
        Spawn();
        _startGame = true;
    }
    private void Spawn()
    {
        //for (int i = 0; i < _additionalSphere.Count; i++)
        //{
        //    Vector3 positionSpher = _trafficInspector.GetGlobalPositionRow(_additionalSphere[i].Row, _spherDataPrefabs.Radius);
        //    positionSpher.z += _additionalSphere[i].ZPosition;

        //    SpherData spher = Instantiate(_spherDataPrefabs, positionSpher, Quaternion.identity);
        //    spher.RowNumber = _additionalSphere[i].Row;
        //    _trafficInspector.AddAdditionalSphere(spher);
        //    spher.ChooseModelColor(_additionalSphere[i].NumberColor);
        //}

        for (int j = 0; j < _startSpawn.Count.Length; j++)
        {
            SpherData spher = Instantiate(_spherDataPrefabs, transform.position, Quaternion.identity);
            spher.RowNumber = 1;
            _trafficInspector.AddNewSpher(1, spher);
            //spher.StoodInARow();
            spher.ChooseModelColor(_startSpawn.Count[j]);
        }
    }
    //private void OnDrawGizmos()
    //{
    //    if (!_startGame)
    //    {
    //        Gizmos.color = Color.blue;
    //        if (_additionalSphere.Count > 0)
    //        {
    //            for (int i = 0; i < _additionalSphere.Count; i++)
    //            {
    //                Vector3 positionSpher = _trafficInspector.GetGlobalPositionRow(_additionalSphere[i].Row, _spherDataPrefabs.Radius);
    //                positionSpher.z += _additionalSphere[i].ZPosition;
    //                Gizmos.DrawSphere(positionSpher, _spherDataPrefabs.Radius);
    //            }
    //        }
    //    }
    //}
}
