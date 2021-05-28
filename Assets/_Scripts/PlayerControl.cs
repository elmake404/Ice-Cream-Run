using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Vector3 _startPosTouth, _currentPosPlayer, _targetPosPlayer;
    private Transform _moveRowe;
    private Camera _cam;

    [SerializeField]
    private float _runningSpeed;

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
                    Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
                    _currentPosPlayer = transform.position;

                    _startPosTouth = (_cam.transform.position - ((ray.direction) *
                            ((_cam.transform.position - transform.position).z / ray.direction.z)));
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

                    if (_startPosTouth == Vector3.zero)
                    {
                        _startPosTouth = (_cam.transform.position - ((ray.direction) *
                                ((_cam.transform.position - transform.position).z / ray.direction.z)));
                    }

                    _targetPosPlayer = _currentPosPlayer + ((_cam.transform.position - ((ray.direction) *
                            ((_cam.transform.position - transform.position).z / ray.direction.z))) - _startPosTouth);
                }
            }
        }
        else
        {
            _targetPosPlayer = _moveRowe.position;
        }

    }
    //private void FixedUpdate()
    //{
    //    if (GameStage.IsGameFlowe)
    //    {
    //        Vector3 PosX = transform.position;
    //        PosX.x = CheckLimmit(_targetPosPlayer);
    //        transform.position = Vector3.MoveTowards(transform.position, PosX, GetSpeed(_lateralSpeed));

    //        transform.Translate(Vector3.forward * _runningSpeed);
    //    }
    //}
}
