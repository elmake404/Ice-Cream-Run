using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomRow : MonoBehaviour
{
    [SerializeField]
    private int _row;
    private Vector3 _startPosTouth, _currentPosTouth;
    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
    }
    private void Update()
    {
        if (GameStage.IsGameFlowe)
        {
            if (TouchUtility.TouchCount > 0)
            {
                Touch touch = TouchUtility.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    _startPosTouth = _cam.ScreenToViewportPoint(touch.position);
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    _currentPosTouth = _cam.ScreenToViewportPoint(touch.position);
                }
            }
        }
    }

    public void MovingSpherToAnotherRow()
    {
        Debug.Log(1);
        SpherData spher = TrafficInspector.Instance.GetFirstSphereOfRow(_row);
        if (spher!= null)
        {
            Debug.Log(2);

            spher.Move.MoveToAnotherRow((_currentPosTouth.x - _startPosTouth.x) > 0);
        }
    }
}
