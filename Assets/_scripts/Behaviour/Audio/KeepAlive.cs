using UnityEngine;

public class KeepAlive : GameCore
{
    private GameObject[] gameMusic;

    // Use this for initialization
    void Start()
    {
        this.Type = EntityType.AUDIO;

        gameMusic = GameObject.FindGameObjectsWithTag(GameStrings.GAME_MUSIC);
        if (gameMusic.Length == 1)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
        else
        {
            for (int i = 1; i < gameMusic.Length; i++)
            {
                Destroy(gameMusic[i]);
            }
        }
    }

    void Awake()
    {
        //Keep game music playing
        DontDestroyOnLoad(gameObject);
    }
}
