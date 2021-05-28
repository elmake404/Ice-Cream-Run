using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    [SerializeField]
    private BezierSpline _spline;

    [SerializeField]
    private float _way;
    [HideInInspector]
    public Transform Sightseer;
    void Start()
    {
        transform.position = _spline.GetPoint(_way);
    }

    void FixedUpdate()
    {
        if (Sightseer != null)
        {
            if ((transform.position - Sightseer.position).sqrMagnitude <= 0.5f)
            {
                _way += 0.1f;
                transform.position = _spline.GetPoint(_way);
            }
            else if(_way>=1)
            {
                enabled = false;
                TrafficInspector.Instance.MixingAllSpher(transform.position);
                GameStage.Instance.ChangeStage(Stage.WinGame);
            }
        }
    }
}
