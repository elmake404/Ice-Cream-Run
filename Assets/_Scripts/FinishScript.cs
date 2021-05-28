using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{
    [SerializeField]
    private Guide PosSpher;
    private bool _isFinish=false;
    private void OnTriggerEnter(Collider other)
    {
        var spher = other.GetComponent<SpherData>();
        if (spher != null && spher.IsRow && GameStage.IsGameFlowe&&!_isFinish)
        {
            _isFinish = true;
            TrafficInspector.Instance.GoToTheHorn(PosSpher);
            FindObjectOfType<CameraMove>().StartAnimation();
        }
    }
}
