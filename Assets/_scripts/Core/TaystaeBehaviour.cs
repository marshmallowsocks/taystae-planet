using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;

public class TaystaeBehaviour : GameCore
{
    public enum Gravity
    {
        NORMAL,
        REVERSED,
        ZERO
    };

    public enum BulletTime
    {
        ENABLED,
        DISABLED
    };

    private static bool winStatus;
    private static Gravity gravityStatus;
    private static BulletTime bulletTimeStatus;

    protected static bool isAscending;

    protected static int currentScene;

    protected bool WinStatus
    {
        get
        {
            return winStatus;
        }
    }

    protected Gravity GravityStatus
    {
        get
        {
            return gravityStatus;
        }
    }

    protected BulletTime BulletTimeStatus
    {
        get
        {
            return bulletTimeStatus;
        }
    }

    private void Start()
    {
        currentScene = Array.IndexOf(GameStrings.SCENES, SceneManager.GetActiveScene().name);
    }

    private void OnLevelWasLoaded(int level)
    {
        winStatus = false;
    }

    public string getNextSceneName()
    {
        currentScene++;
        if(currentScene > Values.MAX_SCENES)
        {
            // The game is over.
            // Use this area to display credits or whatnot.
            return "GameOver";
            //Application.Quit();
        }
        try
        {
            return GameStrings.SCENES[currentScene];
        }
        catch(IndexOutOfRangeException e)
        {
            return "GameOver";
        }
    }

    /// <summary>
    /// Call this method before the scene reloads. 
    /// Will reset score, gravity, victory status, and bullet time status.
    /// </summary>
    public void BeforeReload()
    {
        //Reset score
        ScoreTracker.ResetScore();
        //Reset gravity
        ToggleGravity(GameStrings.GRAVITY_RESET);
        //Reset bullet time
        ToggleBulletTime(GameStrings.STOP_BULLET_TIME_IMMEDIATE, null);
    }

    protected void ToggleGravity(string toggleStatus)
    {
        switch(toggleStatus)
        {
            case GameStrings.GRAVITY_REVERSE:
                Physics2D.gravity *= Values.IDENTITY_REVERSE;
                gravityStatus = Gravity.REVERSED;
                break;
            case GameStrings.GRAVITY_RESET:
                Physics2D.gravity = new Vector2(0, Values.GRAVITY_STANDARD);
                gravityStatus = Gravity.NORMAL;
                break;
            default:
                break;
        }

        SendMessage("__OnGravityChange", toggleStatus, SendMessageOptions.DontRequireReceiver);
    }

