using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject languagePanel;
    public GameObject mainMenuPanel;
    public GameObject coursesPanel;
    public GameObject lessonsPanel;
    public GameObject optionsPanel;

    void Start()
    {
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
    public void ShowLessons()
    {
        HideAll();
        lessonsPanel.SetActive(true);
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
        ShowLessons();
    }
}
