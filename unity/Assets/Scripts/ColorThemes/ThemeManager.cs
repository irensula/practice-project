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
// using Unity.VisualScripting;
// using UnityEngine;
// using System.Collections.Generic;

// public class ThemeManager : MonoBehaviour
// {
//     public static ThemeManager Instance {get; private set; }

//     [SerializeField] private UITheme currentTheme;
//     [SerializeField] private UITheme lightTheme;
//     [SerializeField] private UITheme darkTheme;
//     private List<IThemeable> themeables = new List<IThemeable>();

//     private void Start()
//     {
//         SetTheme(currentTheme);
//     }

//     private void Awake()
//     {
//         if (Instance != null)
//         {
//             Destroy(gameObject);
//             return;
//         }
//         Instance = this;
//         DontDestroyOnLoad(gameObject);
//     }

//     public void Register(IThemeable themeable)
//     {
//         themeables.Add(themeable);

//         if (currentTheme != null)
//             themeable.ApplyTheme(currentTheme);
//     }

//     public void Unregister(IThemeable themeable)
//     {
//         themeables.Remove(themeable);
//     }
    
//     public void SetTheme(UITheme theme)
//     {
//         currentTheme = theme;
        
//         foreach (var t in themeables)
//         {
//             t.ApplyTheme(currentTheme);
//         }
//     }
//     public UITheme CurrentTheme => currentTheme;

//     public void ToggleTheme()
//     {
//         var next = currentTheme == lightTheme ? darkTheme : lightTheme;

//         SetTheme(next);
//     }
// }
