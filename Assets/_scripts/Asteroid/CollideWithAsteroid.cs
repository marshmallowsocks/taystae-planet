using UnityEngine;

public class CollideWithAsteroid : TaystaeBehaviour
{
    private void Start()
    {
        this.Type = EntityType.ASTEROID;
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
