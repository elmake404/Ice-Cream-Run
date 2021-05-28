using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutor : MonoBehaviour
{
    public static bool IsTutor;

    [SerializeField]
    private GameObject _tutorFirst, _tutorSecond;
    [SerializeField]
    private float _firstTutorTime, _secondTutorTime;
    private bool _completedTutor;


    void Awake()
    {
        FindObjectOfType<PlayerTutor>().Swipe += ActiVationTutorTwo;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(Time.fixedDeltaTime);
        }
    }
    public void ActivationTutorOne()
    {
        StartCoroutine(TutorActivation(_tutorFirst, _firstTutorTime));
    }
    public void ActiVationTutorTwo(bool Right)
    {
        if (!_completedTutor)
        {
            GameStage.IsGameFlowe = true;
            _tutorFirst.SetActive(false);
            _completedTutor = true;
            StartCoroutine(TutorActivation(_tutorSecond, _secondTutorTime));
        }
        else
        {
            _tutorSecond.SetActive(false);
            GameStage.IsGameFlowe = true;
        }
    }
    private IEnumerator TutorActivation(GameObject ActivationObj, float time)
    {
        yield return new WaitForSeconds(time);
        ActivationObj.SetActive(true);
        GameStage.IsGameFlowe = false;
        IsTutor = true;
    }

}