    protected void ToggleBulletTime(string toggleStatus, TextMeshProUGUI button)
    {
        switch(toggleStatus)
        {
            case GameStrings.START_BULLET_TIME_IMMEDIATE:
                _StartBulletTime(button);
                break;
            case GameStrings.START_BULLET_TIME:
                StartCoroutine(_StartBulletTimeWithDelay(button));
                break;
            case GameStrings.STOP_BULLET_TIME:
                StartCoroutine(_StopBulletTimeWithDelay());
                break;
            case GameStrings.STOP_BULLET_TIME_IMMEDIATE:
                Time.timeScale = Values.TIMESCALE_STANDARD;
                bulletTimeStatus = BulletTime.DISABLED;
                break;
        }

        SendMessage("__OnBulletTimeChange", toggleStatus, SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// This method gracefully loses the game.<para />
    /// <b>WARNING: </b> An exception will be thrown if this method is called after win in the same game.
    /// </summary>
    public void Lose()
    {
        if(winStatus)
        {
            return;
        }

        BeforeReload();
        StartCoroutine(_Lose());
    }

    public void Win(Animator eatAnimation, Animator screenTransition, Image fadeImage)
    {
        winStatus = true;
        BeforeReload();
        StartCoroutine(_Win(eatAnimation, screenTransition, fadeImage));
    }

    public void RestartLevel()
    {
        BeforeReload();
        winStatus = false;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void LowerVolume(AudioSource levelAudio)
    {
        
    }

    public void RaiseVolume(AudioSource levelAudio)
    {
        
    }

    private void AudioCheck(AudioSource levelAudio)
    {
        if(levelAudio == null)
        {
            
        }
    }

    private IEnumerator _Lose()
    {
        yield return new WaitForSeconds(Values.LOAD_MEDIUM_PAUSE);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    private IEnumerator _Win(Animator eatAnimation, Animator screenTransition, Image fadeImage)
    {
        eatAnimation.SetBool(GameStrings.EATING, true);

        yield return new WaitForSeconds(Values.LOAD_LONG_PAUSE);
        screenTransition.SetBool("Fade", true);
        yield return new WaitUntil(() => fadeImage.color.a == 1);
        SceneManager.LoadSceneAsync(getNextSceneName());
    }

    private void _StartBulletTime(TextMeshProUGUI button)
    {
        Time.timeScale = Values.TIMESCALE_BULLET_TIME;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        bulletTimeStatus = BulletTime.ENABLED;
        StartCoroutine(_BulletTimeLoop(button));
    }

    private IEnumerator _StartBulletTimeWithDelay(TextMeshProUGUI button)
    {
        while(true)
        {
            Time.timeScale -= Values.TIMESCALE_GRADUAL_DELTA;
            yield return new WaitForSeconds(Values.LOAD_SHORT_PAUSE);
            
            if (Time.timeScale < 0.6f)
            {
                Time.timeScale = Values.TIMESCALE_BULLET_TIME;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                bulletTimeStatus = BulletTime.ENABLED;
                StartCoroutine(_BulletTimeLoop(button));
                yield break;
            }
        }
    }

    private IEnumerator _BulletTimeLoop(TextMeshProUGUI button)
    {
        int timer = 5;

        while (timer > 0)
        {
            button.SetText(timer.ToString());
            timer--;
            yield return new WaitForSecondsRealtime(Values.IDENTITY);
        }

        StartCoroutine(_StopBulletTimeWithDelay());
        button.SetText(GameStrings.BULLET);
        yield break;
    }

    private IEnumerator _StopBulletTimeWithDelay()
    {
        while(true)
        {
            Time.timeScale += Values.TIMESCALE_GRADUAL_DELTA;
            yield return new WaitForSeconds(Values.LOAD_SHORT_PAUSE);
            
            if (Time.timeScale > 0.9f)
            {
                Time.timeScale = Values.TIMESCALE_STANDARD;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                bulletTimeStatus = BulletTime.DISABLED;
                yield break;
            }
        }
    }

    protected class TeleportManager : MonoBehaviour
    {
        public enum DispatchOptions
        {
            RequireProcessing,
            RequireImmediateProcessing,
            DiscardIfProcessing
        };

        private static List<DictionaryEntry> _teleportQueue = new List<DictionaryEntry>();
        private static List<DictionaryEntry> _teleportBackupQueue = new List<DictionaryEntry>();

        private static bool isProcessing;
        private object processLock = new object();
        
        private void DispatchRequireProcessing(GameObject teleportObject, Vector3 destination)
        {
            DictionaryEntry newEntry = new DictionaryEntry
            {
                Key = teleportObject,
                Value = destination
            };

            if (!isProcessing)
            {
                if (!_teleportQueue.Contains(newEntry))
                {
                    _teleportQueue.Add(newEntry);
                    StartCoroutine(_ProcessTeleportQueueAsync());
                }
            }
            else
            {
                if (!_teleportBackupQueue.Contains(newEntry))
                {
                    AddToBackupQueue(newEntry);
                }
            }
        }

        private void DispatchDiscardIfProcessing(GameObject teleportObject, Vector3 destination)
        {
            if(isProcessing)
            {
                return;
            }
            
            DispatchRequireProcessing(teleportObject, destination);
        }

        /// <summary>Do NOT use for 2 way teleports.<para />
        /// This method will infinitely cycle a two way teleport.<para />
        /// If the two way teleport must absolutely process, use
        /// the RequireProcessing option.<para /><see cref="DispatchRequireProcessing(GameObject, Vector3)"/></summary>
        private void DispatchRequireImmediateProcessing(GameObject teleportObject, Vector3 destination)
        {
            teleportObject.transform.position = destination;
        }

        private void DispatchRequireImmediateProcesingWithOrientation(GameObject teleportObject, GameObject destination)
        {
            //destination.GetComponent<PolygonCollider2D>().enabled = false;
            teleportObject.transform.position = destination.transform.position;
            teleportObject.GetComponent<Rigidbody2D>().velocity = destination.transform.up * teleportObject.GetComponent<Rigidbody2D>().velocity.magnitude;
        }

        /// <summary>
        /// Routes the teleport request to the right handler.
        /// Options: <para />
        /// RequireProcessing: Will add to the teleport queue;<para/>
        /// DiscardIfProcessing: Will reject the teleport if the manager is busy; <para/>
        /// RequireImmediateProcessing: Will process it immediately, bypassing the queue. <para />
        /// </summary>
        /// <param name="teleportObject"> The object to teleport.</param>
        /// <param name="destination"> Where to teleport the object to.</param>
        /// <param name="options"> Whether to discard, wait, or process immediately.</param>
        /// <param name="teleportGate"> OPTIONAL. Provide for ONE WAY TELEPORTS that have orientation.</param>

        public void Dispatch(GameObject teleportObject, Vector3 destination, DispatchOptions options, GameObject teleportGate=null)
        {
            if (teleportGate != null)
            {
                DispatchRequireImmediateProcesingWithOrientation(teleportObject, teleportGate);
            }
            else
            {
                switch (options)
                {
                    case DispatchOptions.DiscardIfProcessing:
                        DispatchDiscardIfProcessing(teleportObject, destination);
                        break;
                    case DispatchOptions.RequireProcessing:
                        DispatchRequireProcessing(teleportObject, destination);
                        break;
                    case DispatchOptions.RequireImmediateProcessing:
                        DispatchRequireImmediateProcessing(teleportObject, destination);
                        break;
                }
            }
        }

        private void AddToBackupQueue(DictionaryEntry entry)
        {
            lock(processLock)
            {
                _teleportBackupQueue.Add(entry);
            }
        }

        private void PickupWaitingTeleports()
        {
            lock (processLock)
            {
                if (_teleportBackupQueue.Count != 0)
                {
                    _teleportQueue.AddRange(_teleportBackupQueue);
                    _teleportBackupQueue.Clear();
                    StartCoroutine(_ProcessTeleportQueueAsync());
                }
            }
        }

        private IEnumerator _ProcessTeleportQueueAsync()
        {
            isProcessing = true;
            foreach(DictionaryEntry teleportObjectEntry in _teleportQueue)
            {
                GameObject go = (GameObject)teleportObjectEntry.Key;
                Vector3 position = (Vector3)teleportObjectEntry.Value;
                go.transform.position = position;
                yield return new WaitForSeconds(1f);
            }

            _teleportQueue.Clear();
            isProcessing = false;
            PickupWaitingTeleports();
            yield break;
        }
    }
}
