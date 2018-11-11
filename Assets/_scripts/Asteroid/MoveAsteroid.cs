using UnityEngine;

public class MoveAsteroid : Movable
{
    public Transform toPosition;
    
	// Use this for initialization
	void Start ()
    {
        moveType = MoveType.PING_PONG;
        from = transform.position;
        to = toPosition.position;
        this.Type = EntityType.ASTEROID;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Move();
        transform.Rotate(Vector3.back, Values.ASTEROID_ROTATION_SPEED * Time.deltaTime);
	}
}
