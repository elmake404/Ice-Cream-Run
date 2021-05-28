using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDeformation : MonoBehaviour
{
    [SerializeField]
    private MeshFilter _meshFilter;
    private Mesh _mesh;
    Vector3[] _verts;
    void Start()
    {
        if (_meshFilter != null)
        {
            _mesh = _meshFilter.mesh;
            _verts = _mesh.vertices;
        }
    }
    public void Deform(SpherData spher)
    {
        if (_meshFilter != null)
        {
            Vector3 PositionSpher = transform.InverseTransformPoint(spher.transform.position);
            float radius = spher.Radius + 0.1f;
            for (int i = 0; i < _verts.Length; i++)
            {
                float dist = (_verts[i] - PositionSpher).magnitude;
                if (dist < radius)
                {
                    _verts[i] += Vector3.down * (radius - dist);
                }
            }
            _mesh.vertices = _verts;
        }

    }

}
