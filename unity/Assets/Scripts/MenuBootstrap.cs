using Unity.VisualScripting;
using UnityEngine;

public class MenuBootstrap : MonoBehaviour
{
    public static MenuBootstrap Instance;
    
    [Header("User")]
    public string UserName = "user";
    public int UserID = 1;

    [Header("Progress")]
    public string LanguageSelected;
    public string CourseSelected;
    public string LessonSelected;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }    

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
