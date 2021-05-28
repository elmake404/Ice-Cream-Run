using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TopingType
{
    cylinder,
    polygon,
    parallelepiped,
}
public class Toping : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _topping;
    private SpherData _spherData;
    [SerializeField]
    private TopingType _myType; public TopingType TopingType { get { return _myType; } }
    [SerializeField]
    private ParticleSystem _particle;
    public void Initialization(SpherData spherData)
    {
        _spherData = spherData;
    }
    public void ActivationTopping()
    {
        for (int i = 0; i < _topping.Count; i++)
        {
            if (_spherData.transform.position.y > _topping[i].position.y &&
                Mathf.Abs(_spherData.transform.position.z - _topping[i].position.z) < 0.1f)
            {
                _topping[i].gameObject.SetActive(true);
            }
        }
    }
    [ContextMenu("Get All Toping In The List")]
    private void GetAllTopingInTheList()
    {
        _topping = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            _topping.Add(transform.GetChild(i));
        }
    }
    public void IsThereAnActiveObject()
    {
        for (int i = 0; i < _topping.Count; i++)
        {
            if (_topping[i].gameObject.activeSelf)
            {
                _particle.Play();
                _particle.transform.rotation = Quaternion.identity;
                _particle.transform.SetParent(null);
                gameObject.SetActive(false);

                return;
            }
        }
    }

}
