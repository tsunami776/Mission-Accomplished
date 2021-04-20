using UnityEngine;

public class Config
{
    // IO
    public const float MOUSE_SENSITIVE = 2f;

    // Interaction
    public const float INTERACTION_RANGE = 8f;

    // Color
    public static Color32 INTERACTION_COLOR_DEFAULT = new Color32(255, 255, 255, 255);
    public static Color32 INTERACTION_COLOR_SELECTED = new Color32(0, 200, 0, 255);
    public static Color32 MISSION_COLOR_UNLOCKED = new Color32(136, 255, 120, 255);
    public static Color32 MISSION_COLOR_ONGOING = new Color32(255, 230, 111, 255);
    public static Color32 MISSION_COLOR_COMPLETED = new Color32(17, 142, 0, 255);
    public static Color32 MISSION_COLOR_FAILED = new Color32(176, 33, 33, 255);

    // Layer Index
    public const int LAYER_INDEX_INTERACTABLE = 8;

    // Modifier
    public const float MODIFIER_TIMER = 1f;

    // Time
    public const float TIME_ONEDAY = 180f;
    public const float TIME_DAY_UPDATE_DELAY = 1f;
}
