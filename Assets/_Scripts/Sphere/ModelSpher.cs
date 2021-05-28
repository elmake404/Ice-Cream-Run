using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSpher : MonoBehaviour
{
    [SerializeField]
    private SpherData _spherData;
    [SerializeField]
    private List<Material> _materials;
    [SerializeField]
    private List<Toping> _topings;
    private Dictionary<TopingType, Toping> _topingsDictionary = new Dictionary<TopingType, Toping>();
    [SerializeField]
    private MeshRenderer _activeModel;
    [SerializeField]
    private BrokenModel _brokenModel;

    [SerializeField]
    private float _speedRotation;
    private void Start()
    {
        for (int i = 0; i < _topings.Count; i++)
        {
            _topings[i].Initialization(_spherData);
            _topingsDictionary[_topings[i].TopingType] = _topings[i];
        }
    }
    private void FixedUpdate()
    {
        if (_spherData.IsRow && GameStage.IsGameFlowe)
        {
            int x = TrafficInspector.Instance.GetIndexSpher(_spherData.RowNumber, _spherData) % 2 == 0 ? 1 : -1;
            transform.Rotate(Vector3.right * x * _speedRotation);
        }
    }
    public void СhooseСolor(int number)
    {
        if (number >= _materials.Count) number = _materials.Count - 1;
        _activeModel.material = _materials[number];
        _brokenModel.СhooseСolor(_materials[number]);
    }
    public void ActivationToping(TopingType toping) => _topingsDictionary[toping].ActivationTopping();
    public void ModelReplacement()
    {
        for (int i = 0; i < _topings.Count; i++)
        {
            _topings[i].IsThereAnActiveObject();
        }
        _activeModel.gameObject.SetActive(false);
        _brokenModel.Push(-_spherData.transform.forward);
    }
}
