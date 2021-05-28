using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTutor : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameStage.Instance.ChangeStage(Stage.StartLevel);
            FindObjectOfType<Tutor>().ActivationTutorOne();
            gameObject.SetActive(false);
        }
    }
}
