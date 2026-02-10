using UnityEngine;
using UnityEngine.UI;

public class ThemeableBackground : MonoBehaviour, IThemeable
{
    [SerializeField] private Image image;

    private void Start()
    {
    if (image == null)
        image = GetComponent<Image>();

    if (ThemeManager.Instance != null)
        {
            ApplyTheme(ThemeManager.Instance.CurrentTheme);
        } 
        else
        {
            Debug.LogWarning("ThemeManager.Instance ещё не готов!", this);
        }
    }

    // Update is called once per frame
    public void ApplyTheme(UITheme theme)
    {
        if (image != null)
        {
            image.color = theme.background;
            Debug.Log("ApplyTheme: color = " + theme.background, this);
        }
        else
            Debug.LogWarning("Image не присвоен!", this);
    }
} 