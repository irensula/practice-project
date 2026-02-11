using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PasswordVisiibilityToggle : MonoBehaviour
{
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Image eyeIcon;
    [SerializeField] private Sprite eyeOpen;
    [SerializeField] private Sprite eyeClosed;

    private bool isVisible = false;

    void Start()
    {
        SetHidden();
    }

    // Update is called once per frame
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
        passwordInput.contentType = TMP_InputField.ContentType.Standard;
        passwordInput.inputType = TMP_InputField.InputType.Standard;
        passwordInput.ForceLabelUpdate();

        eyeIcon.sprite = eyeOpen;
    }

    private void SetHidden()
    {
        isVisible = false;
        passwordInput.contentType = TMP_InputField.ContentType.Password;
        passwordInput.inputType = TMP_InputField.InputType.Password;
        passwordInput.ForceLabelUpdate();

        eyeIcon.sprite = eyeClosed;
    }

}
