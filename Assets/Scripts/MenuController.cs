using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography;
using System.Linq;
using UnityEditor.Rendering;

public class MenuController : MonoBehaviour
{
    public GameObject languagePanel;
    public GameObject mainMenuPanel;
    public GameObject coursesPanel;
    public GameObject lessonsPanel;
    public GameObject optionsPanel;

    public GameObject courseButtonPrefab;
    public GameObject lessonButtonPrefab;
    public Transform coursesContainer;
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

        // clear the previous buttons
        foreach (Transform child in coursesContainer)
        {
            Destroy(child.gameObject);
        }

        // create new buttons
        foreach (var course in db.courses)
        {
            GameObject newCourseObj = Instantiate(courseButtonPrefab, coursesContainer);

            newCourseObj.GetComponentInChildren<TextMeshProUGUI>().text = course.courseName;

            Button btn = newCourseObj.GetComponent<Button>();
            btn.onClick.AddListener(() => SelectCourse(course));
        }
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
        foreach (var lesson in db.lessons) // !!! add filter !!!
        {
            GameObject newLessonObj = Instantiate(lessonButtonPrefab, lessonsContainer);
            
            newLessonObj.GetComponentInChildren<TextMeshProUGUI>().text = lesson.title;

            Button btn = newLessonObj.GetComponent<Button>();

            btn.onClick.AddListener(() => OnLessonClicked(lesson));
        }
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

    public void SelectCourse(CourseData course)
    {
        MenuBootstrap.Instance.CourseSelected = course.courseName;
        ShowCourseLessons();
    }

    private void OnLessonClicked(LessonData lesson)
    {
        Debug.Log("The lesson opened: " + lesson.title);
    }
}
