public static class GameStrings
{
    public const string LOSE_TEXT = "You lose";
    public const string WIN_TEXT = "You win";

    // GameObject names
    public const string PLANET = "Planet";
    public const string ASTEROID = "Asteroid";
    public const string GAME_MUSIC = "GameMusic";
    public const string BULLET = "Bullet";
    public const string CHAIN_SLICER = "ChainSlicer";
    public const string BULLET_TIME_START_AUDIO = "BulletTimeAudio-Start";
    public const string BULLET_TIME_STOP_AUDIO = "BulletTimeAudio-Stop";
    public const string FLOATSPACE = "FloatSpace";

    // Scene names
    public const string SCENE_LEVEL_ZERO = "LevelZero";
    public const string SCENE_LEVEL_ONE = "Level1";
    public const string SCENE_LEVEL_TWO = "Level2";
    public const string SCENE_LEVEL_THREE = "Level3";
    public const string SCENE_LEVEL_FOUR = "Level4";
    public const string SCENE_LEVEL_FIVE = "Level5";

    public static readonly string[] SCENES =
    {
        SCENE_LEVEL_ONE,
        SCENE_LEVEL_TWO,
        SCENE_LEVEL_THREE,
        SCENE_LEVEL_FOUR,
        SCENE_LEVEL_FIVE,
        SCENE_LEVEL_ZERO
    };

    // Gravity
    public const string GRAVITY_REVERSE = "gravity_reverse";
    public const string GRAVITY_RESET = "gravity_reset";

    // Bullet time
    public const string START_BULLET_TIME = "start_bullet_time";
    public const string STOP_BULLET_TIME = "stop_bullet_time";
    public const string START_BULLET_TIME_IMMEDIATE = "start_bullet_time_immediate";
    public const string STOP_BULLET_TIME_IMMEDIATE = "stop_bullet_time_immediate";

    // Logger
    public const string WTF = "wtf";
    public const string WARNING = "warning";
    public const string INFO = "info";
    public const string NOT_AVAILABLE = "UNAVAILABLE";
    public const string LOG_STRING = "{0}.{1} -> {2} (From {3}.{4})";

    // Exceptions
    public const string NON_FATAL = "A non fatal exception occured.";
    public const string FATAL = "A fatal exception occured.";
    public const string ON_NOTIFY_NOT_IMPLEMENTED = "OnNotify must be implemented to use subscriber services";

    // Warnings
    public const string PROPERTY_UNSUPPORTED = "{0} does not support the change to property {1}";
    
    // Animations
    public const string PAUSE = "PauseMenuSlideIn";
    public const string UNPAUSE = "PauseMenuSlideOut";
    public const string IDLE = "Idle";
    public const string PRE_EATING = "PreEating";
    public const string EATING = "Eating";
}
