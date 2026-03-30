using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Reflection;

public class MenuController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject languagePanel;
    public GameObject mainMenuPanel;
    public GameObject coursesPanel;
    public GameObject lessonsPanel;
    public GameObject optionsPanel;
    
    [Header("Buttons")]
    public GameObject languageButtonPrefab;
    public GameObject baseButtonPrefab;
    public Button backButton;

    [Header("Button Containers")]
    public Transform languagesContainer;
    public Transform coursesContainer;
    public Transform lessonsContainer; 

    [Header("Languages")]
    public List<LanguageData> languages;

    public TextMeshProUGUI languageText;

    [Header("Database JSON")]
    private Database db;


    void Start()
    {
        DatabaseService.Init(this, OnDatabaseLoaded);
    }
    void OnDatabaseLoaded()
    {
        db = DatabaseService.Load();
        Debug.Log("DB loaded! Languages: " + db.languages.Length);
        ShowMenuUI();

    }

    void ShowMenuUI()
    {
        if (MenuState.PanelToOpen != null)
        {
            switch (MenuState.PanelToOpen)
            {
                case MenuState.PanelType.Language:
                    ShowLanguage();
                    break;
                case MenuState.PanelType.MainMenu:
                    ShowMainMenu();
                    break;
                case MenuState.PanelType.Courses:
                    ShowCourses();
                    break;
                case MenuState.PanelType.Lessons:
                    CourseData course = System.Array.Find(db.courses, c => c.courseName == MenuBootstrap.Instance.CourseSelected);
                    if(course != null)
                        SelectCourse(course);
                    break;
                case MenuState.PanelType.Options:
                    ShowOptions();
                    break;
            }
            MenuState.PanelToOpen = null;
        }
        else
        {
            ShowLanguage();
        }

        AudioManager.Instance.RegisterButton(backButton);
    }

    public static MenuController Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void HideAllPanels()
    {
        languagePanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        coursesPanel.SetActive(false);
        lessonsPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void ShowLanguage()
    {
        HideAllPanels();
        languagePanel.SetActive(true);
        MenuState.SetLevel(MenuState.PanelLevel.Language); 

        // clear the previous buttons
        foreach (Transform child in languagesContainer)
        {
            Destroy(child.gameObject);
        }

        // create new buttons
        foreach (var language in languages)
        {
            GameObject newLanguageObj = Instantiate(languageButtonPrefab, languagesContainer);

            // set image
            Image img = newLanguageObj.GetComponent<Image>();
            img.sprite = language.languageFlag;

            // set button listener
            Button btn = newLanguageObj.GetComponent<Button>();
            string langCode = language.code;
            btn.onClick.AddListener(() => SelectLanguage(langCode));
            
            // fix layout for WebGL / dynamic instantiation
            RectTransform rt = newLanguageObj.GetComponent<RectTransform>();
            rt.localScale = Vector3.one;                   
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(languagesContainer.GetComponent<RectTransform>());
        RegisterPanelButtons(languagePanel);
    }

    public void ShowMainMenu()
    {
        HideAllPanels();
        mainMenuPanel.SetActive(true);
        MenuState.SetLevel(MenuState.PanelLevel.MainMenu);

        RegisterPanelButtons(mainMenuPanel);
    }

    public void ShowCourses()
    {
        HideAllPanels();
        coursesPanel.SetActive(true);
        MenuState.SetLevel(MenuState.PanelLevel.Courses); 

        // clear the previous buttons
        foreach (Transform child in coursesContainer)
        {
            Destroy(child.gameObject);
        }

        // create new buttons
        foreach (var course in db.courses)
        {
            GameObject newButton = Instantiate(baseButtonPrefab, coursesContainer);

            newButton.GetComponentInChildren<TextMeshProUGUI>().text = course.courseName;

            Button btn = newButton.GetComponent<Button>();            

            if(!course.locked)
            {
                btn.interactable = true;
                btn.onClick.AddListener(() => SelectCourse(course));
            }
            else
            {
                btn.interactable = false;
            }
        }
        RegisterPanelButtons(coursesPanel);
    }

    public void ShowCourseLessons()
    {
        HideAllPanels();
        lessonsPanel.SetActive(true);
        MenuState.SetLevel(MenuState.PanelLevel.Lessons);   

        // clear the previous buttons
        foreach (Transform child in lessonsContainer)
        {
            Destroy(child.gameObject);
        }

        // create new buttons
        foreach (var lesson in db.lessons) // !!! add filter !!!
        {
            GameObject newButton = Instantiate(baseButtonPrefab, lessonsContainer);
            
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = lesson.title;

            Button btn = newButton.GetComponent<Button>();

            btn.onClick.AddListener(() => OnLessonClicked(lesson));
        }
        RegisterPanelButtons(lessonsPanel);
    }

    public void ShowOptions()
    {
        HideAllPanels();
        optionsPanel.SetActive(true);
        RegisterPanelButtons(optionsPanel);
    }

    public void CloseOptionsPanel()
    {
        ShowMainMenu();
    }
    public void SelectLanguage(string lang)
    {
        MenuBootstrap.Instance.LanguageSelected = lang;
        UpdateProgressUI();
        ShowMainMenu();
    }

    public void SelectCourse(CourseData course)
    {
        MenuBootstrap.Instance.CourseSelected = course.courseName;
        ShowCourseLessons();
    }

    public string GetLanguageNameByCode(string code)
    {
        LanguageData lang = languages.Find(l => l.code == code);
        if (lang != null)
            return lang.languageName;
        return code;
    }

    public void UpdateProgressUI()
    {
        if (languageText != null)
            languageText.text = "Language: " + GetLanguageNameByCode(MenuBootstrap.Instance.LanguageSelected);
    }

    private void OnLessonClicked(LessonData lesson)
    {
        SceneManager.LoadScene("LessonScene");
    }
    
    public void GoBack()
    {
        MenuState.PanelLevel? level = MenuState.GetLevel();
        
        if(level == null)
            return;

        switch(level)
        {
            case MenuState.PanelLevel.Lessons:
                ShowCourses();
                MenuState.SetLevel(MenuState.PanelLevel.Courses);
                break;
            case MenuState.PanelLevel.Courses:
                ShowMainMenu();
                MenuState.SetLevel(MenuState.PanelLevel.MainMenu);
                break;
            case MenuState.PanelLevel.MainMenu:
                ShowLanguage();
                MenuState.SetLevel(MenuState.PanelLevel.Language);
                break;
            case MenuState.PanelLevel.Language:
                break;
        }
    }

    public void OnBackButton()
    {
        GoBack();
    }

    private void RegisterPanelButtons(GameObject panel)
    {
        if (AudioManager.Instance == null) return;

        Button[] buttons = panel.GetComponentsInChildren<Button>(true);
        foreach (Button btn in buttons)
        {
            AudioManager.Instance.RegisterButton(btn);
        }
    }
}
