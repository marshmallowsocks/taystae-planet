using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MoveShip : GameCore {
    float maxX = 4f;
    float minX = -4f;
    float maxY = 4.2f;
    float minY = -4.2f;

    private float tChange = 0; // force new direction in the first Update
    private float randomX;
    private float randomY;
 
    private void Update()
    {
        // change to random direction at random intervals
        if (Time.time >= tChange)
        {
            randomX = Random.Range(-2.0f, 2.0f); // with float parameters, a random float
            randomY = Random.Range(-2.0f, 2.0f); //  between -2.0 and 2.0 is returned
                                               // set a random interval between 0.5 and 1.5
            tChange = Time.time + Random.Range(0.5f, 1.5f);
        }
        Vector3 newPoint = new Vector3(randomX, randomY, 0);
        transform.Translate(newPoint * Time.deltaTime);
        // if object reached any border, revert the appropriate direction
        if (transform.position.x >= maxX || transform.position.x <= minX)
        {
            randomX = -randomX;
        }
        if (transform.position.y >= maxY || transform.position.y <= minY)
        {
            randomY = -randomY;
        }

        // make sure the position is inside the borders
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY)
        );
    }
}
