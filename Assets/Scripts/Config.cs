using UnityEngine;

public class Config
{
    // IO
    public const float MOUSE_SENSITIVE = 2f;

    // Interaction
    public const float INTERACTION_RANGE_FPS = 8f;
    public const float INTERACTION_RANGE_TPS = 15f;

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
    public const float MODIFIER_SPRITE = 1.5f;
    public const float MODIFIER_ROTATION_SPEED_SMOOTH = 3f;
    public const float MODIFIER_CONSTRUCTION_1 = 0.0002f;
    public const float MODIFIER_CONSTRUCTION_2 = 0.0003f;
    public const float MODIFIER_CONSTRUCTION_3 = 0.0005f;
    public const float MODIFIER_AMPLIFIER_FUND_1 = 1f;
    public const float MODIFIER_AMPLIFIER_FUND_2 = 0.4f;
    public const float MODIFIER_AMPLIFIER_FUND_3 = 0.5f;
    public const float MODIFIER_AMPLIFIER_PERSONNEL_1 = 0.2f;
    public const float MODIFIER_AMPLIFIER_PERSONNEL_2 = 2f;
    public const float MODIFIER_AMPLIFIER_PERSONNEL_3 = 1f;
    public const float MODIFIER_GUN_FIRE_ALPHA = 0.5f;
    public const float MODIFIER_GUN_SHOOT_SLOWDOWN = 0.5f;
    public const float MODIFIER_GUN_AIM_SLOWDOWN = 0.5f;

    // Offset
    public const float OFFSET_CAM_FPS_TO_TPS_X = -0.8f;
    public const float OFFSET_CAM_FPS_TO_TPS_Y = 0.8f;
    public const float OFFSET_CAM_FPS_TO_TPS_Z = -5f;
    public const float OFFSET_GUN_SWITCH_X = 0.35f;
    public const float OFFSET_GUN_SWITCH_Y = 2.13f;
    public const float OFFSET_GUN_SWITCH_Z = -2.61f;
    public const float OFFSET_GUN_SWITCH_ROTATION_X = -86.45f;
    public const float OFFSET_FPS_MID_AIM_X = -0.242f;
    public const float OFFSET_FPS_MID_AIM_Y = 0.033f;
    public const float OFFSET_FPS_MID_AIM_Z = -0.167f;
    public const float OFFSET_FPS_MID_AIM_ZOOM = 20f;

    // Physics
    public const float PHYSICS_SHELL_BOUNCE_FORCE = 3f;
    public const float PHYSICS_GUN_SHOOT_RECOIL_RESTORE = 1.5f;
    public const float PHYSICS_CAM_SHOOT_RECOIL = 0.8f;

    // Time
    public const float TIME_ONEDAY = 360f;
    public const float TIME_DAY_UPDATE_DELAY = 1f;
    public const float TIME_DAY_TRANSITION_OFFSET = -25f;
    public const float TIME_ANIMATION_TRANSITION_FRAME = 0.02f;
    public const float TIME_ANIMATION_TRANSITION_DELAY = 1f;
    public const float TIME_ANIMATION_TRANSCAM = 5f;
    public const float TIME_ANIMATION_MISSION_COMPLETE_POPUP = 1f;
    public const float TIME_SUSTAIN_MISSION_COMPLETE_POPUP = 2f;
    public const float TIME_MISSION_TIME_1 = 20f;
    public const float TIME_MISSION_TIME_2 = 160f;
    public const float TIME_MISSION_TIME_3 = 260f;
    public const float TIME_MISSION_TIME_4 = 150f;
    public const float TIME_GUN_SHOOT_INTERVAL = 0.1f;
    public const float TIME_GUN_EMPTY_INTERVAL = 0.3f;
    public const float TIME_GUN_RELOAD = 3f;
    public const float TIME_CAM_SHOOT_RECOIL_TIMESTEP = 5f;
    public const float TIME_SHELL_LAST = 3f;
    public const float TIME_FPS_MID_AIM_ZOOM_TIMESTEP = 0.1f;
    public const float TIME_COLOR_DECREASE = 0.6f;

    // String
    public const string STRING_CONSTRUCTION_NAME_1 = "Water Treatment";
    public const string STRING_CONSTRUCTION_REWARD_1 = "Water Growth Rate +5";
    public const string STRING_CONSTRUCTION_NAME_2 = "Transportation Construction";
    public const string STRING_CONSTRUCTION_REWARD_2 = "Food Growth Rate +5   Fund Growth Rate +200";
    public const string STRING_CONSTRUCTION_NAME_3 = "Housing Repair";
    public const string STRING_CONSTRUCTION_REWARD_3 = "Troop Growth Rate +50";
    public const string STRING_MISSION_NAME_1 = "Bandit Rush";
    public const string STRING_MISSION_COST_1 = "Food +5";
    public const string STRING_MISSION_REWARD_1 = "Food +5";
    public const string STRING_MISSION_NAME_2 = "Hospital";
    public const string STRING_MISSION_REWARD_2 = "Water Growth Rate +5";
    public const string STRING_MISSION_COST_2 = "Food +5";
    public const string STRING_MISSION_NAME_3 = "Vender";
    public const string STRING_MISSION_REWARD_3 = "Water Growth Rate +5";
    public const string STRING_MISSION_COST_3 = "Food +5";
    public const string STRING_MISSION_NAME_4 = "Mom's Concern";
    public const string STRING_MISSION_REWARD_4 = "Water Growth Rate +5";
    public const string STRING_MISSION_COST_4 = "Food +5";

    // Default Setting
    public const float DEFAULT_INTI_MAIN_CAM_VIEW = 60;
    public const float DEFAULT_INTI_FOOD = 10;
    public const float DEFAULT_INTI_WATER = 5;
    public const float DEFAULT_INTI_FUND = 1000;
    public const float DEFAULT_INTI_TROOP = 200;
    public const float DEFAULT_GROWTH_FOOD = 10;
    public const float DEFAULT_GROWTH_WATER = 5;
    public const float DEFAULT_GROWTH_FUND = 500;
    public const float DEFAULT_GROWTH_TROOP = 100;
    public const float DEFAULT_REWARD_WATER_PROJECT_1 = 5;
    public const float DEFAULT_REWARD_FOOD_PROJECT_2 = 5;
    public const float DEFAULT_REWARD_FUND_PROJECT_2 = 200;
    public const float DEFAULT_REWARD_TROOP_PROJECT_3 = 50;
    public static int[] DEFAULT_MAX_ONE_CLIP = new int[] { 30 };
    public static int[] DEFAULT_MAX_TOTAL_AMMO = new int[] { 300 };
}
