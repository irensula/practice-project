using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;
public class RegistrationScript : MonoBehaviour
{
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;

    string defaultEmail = "user";
    string defaultPassword = "password";

    public void ReadData()
    {
        string email = inputEmail.text;
        string password = inputPassword.text;

        if (email == defaultEmail && password == defaultPassword)
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}
