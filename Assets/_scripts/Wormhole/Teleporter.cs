using UnityEngine;

public class Teleporter : TaystaeBehaviour
{
    public Transform toPosition;
    public GameObject audioClipContainer;
    public bool isOriented;

    private TeleportManager teleport;

    private void Start()
    {
        teleport = teleport ?? gameObject.AddComponent<TeleportManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == GameStrings.PLANET)
        {
            PolygonCollider2D destinationCollider = toPosition.GetComponent<PolygonCollider2D>();
            audioClipContainer.GetComponent<AudioSource>().Play();
            //Very VERY bad idea
            //Will target ALL chains, not just connected ones.
            //TODO: change this shit
            var chains = FindObjectsOfType<ChainLinkFadeAndDestroy>();
            foreach(var chain in chains)
            {
                chain.ChainLinkFadeDestroy();
            }
            if(isOriented)
            {
                //Logger.Info("An oriented teleport was requested!");
                teleport.Dispatch(
                    collision.gameObject,
                    toPosition.position,
                    TeleportManager.DispatchOptions.DiscardIfProcessing,
                    toPosition.gameObject
                );
            }
            else
            {
                teleport.Dispatch(
                    collision.gameObject,
                    toPosition.position,
                    TeleportManager.DispatchOptions.DiscardIfProcessing
                );
            }
            
        }
    }
}