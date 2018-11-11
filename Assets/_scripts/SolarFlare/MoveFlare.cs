using UnityEngine;
using System.Collections;

public class MoveFlare : Movable
{
    private bool canMove = false;

    private static float waitTime;
    private static bool isWaitTimeInitialized = false;
    private static float timeFromLastInitialize = -100f;

    // Use this for initialization
    void Start()
    {
        moveType = MoveType.ONE_WAY;
        startPosition = from = transform.Find("startPos").position;
        to = transform.Find("endPos").position;

        journeyLength = Vector3.Distance(from, to);
        Wait();
    }

    private void InitializeWaitTime()
    {
        if(!isWaitTimeInitialized && Time.time - timeFromLastInitialize > 3f)
        {
            waitTime = Random.Range(0f, 5f);
            //Logger.Info("Interval is now " + waitTime);
            timeFromLastInitialize = Time.time;
            isWaitTimeInitialized = true;
        }
    }

    private void Wait()
    {
        InitializeWaitTime();
        StartCoroutine(_WaitSeconds());
    }

    IEnumerator _WaitSeconds()
    {
        canMove = false;
        yield return new WaitForSeconds(waitTime);
        startTime = Time.time;
        canMove = true;
    }

    protected override void __AfterMove()
    {
        if (completedDelta >= 1f)
        {
            completedDelta = 0f;
            transform.position = startPosition;
            startTime = Time.time;
            Wait();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            Move();
            isWaitTimeInitialized = false;
        }
    }
}

