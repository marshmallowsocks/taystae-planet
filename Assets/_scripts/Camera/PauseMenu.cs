using UnityEngine;
using System.Collections;

public class PauseMenu : TaystaeBehaviour
{
    //Pause game is not part of Taystae Behaviour, it's easier to manage this way.

    public GameObject pauseMenuPanel;
    public AudioSource levelAudio;
    public GameObject gameUI;

    private Animator anim;
    //isPaused is a convenience field to build upon.
    //currently has no purpose.
    private bool isPaused = false;

    void Start()
    {
        Time.timeScale = 1;
        anim = pauseMenuPanel.GetComponent<Animator>();
        gameUI = GameObject.Find("Top");
        anim.enabled = false;
    }

    public void PauseGame()
    {
        LowerVolume(levelAudio);
        anim.enabled = true;
        anim.Play(GameStrings.PAUSE);
        isPaused = true;
        gameUI.SetActive(false);
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        RaiseVolume(levelAudio);
        isPaused = false;
        gameUI.SetActive(true);
        anim.Play(GameStrings.UNPAUSE);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}