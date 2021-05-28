using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZeroLevel : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("Scenes") <= 1)
        {
            PlayerPrefs.SetInt("Scenes", 2);
        }

        if (PlayerPrefs.GetInt("Level") <= 0)
        {
            PlayerPrefs.SetInt("Level", 1);
        }

        if (PlayerPrefs.GetInt("Tutor") != 100)
        {
            SceneManager.LoadScene(1);
            PlayerPrefs.SetInt("Tutor", 100);
            PlayerPrefs.SetInt("Level", 0);

        }
        else if (PlayerPrefs.GetInt("Scenes") < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Scenes"));
        }
        else
        {
            PlayerPrefs.SetInt("Scenes", 2);
            SceneManager.LoadScene(PlayerPrefs.GetInt("Scenes"));
        }
    }

}
