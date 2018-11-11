using UnityEngine;

public class FollowPlanet : TaystaeBehaviour
{
    // Script to track the planet with the camera
    // Only tracks the planet movement downwards
    // If the planet is floating in a FloatSpace,
    // isAscending is set to true forcing the camera
    // to track upwards.

    public Transform planet;
    public float upperBound;
    public float lowerBound;

    private Vector3 prevPosition;

    void Start()
    {
        prevPosition = transform.position;
    }

    void LateUpdate()
    {
        if (planet != null)
        {
            if (ShouldCameraUpdate())
            {
                transform.position = new Vector3(0, planet.transform.position.y, -10);
                prevPosition = transform.position;
            }
        }
    }

    private bool ShouldCameraUpdate()
    {
        bool isPlanetBelowUpperGameBounds = planet.position.y < upperBound;
        bool isPlanetAboveLowerGameBounds = planet.position.y > lowerBound;
        bool isPlanetDescending = planet.position.y - prevPosition.y <= 0;
        bool isPlanetAscending = isAscending || planet.transform.position.y - prevPosition.y > 5;
        return (isPlanetBelowUpperGameBounds && isPlanetAboveLowerGameBounds && (isPlanetDescending || isPlanetAscending));
    }
}