using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using System.Collections.Generic;

public class RegistrationScript : AuthUIHelper
{
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;
    public TMP_InputField inputConfirmPassword;

    Database db;
    string dbPath;

    void Start()
    {
        string persistentPath = Path.Combine(Application.persistentDataPath, "db.json");
        string streamingPath = Path.Combine(Application.streamingAssetsPath, "db.json");

        if (!File.Exists(persistentPath))
        {
            File.Copy(streamingPath, persistentPath);
        }
        dbPath = persistentPath;

        // hide error message when user types in the input fields
        inputEmail.onValueChanged.AddListener(delegate { ClearMessage(); });
        inputPassword.onValueChanged.AddListener(delegate { ClearMessage(); });
        inputConfirmPassword.onValueChanged.AddListener(delegate { ClearMessage(); });

        txtMessage.gameObject.SetActive(false);
    }
    public void RegisterUser()
    {
        if (!IsValidEmail(inputEmail.text))
        {
            ShowMessage("The email address is invalid");
            return;
        }

        if (inputPassword.text != inputConfirmPassword.text)
        {
            ShowMessage("Passwords do not match.");
            return;
        }
        
        if (inputPassword.text.Length < 8)
        {
            ShowMessage("Passwords must be at least 8 characters long.");
            return;
        }
        // load db.json
        string json = File.ReadAllText(dbPath);
        db = JsonUtility.FromJson<Database>(json);
        if (db == null || db.users == null)
        {
            db = new Database();
            db.users = new UserData[0];
        }

        // create new user
        UserData newUser = new UserData();
        newUser.email = inputEmail.text.Trim();
        newUser.password = inputPassword.text.Trim();
        

        // check email if exists
        foreach (var user in db.users)
        {
            if (user.email.ToLower() == newUser.email.ToLower())
            {
                Debug.Log("This email is already registered.");
                return;
            }
        }

        // add id
        int newId = 1;
        if (db.users.Length > 0)
            newId = db.users[db.users.Length - 1].id + 1;

        newUser.id = newId;

        // add user to array
        var tempList = new List<UserData>(db.users);
        tempList.Add(newUser);
        db.users = tempList.ToArray();

        // save to db.json
        string newJson = JsonUtility.ToJson(db, true);
        File.WriteAllText(dbPath, newJson);
        Debug.Log("RegisterUser called");
        Debug.Log("DB path: " + dbPath);
        // go to the next scene
        SceneManager.LoadScene("MainMenuScene");
    }
}
