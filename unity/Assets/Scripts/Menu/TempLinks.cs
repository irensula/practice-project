using UnityEngine;
using UnityEngine.SceneManagement;

public class TempLinks : MonoBehaviour
{
    public void MatchGameFromDatabase()
    {
        SceneManager.LoadScene("MatchGame");
    }
    public void MatchGameV2()
    {
        SceneManager.LoadScene("V2LessonScene");
    }
}
