using UnityEngine;

public class Config
{
    // IO
    public const float MOUSE_SENSITIVE = 2f;

    // Interaction
    public const float INTERACTION_RANGE = 8f;

    // Color
    public static Color INTERACTION_COLOR_DEFAULT = new Color(255f,255f,255f);
    public static Color INTERACTION_COLOR_SELECTED = new Color(0f,200f,0f);

    // Layer Index
    public const int LAYER_INDEX_INTERACTABLE = 8;
}
