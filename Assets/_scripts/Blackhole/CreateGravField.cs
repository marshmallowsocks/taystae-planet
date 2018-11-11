using UnityEngine;

public class CreateGravField : GameCore
{
    private bool isChained = false;
    public GameObject target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == GameStrings.PLANET && !isChained)
        {
            gameObject.GetComponent<RuntimeRope>().Create();
            isChained = true;
            target.GetComponent<TargetFadeAndDestroy>().TargetFadeDestroy();
        }
    }
}
