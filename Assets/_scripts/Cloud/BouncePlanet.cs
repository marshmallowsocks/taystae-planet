using System.Collections;
using UnityEngine;

public class BouncePlanet : GameCore
{
    public float maxSize;
    public float shrinkFactor;
    public int force;

    private void Start()
    {
        maxSize = Values.CLOUD_MAX_SIZE;
        shrinkFactor = Values.CLOUD_SHRINK_FACTOR;
        force = Values.CLOUD_BOUNCE_FORCE;
    }

    IEnumerator _Scale()
    {
        float timer = 0;

        while ((Values.CLOUD_SHRINK_FACTOR * Values.CLOUD_MAX_SIZE) < transform.localScale.y)
        {
            timer += Time.deltaTime;
            transform.localScale -= Vector3.up * Time.deltaTime * shrinkFactor;
            yield return null;
        }
        
        // reset the timer
        yield return null;
        timer = 0;

        while (maxSize > transform.localScale.y)
        {
            timer += Time.deltaTime;
            transform.localScale += Vector3.up * Time.deltaTime * shrinkFactor;
            yield return null;
        }

        timer = 0;
        yield return null;
    }

    protected void __OnBulletTimeChange(string bulletTimeStatus)
    {
        if(bulletTimeStatus.Equals(GameStrings.START_BULLET_TIME))
        {
            //Logger.Warn("SINGLE FORCE WARNING: cloud does not support bullet time.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == GameStrings.PLANET)
        {
            //Logger.Info("Colliding with " + collision.gameObject.name);
            StartCoroutine(_Scale());
            Rigidbody2D body = collision.gameObject.GetComponent<Rigidbody2D>();
            body.AddForce(force * transform.up);
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
