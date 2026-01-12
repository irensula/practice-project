// using UnityEngine;
// using UnityEngine.SceneManagement;
// using System.IO;
// using TMPro;

// public class RegistrationScript : MonoBehaviour
// {
//     public TMP_InputField inputEmail;
//     public TMP_InputField inputPassword;

//     void Start()
//     {
//         LoadUserData();
//     }

//     void LoadUserData()
//     {
//         string path = Path.Combine(Application.streamingAssetsPath, "db.json");

//         if (File.Exists(path))
//         {
//             string json = File.ReadAllText(path);
//             Database db = JsonUtility.FromJson<Database>(json);

            
//         } 
//         else
//         {
//             Debug.LogError("db.json not found!");    
//         }
//     }

//     public void ReadData()
//     {
//         string email = inputEmail.text;
//         string password = inputPassword.text;

//         if (true)
//         {
//             SceneManager.LoadScene("MenuScene");
//         }
//         else
//         {
//             Debug.Log("The wrong email or password.");
//         }
//     }
// }
