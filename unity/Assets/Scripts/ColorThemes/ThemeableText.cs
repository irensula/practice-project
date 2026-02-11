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
        text.color = theme.textPrimary;
    }
}
