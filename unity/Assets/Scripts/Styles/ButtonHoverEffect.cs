using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtonHoverHover : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Button button;

    [Header("Text Colors")]
    [SerializeField] private Color normalColor = new Color32(41, 40, 46, 255);
    [SerializeField] private Color hoverColor = new Color32(255, 140, 0, 255);
    [SerializeField] private Color disabledColor = new Color32(41, 40, 46, 100);

    [Header("Scale Settings")]
    [SerializeField] private float hoverScale = 1.08f;
    [SerializeField] private float scaleSpeed = 10f;

    private Vector3 originalScale;
    private Vector3 targetScale;

    private void Awake()
    {
        if (buttonText == null)
            buttonText = GetComponentInChildren<TextMeshProUGUI>();

        if (button == null)
            button = GetComponent<Button>();

        originalScale = transform.localScale;
        targetScale = originalScale;

        normalColor = buttonText.color;
    }

    private void Update()
    {
        // smooth scale
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * scaleSpeed
        );

        // color for disabled
        if (button != null && !button.interactable)
        {
            buttonText.color = disabledColor;
            targetScale = originalScale;
        }
    }

    // change color on hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button != null && !button.interactable)
            return;

        buttonText.color = hoverColor;
        targetScale = originalScale * hoverScale;
    }

    // make color agan normal
    public void OnPointerExit(PointerEventData eventData)
    {
        if (button != null && !button.interactable)
            return;

        buttonText.color = normalColor;
        targetScale = originalScale;
    }

    // make color again normalColor after changing the panel
    private void OnEnable()
    {
        // don't get a reference for other buttons
        if (button == null || buttonText == null)
            return;

        if (!button.interactable)
            buttonText.color = disabledColor;
        else
            buttonText.color = normalColor;

        transform.localScale = originalScale;
        targetScale = originalScale;
    }
}
