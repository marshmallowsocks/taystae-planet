using UnityEngine;

public class BonusCollector : Movable
{
    public bool shouldMove;
    public bool isObstructed; // if the bonus is obstructed by a different collider, aberrant behavior might occur.
    public Transform toPosition;
    public GameObject audioClipContainer;

    private void Start()
    {
        if (shouldMove)
        {
            moveType = MoveType.PING_PONG;
            from = transform.position;
            to = toPosition.position;
        }
    }

    private void FixedUpdate()
    {
        if(shouldMove)
        {
            Move();
        }
        gameObject.transform.Rotate(Vector3.up, Time.deltaTime * 50);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == GameStrings.PLANET || (collision.gameObject.tag == GameStrings.FLOATSPACE && isAscending && !isObstructed))
        {
            audioClipContainer.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
            ScoreTracker.UpdateScore();
        }
    }
}
