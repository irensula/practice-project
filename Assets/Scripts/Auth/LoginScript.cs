using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class LoginScript : AuthUIHelper
{
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;
    
    private Database db;

    void Start()
    {
        DatabaseService.Init();
        db = DatabaseService.Load();

        // hide error message when user types in the input fields
        inputEmail.onValueChanged.AddListener(delegate { ClearMessage(); });
        inputPassword.onValueChanged.AddListener(delegate { ClearMessage(); });

        txtMessage.gameObject.SetActive(false);
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
        bool found = db.users.Any(u =>
            u.email.ToLower() == email.ToLower() &&
            u.password == password
        );

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