using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrafficInspector : MonoBehaviour
{
    public static TrafficInspector Instance;

    [SerializeField]
    private List<Row> _rows = new List<Row>();
    private List<SpherData> _additionalSphere = new List<SpherData>();

    private int _numberOfSpheresInTheGame
    {
        get
        {
            int count = 0;
            for (int i = 0; i < _rows.Count; i++)
            {
                count += _rows[i].GetCountSpher();
            }
            return count;
        }
    }

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < _rows.Count; i++)
        {
            _rows[i].InitializationNumber(i);
        }
    }
    public void UpdateRowPosition(int Row) => _rows[Row].UpdateSpherPosition();
    public void AddNewSpher(int row, SpherData spherData)
    {
        Vector3 posSpher = GetLocalPositionInRow(row, spherData.Radius);
        AddSpherDats(row, spherData);
        spherData.transform.localPosition = posSpher;
    }
    public void RemoveSpher(int row, SpherData spherData)
    {
        _rows[row].RemoveSpher(spherData);
        if (_numberOfSpheresInTheGame <= 0)
        {
            GameStage.Instance.ChangeStage(Stage.LostGame);
        }
    }
    public void AddSpherDats(int rowNumber, SpherData spher)
    {
        int oldRowNumber = spher.RowNumber;
        Transform parent = _rows[rowNumber].GetRowLastPrent();
        spher.transform.SetParent(parent);
        _rows[rowNumber].AddSpher(_rows[oldRowNumber].GetHigherSpheres(spher));
    }
    public void AddAdditionalSphere(SpherData sphere)
    => _additionalSphere.Add(sphere);
    public void RemoveAdditionalSphere(SpherData sphere)
    => _additionalSphere.Remove(sphere);
    public void GoToTheHorn(Guide guide)
    {
        for (int i = 0; i < _rows.Count; i++)
        {
            if (i != _rows.Count / 2)
            {
                SpherData spherData = _rows[i].GetFirstSphere();
                
              if(spherData!=null)  AddNewSpher(_rows.Count / 2, spherData);
            }
        }
        SpherData spher = _rows[_rows.Count / 2].GetFirstSphere();
        guide.Sightseer = spher.transform;
        spher.Move.GoToTheHorn(guide.transform);
    }
    public void MixingAllSpher(Vector3 position)
    {
        for (int i = 0; i < _rows.Count; i++)
        {
            _rows[i].MixedSpher(position);
        }
    }
    public Vector3 GetLocalPositionInRow(int rowNumber, float radius)
        => _rows[rowNumber].GetLocalPosition(radius);
    public Vector3 GetGlobalPositionRow(int rowNumber, float radius)
     => _rows[rowNumber].transform.TransformPoint(_rows[rowNumber].GetLocalPosition(radius));
    public SpherData GetFirstSphereOfRow(int rowNumber) => _rows[rowNumber].GetFirstSphere();
    public SpherData GetAdditionalSphere(GameObject sphere)
    {
        for (int i = 0; i < _additionalSphere.Count; i++)
        {
            if (_additionalSphere[i].gameObject == sphere)
            {
                return _additionalSphere[i];
            }
        }
        return null;
    }
    public int GetIndexSpher(int row, SpherData spherData) => _rows[row].IndexOf(spherData);
    public bool CheckingSeriesForExistence(int number)
        => number >= 0 && number < _rows.Count;
    public bool ContainsRow(SpherData sphere)
    {
        for (int i = 0; i < _rows.Count; i++)
        {
            if (_rows[i].ConteinsSpher(sphere))
            {
                return true;
            }
        }
        return false;
    }
    public bool RowIsOnTheGround(int number)
        => _rows[number].IsOnGround;
    public bool CheckRow(int row)
    {
        return row >= 0 && row < _rows.Count;
    }
}
