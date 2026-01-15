using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject languagePanel;
    public GameObject mainMenuPanel;
    public GameObject coursesPanel;
    public GameObject lessonsPanel;
    public GameObject optionsPanel;

    private Stack<GameObject> panelStack = new Stack<GameObject>();

    public GameObject languageButtonPrefab;
    public GameObject courseButtonPrefab;
    public GameObject lessonButtonPrefab;
    public Transform languagesContainer;
    public Transform coursesContainer;
    public Transform lessonsContainer; 

    public List<LanguageData> languages;
    private Database db;

    void Start()
    {
        DatabaseService.Init();
        db = DatabaseService.Load();

        ShowLanguage();
    }

    // Update is called once per frame

    public void ShowLanguage()
    {
        panelStack.Clear();
        ShowPanel(languagePanel); 

        // clear the previous buttons
        foreach (Transform child in languagesContainer)
        {
            Destroy(child.gameObject);
        }

        // create new buttons
        foreach (var language in languages)
        {
            GameObject newLanguageObj = Instantiate(languageButtonPrefab, languagesContainer);

            Image img = newLanguageObj.GetComponent<Image>();
            img.sprite = language.languageFlag;

            Button btn = newLanguageObj.GetComponent<Button>();
            UIAudioManager.Instance.RegisterButton(btn);
            string langCode = language.code;
            btn.onClick.AddListener(() => SelectLanguage(langCode));
        }

    }
    public void ShowMainMenu()
    {
       
        ShowPanel(mainMenuPanel);
    }
    public void ShowCourses()
    {
        
        ShowPanel(coursesPanel); 

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
            UIAudioManager.Instance.RegisterButton(btn);

            if(course.locked == false)
            {
                btn.interactable = true;
                btn.onClick.AddListener(() => SelectCourse(course));
            }
            else
            {
                btn.interactable = false;
            }
        }
    }
    public void ShowCourseLessons()
    {
        ShowPanel(lessonsPanel);   

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
            UIAudioManager.Instance.RegisterButton(btn);

            btn.onClick.AddListener(() => OnLessonClicked(lesson));
        }
    }

    public void ShowOptions()
    {
        ShowPanel(optionsPanel);
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
        SceneManager.LoadScene("LessonScene");
    }

    // go back to the previos menu panel
    public void ShowPanel(GameObject panel)
    {
        if (panelStack.Count > 0)
            panelStack.Peek().SetActive(false);

        panelStack.Push(panel);
        panel.SetActive(true);
    }

    public void GoBack()
    {
        if (panelStack.Count <= 1)
            return;
            
        GameObject current = panelStack.Pop();
        current.SetActive(false);

        panelStack.Peek().SetActive(true);
    }

    public void OnBackButton()
    {
        GoBack();
    }
}
