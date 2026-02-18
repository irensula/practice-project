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
            Debug.LogWarning("ThemeManager.Instance is not ready yet!", this);
        }
    }

    // Update is called once per frame
    public void ApplyTheme(UITheme theme)
    {
        if (image != null)
        {
            image.color = theme.primary;
        }
        else
            Debug.LogWarning("Image not assigned!", this);
    }
}
// using UnityEngine;
// using UnityEngine.UI;

// public class ThemeableBackground : MonoBehaviour, IThemeable
// {
//     [SerializeField] private Image image;

//     private void Awake()
//     {
//         if (image == null)
//             image = GetComponent<Image>();

//         Debug.Log("Image assigned: " + (image != null));

//     }

//     private void OnEnable()
//     {
//         ThemeManager.Instance?.Register(this);
//     }

//     private void OnDisable()
//     {
//         ThemeManager.Instance?.Unregister(this);
//     }

//     public void ApplyTheme(UITheme theme)
//     {
//         if (image != null)
//             image.color = theme.primary;

//         Debug.Log("Registering themeable: " + this.name, this);
//         Debug.Log("Applying theme: " + ThemeManager.Instance.CurrentTheme.primary);
//     }
// } 