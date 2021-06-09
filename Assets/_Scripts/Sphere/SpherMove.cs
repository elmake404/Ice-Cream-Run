﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherMove : MonoBehaviour
{
    private SpherData _spherData;
    private TrafficInspector _trafficInspector;
    [SerializeField]
    private Rigidbody _rbMain;
    [SerializeField]
    private float _speedHorn;
    private bool _death;
    public bool IsOnReiki { get; private set; }


    void Awake()
    {
        _spherData = GetComponent<SpherData>();
        GameStageEvent.WinLevel += EndGame;
    }
    private void Start()
    {
        _trafficInspector = TrafficInspector.Instance;
    }
    private void OnDestroy()
    {
        GameStageEvent.WinLevel -= EndGame;
    }
    void FixedUpdate()
    {
        if (GameStage.IsGameFlowe && !_death)
            if ((_spherData.RowNumber != -1) && _trafficInspector.GetIndexSpher(_spherData.RowNumber, _spherData) == 0)
            {
                _rbMain.isKinematic = false;
            }
            else
            {
                _rbMain.isKinematic = true;
            }
    }
    private void OnTriggerEnter(Collider other)
    {
    }
    private void OnTriggerExit(Collider other)
    {
    }
    private void EndGame()
    {
        GameStageEvent.WinLevel -= EndGame;

        _rbMain.isKinematic = true;
    }
    private IEnumerator MoveToHorn(Transform Target)
    {
        float Yoffset = transform.position.y - Target.position.y;
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position + Vector3.up * Yoffset, _speedHorn);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }
    public void MoveToAnotherRow(bool right)
    {
        int row = right ? 1 : -1;
        row += _spherData.RowNumber;

        if (!_spherData.IsRow
            || !TrafficInspector.Instance.CheckingSeriesForExistence(row)
            || !TrafficInspector.Instance.RowIsOnTheGround(row))
            return;

        if (_trafficInspector.CheckRow(row))
        {
            Vector3 posSpher = TrafficInspector.Instance.GetLocalPositionInRow(row, _spherData.Radius);
            TrafficInspector.Instance.AddSpherDats(row, _spherData);
            transform.localPosition = posSpher;
        }
    }
    public void RigidbodyConstraintsNone()
    {
        _death = true;
        _rbMain.constraints = RigidbodyConstraints.None;
        _rbMain.isKinematic = false;
    }
    public void GoToTheHorn(Transform Target)
    {
        StartCoroutine(MoveToHorn(Target));
    }
}
