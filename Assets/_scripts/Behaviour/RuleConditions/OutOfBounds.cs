using UnityEngine;

public class OutOfBounds : TaystaeBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == GameStrings.PLANET && !WinStatus) 
        {
            Lose();
            Destroy(collision.gameObject);
        }
    }
}
