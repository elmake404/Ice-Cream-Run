﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStageEvent
{
    public delegate void Empty();
    public static event Empty StartLevel;
    public static event Empty WinLevel;
    public static void InvokeStartLevel()
    {
        StartLevel?.Invoke();
    }
    public static void InvokeWinLevel()
    {
        WinLevel?.Invoke();
    }
}
public enum Stage { StartGame, StartLevel, WinGame, LostGame }
public class GameStage : MonoBehaviour
{
    public static GameStage Instance;

    private static bool _isGameStart;
    public static bool IsGameFlowe;
    //{ get; private set; }

    [SerializeField]
    private CanvasManager _canvasManager;
    public Stage StageGame
    { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        ChangeStage(_isGameStart ? Stage.StartLevel : Stage.StartGame);
    }

    void Update()
    {

    }
    public void ChangeStage(Stage stage)
    {
        StageGame = stage;

        switch (stage)
        {
            case Stage.StartGame:
                FacebookManager.Instance.GameStart();
                _canvasManager.GameStageWindow(StageGame);
                _isGameStart = true;
                break;

            case Stage.StartLevel:
                FacebookManager.Instance.LevelStart(PlayerPrefs.GetInt("Level"));
                _canvasManager.GameStageWindow(StageGame);
                GameStageEvent.InvokeStartLevel();
                IsGameFlowe = true;
                break;

            case Stage.WinGame:
                if (IsGameFlowe)
                {
                    FacebookManager.Instance.LevelWin(PlayerPrefs.GetInt("Level"));

                    _canvasManager.GameStageWindow(StageGame);
                    GameStageEvent.InvokeWinLevel();

                    PlayerPrefs.SetInt("Scenes", PlayerPrefs.GetInt("Scenes") + 1);
                    PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

                    IsGameFlowe = false;

                }
                break;

            case Stage.LostGame:
                if (IsGameFlowe)
                {
                    FacebookManager.Instance.LevelFail(PlayerPrefs.GetInt("Level"));

                    _canvasManager.GameStageWindow(StageGame);

                    IsGameFlowe = false;

                }
                break;
        }

    }
    public void LevelStart()
    {
        ChangeStage(Stage.StartLevel);
    }
}
