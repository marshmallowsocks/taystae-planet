public static class Values
{
    //misc
    public const float IDENTITY = 1f;
    public const float IDENTITY_REVERSE = -1f;

    //fade
    public const float FADE_OUT_DURATION = 0.2f;

    //movable
    public const float MOVABLE_PING_PONG_TIME = 2.5f;


    //gravity
    public const float GRAVITY_STANDARD = -9.81f;
    public const float GRAVITY_REVERSED = 9.81f;
    public const float GRAVITY_ZERO = 0f;

    //timescale
    public const float TIMESCALE_STANDARD = 1f;
    public const float TIMESCALE_BULLET_TIME = 0.5f;
    public const float TIMESCALE_GRADUAL_DELTA = 0.05f;

    //time values
    public const float LOAD_LONG_PAUSE = 2.5f;
    public const float LOAD_MEDIUM_PAUSE = 0.8f;
    public const float LOAD_SHORT_PAUSE = 0.1f;

    //Cloud values
    public const int CLOUD_BOUNCE_FORCE = 500;
    public const float CLOUD_MAX_SIZE = 0.4f;
    public const float CLOUD_SHRINK_FACTOR = 0.9f;

    //Asteroid values
    public const int ASTEROID_ROTATION_SPEED = 50;

    //Game values
    public const int MAX_SCENES = 5; // 6 scenes, 0 - 5

    public static int[] STARS_PER_LEVEL = {
        4,
        3,
        3,
        3,
        3,
        4
    };
}
