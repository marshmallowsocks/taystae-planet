using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Revolve : GameCore {

	// Update is called once per frame
	void Update () {
        transform.RotateAround(transform.parent.transform.position, Vector3.forward, Time.deltaTime * 50);
	}
}
