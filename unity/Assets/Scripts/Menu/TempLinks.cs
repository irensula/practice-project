using UnityEngine;
using UnityEngine.SceneManagement;

public class TempLinks : MonoBehaviour
{    public void MatchGameFromDatabase()
    {
        AudioManager.Instance.PlayClick();
        SceneManager.LoadScene("MatchGame");
    }
    public void MatchGameV2()
    {
        AudioManager.Instance.PlayClick();
        SceneManager.LoadScene("V2LessonScene");
    }
}
