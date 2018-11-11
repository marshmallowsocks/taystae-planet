using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWormhole : GameCore {

    private Vector2 scale;
    public float amplitude = 0.25f;

    void Start()
    {
        scale = Vector2.zero;
    }

	// Update is called once per frame
	void Update () {
        
        for (int i = 0; i < 2; i++)
        {
            scale[i] = amplitude * Mathf.Sin(2 * Time.time) + 1;
        }

        gameObject.transform.localScale = new Vector2(scale[0] * 0.25f, scale[1] * 0.25f);
	}
}
