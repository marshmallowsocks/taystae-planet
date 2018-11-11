using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI_UpdateScore : TaystaeBehaviour {

    public Sprite fullStar;
    private static Sprite fullStarStatic;
    private string[] names = {
        "Star",
        "Star2",
        "Star3",
        "Star4"
    };

    private static Dictionary<int, Transform> editorNameToTransform;

    public void Start()
    {   
        editorNameToTransform = new Dictionary<int, Transform>();
        for (int i = 0; i < Values.STARS_PER_LEVEL[currentScene]; i++)
        {
            editorNameToTransform[i] = transform.Find(names[i]);
        }
        fullStarStatic = fullStar;
    }

    public static void UpdateScoreImage(int score)
    {
        Transform scoreStar = editorNameToTransform[score - 1];
        Image toUpdate = scoreStar.GetComponent<Image>();

        toUpdate.sprite = fullStarStatic;
    }
}
