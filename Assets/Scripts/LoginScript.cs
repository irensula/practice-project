using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using System.Text.RegularExpressions;

[System.Serializable]
public class UserData
{
    public int id;
    public string email;
    public string password;
}

[System.Serializable]
public class Database
{
    public UserData[] users;
}
public class LoginScript : AuthUIHelper
{
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;
    
    private Database db;
    private string dbPath;

    void Start()
    {
        dbPath = Path.Combine(Application.streamingAssetsPath, "db.json");
        LoadUserData();

        // hide error message when user types in the input fields
        inputEmail.onValueChanged.AddListener(delegate { ClearMessage(); });
        inputPassword.onValueChanged.AddListener(delegate { ClearMessage(); });

        txtMessage.gameObject.SetActive(false);
    }
    // load default email and password from db.json
    void LoadUserData()
    {
        if (File.Exists(dbPath))
        {
            string json = File.ReadAllText(dbPath);
            db = JsonUtility.FromJson<Database>(json);
        } 
        else
        {
            db = new Database();
            db.users = new UserData[0];
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
        // looking for user in db.json
        bool found = false;
        foreach (var user in db.users)
        {
            if (user.email == email && user.password == password)
            {
                found = true;
                break;
            }
        }
        if (found)
        {
            SceneManager.LoadScene("MainMenuScene");
        }
        else
        {
            ShowMessage("Incorrect email address or password");
        }
    }
    
}