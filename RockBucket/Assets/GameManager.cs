using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void GameState();

    public static event GameState GamePaused;
    public static event GameState GameResume;

    public static bool IsPaused { get; private set; }

    public GameRules GameRules;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        SoundEffectController.Load();
        ComboController.Load();

        GamePaused += GameManager_GamePaused;
        GameResume += GameManager_GameResume;

        SetPause(false);
    }

    private void OnDestroy()
    {
        GamePaused -= GameManager_GamePaused;
        GameResume -= GameManager_GameResume;

        ComboController.Unload();
    }

    private void GameManager_GameResume()
    {
        if (MenuController.Instance != null)
            MenuController.Instance.DisplayMenu("Game");

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        Time.timeScale = 1;
    }

    private void GameManager_GamePaused()
    {
        if (MenuController.Instance != null)
            MenuController.Instance.DisplayMenu("Pause");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0;
    }

    public void SetPause()
    {
        if (IsPaused)
            GameResume?.Invoke();
        else
            GamePaused?.Invoke();

        IsPaused = !IsPaused;
    }

    public void SetPause(bool pause)
    {
        IsPaused = pause;

        if (!IsPaused)
            GameResume?.Invoke();
        else
            GamePaused?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause();
        }
    }
}