using UnityEngine;

public class MenuState : MonoBehaviour
{
    public enum PanelType
    {
        Language,
        MainMenu,
        Courses,
        Lessons,
        Options
    }
    // The panel that will open after loading MainMenuScene
    public static PanelType? PanelToOpen = null;

    public static PanelLevel? CurrentLevel = null;

    public static void SetLevel(PanelLevel level)
    {
        CurrentLevel = level;
    }

    public static PanelLevel? GetLevel()
    {
        return CurrentLevel;
    }

    public static void Clear()
    {
        PanelToOpen = null;
        CurrentLevel = null;
    }

    public enum PanelLevel
    {
        Language = 0,
        MainMenu = 1,
        Courses = 2,
        Lessons = 3
    }
}
