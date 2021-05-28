using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutor : MonoBehaviour
{
    public delegate void TouchDel(bool Right);
    public event TouchDel Swipe;

    private Vector3 _startPosTouth, _currentPosTouth;
    private Camera _cam;
    private SpherData _spherData;
    [SerializeField]
    private LayerMask _layerMask;
    private bool _isFirstTutor;

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (GameStage.IsGameFlowe||Tutor.IsTutor)
        {
            if (TouchUtility.TouchCount > 0)
            {
                Touch touch = TouchUtility.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    _startPosTouth = _cam.ScreenToViewportPoint(touch.position);
                    Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100f, _layerMask))
                    {
                        if (hit.collider.gameObject.layer == 11)
                        {
                            _spherData = TrafficInspector.Instance.GetFirstSphereOfRow(hit.collider.GetComponentInParent<Row>().RowNumber);
                        }
                        else if (_isFirstTutor)
                            _spherData = hit.collider.GetComponentInParent<SpherData>();
                    }
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    _currentPosTouth = _cam.ScreenToViewportPoint(touch.position);
                }
            }
            else
            {
                if (_spherData != null&&_spherData.IsRow)
                {
                    if (Mathf.Abs(_currentPosTouth.x - _startPosTouth.x) > 0.05)
                    {
                        _spherData.Move.MoveToAnotherRow((_currentPosTouth.x - _startPosTouth.x) > 0);
                        Swipe?.Invoke((_currentPosTouth.x - _startPosTouth.x) > 0);
                        _isFirstTutor = true;
                    }
                }

                _spherData = null;
            }
        }
    }
}
