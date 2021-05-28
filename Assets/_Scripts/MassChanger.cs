using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassChanger : MeshDeformation
{
    [SerializeField]
    private float _addedVolume;public float AddedVolume { get { return _addedVolume; } }
    [SerializeField]
    private bool _isTopping; public bool IsTopping { get { return _isTopping; } }
    [SerializeField]
    private TopingType _myToping; public TopingType TopingType { get { return _myToping; } }
}
