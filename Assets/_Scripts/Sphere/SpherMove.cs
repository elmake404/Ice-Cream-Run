using System.Collections;
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
        if (_rbMain.velocity.y > 0 && transform.position.y> _trafficInspector.GetGlobalPositionRow(_spherData.RowNumber,_spherData.Radius).y)
        {
            _rbMain.velocity = Vector3.Slerp(_rbMain.velocity, Vector3.zero, 0.5f);
        }
    }
    private void EndGame()
    {
        GameStageEvent.WinLevel -= EndGame;

        if (_rbMain==null)
        {
            Debug.Log(transform.position);
        }
        _rbMain.isKinematic = true;
    }
    private IEnumerator MoveToHorn(Transform Target)
    {
        float Yoffset = transform.position.y - Target.position.y;
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position,Target.position+Vector3.up*Yoffset,_speedHorn);
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
        _rbMain.constraints = RigidbodyConstraints.None;
    }
    public void GoToTheHorn(Transform Target)
    {
        StartCoroutine(MoveToHorn(Target));
    }
}
