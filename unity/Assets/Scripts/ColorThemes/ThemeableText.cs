using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThemeableText : MonoBehaviour, IThemeable
{
    [SerializeField] private TMP_Text text;

    private void Awake()
    {
        if (text == null)
            text = GetComponent<TMP_Text>();
    }

    public void ApplyTheme(UITheme theme)
    {
        text.color = theme.text;
    }
}

// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class ThemeableText : MonoBehaviour, IThemeable
// {
//     [SerializeField] private TMP_Text text;

//     private void Awake()
//     {
//         if (text == null)
//             text = GetComponent<TMP_Text>();
//     }

//     private void OnEnable()
//     {
//         ThemeManager.Instance?.Register(this);
//         ApplyTheme(ThemeManager.Instance.CurrentTheme);
//     }

//     private void OnDisable()
//     {
//         ThemeManager.Instance?.Unregister(this);
//     }

//     public void ApplyTheme(UITheme theme)
//     {
//         if (text != null)
//             text.color = theme.text;
//     }
// }
