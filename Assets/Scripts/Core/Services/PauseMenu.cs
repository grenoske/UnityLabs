using Core;
using Core.Services.Updater;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        GameLevelInitializer.onPause = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        GameLevelInitializer.onPause = true;
    }

    public void Restart()
    {
        GameLevelInitializer.onRestart = true;
    }
}
