using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class RegistrationScript : AuthUIHelper
{
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;
    public TMP_InputField inputConfirmPassword;

    private Database db;

    void Start()
    {
        DatabaseService.Init();
        db = DatabaseService.Load();
        
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
        
        string email = inputEmail.text.Trim().ToLower();
        string password = inputPassword.text;

        // check email if exists
        foreach (var user in db.users)
        {
            if (user.email.ToLower() == email)
            {
                Debug.Log("This email is already registered.");
                return;
            }
        }

        // add id
        int newId = 1;
        if (db.users.Length > 0)
            newId = db.users[db.users.Length - 1].userID + 1;

        
        UserData newUser = new UserData
        {
            userID = newId,
            email = email,
            password = password
        };

        // add user to array
        var userList = new List<UserData>(db.users);
        userList.Add(newUser);
        db.users = userList.ToArray();

        // save to db.json
        DatabaseService.Save(db);

        // go to the next scene
        SceneManager.LoadScene("MainMenuScene");
    }
}
