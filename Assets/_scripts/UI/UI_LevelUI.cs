using UnityEngine;
using TMPro;

public class UI_LevelUI : TaystaeBehaviour
{
    public TextMeshProUGUI bulletTimeButtonText;
    public GameObject UIObject;

    private bool isBulletTimeActive = false;
    

    public void OnClickReverseGravity()
    {
        ToggleGravity(GameStrings.GRAVITY_REVERSE);
    }

    public void OnClickRestartLevel()
    {
        RestartLevel();
    }

    public void OnBulletTimeClick()
    {
        Transform audioSource;
        ToggleBulletTime(!isBulletTimeActive ? GameStrings.START_BULLET_TIME_IMMEDIATE : GameStrings.STOP_BULLET_TIME, bulletTimeButtonText);
        audioSource = transform.Find(!isBulletTimeActive ? GameStrings.BULLET_TIME_START_AUDIO : GameStrings.BULLET_TIME_STOP_AUDIO);
        if (audioSource != null)
        {
            audioSource.GetComponent<AudioSource>().Play();
        }
        else
        {
            //Logger.WTF("Could not find usable audiosource", WTFException.Severity.NON_FATAL);
        }
        isBulletTimeActive = !isBulletTimeActive;
    }

    public void __OnGamePause(bool isPaused)
    {
        //Logger.Info("__OnGamePause called!");
        UIObject.SetActive(!isPaused);
    }
}
