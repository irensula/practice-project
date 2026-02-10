using UnityEngine;

[CreateAssetMenu(fileName = "UITheme", 
menuName = "UI/Theme")]
public class UITheme : ScriptableObject
{
    public Color background;
    // public Color surface;
    // public Color primary;
    public Color textPrimary;
    // public Color textSecondary;
}

public interface IThemeable
{
    void ApplyTheme(UITheme theme);
}