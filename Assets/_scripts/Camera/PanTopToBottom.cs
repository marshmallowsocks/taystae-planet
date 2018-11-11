using UnityEngine;

public class PanTopToBottom : GameCore
{
    public float startY;
    public float stopY;
    private float speed;

    private void Start()
    {
        gameObject.transform.position = new Vector3(0, startY, -10);
        speed = 1.1f;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            speed = 11f;
        }
    }

    private void FixedUpdate()
    {
        gameObject.transform.position += Vector3.down * speed * Time.deltaTime;
        if (gameObject.transform.position.y < stopY)
        {
            gameObject.transform.position = new Vector3(0, stopY, -10);
            gameObject.GetComponent<FollowPlanet>().enabled = true;
            enabled = false;
        }
    }
}
