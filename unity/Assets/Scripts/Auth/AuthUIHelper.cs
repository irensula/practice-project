using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System.Collections;
public class AuthUIHelper : MonoBehaviour
{
    [SerializeField] protected TMP_Text txtMessage;

    // check if email is valid
    protected bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;

        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email.Trim(), pattern);
    }
    // display error message in UI and start auto-hide timer
    protected void ShowMessage(string message, float duration = 5f)
    {
        if (txtMessage == null)
        {
            Debug.LogError("txtMessage is not assigned in Inspector!");
            return;
        }
        
        txtMessage.text = message;
        txtMessage.gameObject.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(HideMessageAfterDelay(duration));
    }

    // hide error message immediately when user types
    protected void ClearMessage()
    {
        txtMessage.gameObject.SetActive(false);
        StopAllCoroutines();
    }

    // hide message after delay 
    IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        txtMessage.gameObject.SetActive(false);
    }
    
}
