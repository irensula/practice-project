using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class MiniGamesUIController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject miniGameButtonsPanel;
    public GameObject matchGamePanel;

    [Header("Match Game")]
    public MatchGame matchGame;

    [Header("Vocabulary List")]
    public List<WordData> vocabularyList; 
    private Database db;

    private void Awake()
    {
        miniGameButtonsPanel.SetActive(false);
        matchGamePanel.SetActive(false);

        DatabaseService.Init(this, OnDatabaseLoaded);
    }

    void OnDatabaseLoaded()
    {
        db = DatabaseService.Load();
        if (db.words != null)
            vocabularyList = new List<WordData>(db.words);
        else
            vocabularyList = new List<WordData>();

        foreach (var word in vocabularyList)
        {
            string translations = string.Join(", ", word.translations.Select(t => t.text + $"({t.languageId})"));
        }

        miniGameButtonsPanel.SetActive(true);
    }

    // check if vocabulary list is ready
    private bool IsVocabularyReady => vocabularyList != null && vocabularyList.Count > 0;

    public void OpenMatchGame(List<WordData> vocabulary, MatchGameMode mode, MatchGameType type)
    {
        if (vocabulary == null || vocabulary.Count == 0)
        {
            Debug.LogError("Vocabulary list is empty!");
            return;
        }

        miniGameButtonsPanel.SetActive(false);
        matchGamePanel.SetActive(true);

        matchGame.Setup(vocabulary, mode, type);
    }

    public void StartPracticeTextToPicture()
    {
        AudioManager.Instance.PlayClick();

        OpenMatchGame(vocabularyList, MatchGameMode.Practice, MatchGameType.TextToPicture);
    }

    public void StartPracticeSoundToPicture()
    {
        AudioManager.Instance.PlayClick();

        OpenMatchGame(vocabularyList, MatchGameMode.Practice, MatchGameType.SoundToPicture);
    }

    public void StartPracticeTextToSound()
    {
        AudioManager.Instance.PlayClick();

        OpenMatchGame(vocabularyList, MatchGameMode.Practice, MatchGameType.TextToSound);
    }

    public void StartTestTextToPicture()
    {
        AudioManager.Instance.PlayClick();

        OpenMatchGame(vocabularyList, MatchGameMode.Test, MatchGameType.TextToPicture);
    }

    public void StartTestSoundToPicture()
    {
        AudioManager.Instance.PlayClick();

        OpenMatchGame(vocabularyList, MatchGameMode.Test, MatchGameType.SoundToPicture);
    }

    public void StartTestTextToSound()
    {
        AudioManager.Instance.PlayClick();

        OpenMatchGame(vocabularyList, MatchGameMode.Test, MatchGameType.TextToSound);
    }

    public void ShowMiniGameMenu()
    {
        AudioManager.Instance.PlayClick();

        matchGamePanel.SetActive(false);
        miniGameButtonsPanel.SetActive(true);
    }
    public void BackToMenu()
    {
        AudioManager.Instance.PlayClick();
        
        if (matchGamePanel.activeSelf)
        {
            matchGamePanel.SetActive(false);
            miniGameButtonsPanel.SetActive(true);
        }
        else if (miniGameButtonsPanel.activeSelf)
        {
            MenuState.PanelToOpen = MenuState.PanelType.Lessons; 
            MenuState.SetLevel(MenuState.PanelLevel.Lessons);
            SceneManager.LoadScene("MainMenuScene"); 
        }
    }
}