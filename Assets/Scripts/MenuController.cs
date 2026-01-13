using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography;
using System.Linq;

public class MenuController : MonoBehaviour
{
    public GameObject languagePanel;
    public GameObject mainMenuPanel;
    public GameObject coursesPanel;
    public GameObject lessonsPanel;
    public GameObject optionsPanel;

    public GameObject lessonButtonPrefab;
    public Transform lessonsContainer; 

    private Database db;

    void Start()
    {
        DatabaseService.Init();
        db = DatabaseService.Load();

        ShowLanguage();
    }

    // Update is called once per frame
    void HideAll()
    {
        languagePanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        coursesPanel.SetActive(false);
        lessonsPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void ShowLanguage()
    {
        HideAll();
        languagePanel.SetActive(true);
    }
    public void ShowMainMenu()
    {
        HideAll();
        mainMenuPanel.SetActive(true);
    }
    public void ShowCourses()
    {
        HideAll();
        coursesPanel.SetActive(true);
    }
    public void ShowCourseLessons()
    {
        HideAll();
        lessonsPanel.SetActive(true);

        // clear the previous buttons
        foreach (Transform child in lessonsContainer)
        {
            Destroy(child.gameObject);
        }

        // create new buttons
        foreach (var lessonData in db.lessons) // !!! add filter !!!
        {
            var currentLesson = lessonData;

            GameObject newLessonObj = Instantiate(lessonButtonPrefab, lessonsContainer);
            
            newLessonObj.GetComponentInChildren<TextMeshProUGUI>().text = currentLesson.title;

            Button btn = newLessonObj.GetComponent<Button>();

            btn.onClick.AddListener(() => OnLessonClicked(currentLesson));
        }
    }

    private void OnLessonClicked(LessonData lesson)
    {
        Debug.Log("The lesson opened: " + lesson.title);
    }
    public void ShowOptions()
    {
        HideAll();
        optionsPanel.SetActive(true);
    }

    public void SelectLanguage(string lang)
    {
        MenuBootstrap.Instance.LanguageSelected = lang;
        ShowMainMenu();
    }

    public void SelectCourse(string course)
    {
        MenuBootstrap.Instance.CourseSelected = course;
        ShowCourseLessons();
    }
}
