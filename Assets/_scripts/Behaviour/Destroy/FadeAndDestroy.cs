using UnityEngine;

public class FadeAndDestroy : GameCore
{
    private bool isFading = false;
    private float fadeValue = Values.IDENTITY;
    private float currentTime = 0f;
    protected const float fadeOutDuration = Values.FADE_OUT_DURATION;

    // Update is called once per frame
    void Update()
    {
        if (isFading)
        {
            currentTime += Time.deltaTime;

            if (currentTime <= fadeOutDuration)
            {
                fadeValue = Values.IDENTITY - (currentTime / fadeOutDuration);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, fadeValue);
            }
            else
            {
                fadeValue = Values.IDENTITY;
                currentTime = 0f;
                Destroy(gameObject);
            }
        }
    }

    protected void _FadeDestroy()
    {
        isFading = true;
        //Logger.Info("is true");
    }
}
