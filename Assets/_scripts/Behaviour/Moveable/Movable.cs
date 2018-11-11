using UnityEngine;

public abstract class Movable : TaystaeBehaviour
{
    protected enum MoveType
    {
        ONE_WAY,
        PING_PONG
    };

    //PING PONG
    protected float secondsForOneLength = Values.MOVABLE_PING_PONG_TIME;

    //ONE WAY
    protected float journeyLength;
    protected float startTime;
    protected float completedDelta;

    //Moveable
    protected Vector3 from;
    protected Vector3 to;
    protected Vector3 startPosition;

    protected MoveType moveType;

    private void _PingPong()
    {
        transform.position = Vector3.Lerp(
            from,
            to,
            Mathf.SmoothStep(
                0f, 
                1f, 
                Mathf.PingPong(Time.time / secondsForOneLength, 1f)
            )
        );
    }

    private void _OneWay()
    {
        float distCovered = (Time.time - startTime) * 2;
        completedDelta = distCovered / journeyLength;
        transform.position = Vector3.Lerp(from, to, completedDelta);
    }

    protected virtual void __BeforeMove()
    {

    }

    protected virtual void __AfterMove()
    {

    }

    protected virtual void Move()
    {
        __BeforeMove();
        switch(moveType)
        {
            case MoveType.ONE_WAY:
                _OneWay();
                break;
            case MoveType.PING_PONG:
                _PingPong();
                break;
            default:
                break;
        }
        __AfterMove();
    }
}
