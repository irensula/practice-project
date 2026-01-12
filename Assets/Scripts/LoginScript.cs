using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using System.Text.RegularExpressions;

[System.Serializable]
public class UserData
{
    public string email;
    public string password;
}

[System.Serializable]
public class Database
{
    public UserData user;
}
public class LoginScript : MonoBehaviour
{
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;

    private string defaultEmail;
    private string defaultPassword;

    public TMP_Text txtMessage;
    void Start()
    {
        LoadUserData();
        // hide error message when user types in the input fields
        inputEmail.onValueChanged.AddListener(delegate { ClearMessage(); });
        inputPassword.onValueChanged.AddListener(delegate { ClearMessage(); });

        txtMessage.gameObject.SetActive(false);
    }
    // load default email and password from db.json
    void LoadUserData()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "db.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Database db = JsonUtility.FromJson<Database>(json);

            defaultEmail = db.user.email;
            defaultPassword = db.user.password;
        } 
        else
        {
            Debug.LogError("db.json not found!");    
        }
    }
    // validate user's email and password
    public void ReadData()
    {
        string email = inputEmail.text.Trim();
        string password = inputPassword.text.Trim();

        if (!IsValidEmail(email)) 
        {
            ShowMessage("The email address is invalid");
            return;
        }
        else if (email == defaultEmail && password == defaultPassword) 
        {
            SceneManager.LoadScene("MainMenuScene");
        }
        else
        {
            ShowMessage("Incorrect email address or password");
        }
    }
    // check if email is valid
    bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;

        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email.Trim(), pattern);
    }
    // display error message in UI and start auto-hide timer
    void ShowMessage(string message)
    {
        txtMessage.text = message;
        txtMessage.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(HideMessageAfterDelay(5f));
    }
    // hide message after delay 
    System.Collections.IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        txtMessage.gameObject.SetActive(false);
    }
    // hide error message immediately when user types
    void ClearMessage()
    {
        txtMessage.gameObject.SetActive(false);
        StopAllCoroutines();
    }
}
