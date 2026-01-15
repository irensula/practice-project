using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PasswordVisibilityToggle : MonoBehaviour
{
    [SerializeField] private TMP_InputField targetInput;
    [SerializeField] private Image eyeIcon;
    [SerializeField] private Sprite eyeOpen;
    [SerializeField] private Sprite eyeClosed;

    private bool isVisible;

    void Awake()
    {
        SetHidden();
    }    

    public void Toggle()
    {
        if (isVisible)
        {
            SetHidden();
        }
        else
        {
            SetVisible();
        }
    }

    private void SetVisible()
    {
        isVisible = true;
        targetInput.contentType = TMP_InputField.ContentType.Standard;
        targetInput.ForceLabelUpdate();
        eyeIcon.sprite = eyeOpen;
    }

    private void SetHidden()
    {
        isVisible = false;
        targetInput.contentType = TMP_InputField.ContentType.Password;
        targetInput.ForceLabelUpdate();
        eyeIcon.sprite = eyeClosed;
    }

}
