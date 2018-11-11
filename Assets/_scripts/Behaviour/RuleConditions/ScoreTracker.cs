using UnityEngine;
using System.Collections;

public class ScoreTracker : GameCore
{
    static int score = 0;

    public static void UpdateScore()
    {
        score++;
        UI_UpdateScore.UpdateScoreImage(score);
    }

    public static void ResetScore()
    {
        score = 0;
    }
}
