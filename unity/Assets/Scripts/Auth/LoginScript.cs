using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using System.Collections;
using System.IO;
using UnityEngine.Networking;

public class LoginScript : AuthUIHelper
{
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;
    
    private Database db;

    void Start()
    {
        StartCoroutine(InitDatabase());

        // hide error message when user types in the input fields
        inputEmail.onValueChanged.AddListener(delegate { ClearMessage(); });
        inputPassword.onValueChanged.AddListener(delegate { ClearMessage(); });

        txtMessage.gameObject.SetActive(false);
    
    }
   
    private IEnumerator InitDatabase()
    {
        string persistentPath = Path.Combine(Application.persistentDataPath, "db.json");

        if (!File.Exists(persistentPath))
        {
    #if UNITY_ANDROID && !UNITY_EDITOR
        // read on Android through UnityWebRequest
        UnityWebRequest www = UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, "db.json"));
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            File.WriteAllText(persistentPath, www.downloadHandler.text);
        }
    #else 
        File.Copy(Path.Combine(Application.streamingAssetsPath, "db.json"), persistentPath);
    #endif
        }
        string json = File.ReadAllText(persistentPath);
        db  = JsonUtility.FromJson<Database>(json);

        Debug.Log("LOGIN PATH: " + persistentPath);
        
        yield break;
    }

    // validate user's email and password
    public void ReadData()
    {
        if (db == null)
        {
            ShowMessage("database not loaded yet");
            return;
        }

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