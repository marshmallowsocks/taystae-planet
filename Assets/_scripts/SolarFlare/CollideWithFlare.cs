using UnityEngine;

public class CollideWithFlare : TaystaeBehaviour
{
    private void Start()
    {
        Type = EntityType.SOLAR_FLARE;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == GameStrings.PLANET && !WinStatus)
        {
            Lose();
            Destroy(collision.gameObject);
        }
    }
}