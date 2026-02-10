using Unity.VisualScripting;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public static ThemeManager Instance {get; private set; }

    [SerializeField] private UITheme currentTheme;
    [SerializeField] private UITheme lightTheme;
    [SerializeField] private UITheme darkTheme;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void SetTheme(UITheme theme)
    {
        currentTheme = theme;
        //theme.background.a = 1f;
        foreach (var themeable in FindObjectsOfType<MonoBehaviour>(true))
        {
            if (themeable is IThemeable t)
                t.ApplyTheme(currentTheme);
        }
    }
    public UITheme CurrentTheme => currentTheme;

    public void ToggleTheme()
    {
        var next = currentTheme == lightTheme ? darkTheme : lightTheme;

        SetTheme(next);
    }
}
