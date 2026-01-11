using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

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
public class RegistrationScript : MonoBehaviour
{
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;

    private string defaultEmail;
    private string defaultPassword;

    void Start()
    {
        LoadUserData();
    }

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

    public void ReadData()
    {
        string email = inputEmail.text;
        string password = inputPassword.text;

        if (email == defaultEmail && password == defaultPassword)
        {
            SceneManager.LoadScene("MenuScene");
        }
        else
        {
            Debug.Log("The wrong email or password.");
        }
    }
}
