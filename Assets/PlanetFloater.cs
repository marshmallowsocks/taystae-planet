using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlanetFloater : TaystaeBehaviour {

    private bool isFloating;
    private GameObject planet;

	private void Start()
	{
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
	}

	public void Burst()
    {
        isFloating = false;
        isAscending = false;
        try
        {
            gameObject.GetComponent<FixedJoint2D>().connectedBody = null;
        }
        catch(Exception e)
        {
            //do nothing
        }
        try
        {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        catch(Exception e)
        {
            //do nothing
        }
        try
        {
            planet.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        catch(Exception e)
        {
            //do nothing
        }
            
        gameObject.SetActive(false);
    }

    private void BurstOnWin()
    {
        isFloating = false;
        isAscending = false;
        gameObject.GetComponent<FixedJoint2D>().connectedBody = null;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        gameObject.SetActive(false);
    }

    public void Float(GameObject planet)
    {
        FixedJoint2D glue = gameObject.AddComponent<FixedJoint2D>();
        this.planet = planet;

        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;

        glue.autoConfigureConnectedAnchor = false;
        glue.connectedBody = planet.GetComponent<Rigidbody2D>();
        glue.connectedAnchor = new Vector2(0.1f, 0.1f);

        isFloating = true;
        isAscending = true;

        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = -0.2f;
        gameObject.GetComponent<Rigidbody2D>().mass = planet.GetComponent<Rigidbody2D>().mass * 12.5f;
        planet.GetComponent<Rigidbody2D>().gravityScale = -0.2f;
    }

    private void Update()
    {
        if(WinStatus && isFloating)
        {
            BurstOnWin();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == GameStrings.PLANET)
        {
            Float(collision.gameObject);
        }

        if(collision.gameObject.name == GameStrings.ASTEROID && isFloating)
        {
            Burst();
            planet.SetActive(false);
            Lose();
        }

        if (collision.gameObject.name == GameStrings.CHAIN_SLICER && isFloating)
        {
            Burst();
        }
    }
}
