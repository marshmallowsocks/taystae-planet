using UnityEngine;
using UnityEngine.UI;

public class FinishLevel : TaystaeBehaviour
{
    public Animator screenTransition;
    public Animator eatAnimation;
    public Image fadeImage;

    public bool isPreEat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPreEat)
        {
            if (collision.gameObject.name == GameStrings.PLANET)
            {
                eatAnimation.SetBool(GameStrings.IDLE, false);
                Win(eatAnimation, screenTransition, fadeImage);
                Destroy(collision.gameObject);
            }
        }

        else 
        {
            if (collision.gameObject.name == GameStrings.PLANET)
            {
                ActivateState(GameStrings.PRE_EATING);
            }
        }
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
        if(isPreEat)
        {
            if(collision.gameObject.name == GameStrings.PLANET && !WinStatus)
            {
                ActivateState(GameStrings.IDLE);
            }
        }
	}

    private void ActivateState(string stateName)
    {
        switch(stateName)
        {
            case GameStrings.PRE_EATING:

                eatAnimation.SetBool(GameStrings.EATING, false);
                eatAnimation.SetBool(GameStrings.IDLE, false);
                eatAnimation.SetBool(GameStrings.PRE_EATING, true);
                break;
            case GameStrings.EATING:
                eatAnimation.SetBool(GameStrings.PRE_EATING, false);
                eatAnimation.SetBool(GameStrings.IDLE, false);
                eatAnimation.SetBool(GameStrings.EATING, true);
                break;
            case GameStrings.IDLE:
                eatAnimation.SetBool(GameStrings.EATING, false);
                eatAnimation.SetBool(GameStrings.PRE_EATING, false);
                eatAnimation.SetBool(GameStrings.IDLE, true);
                break;
        }
    }
}
