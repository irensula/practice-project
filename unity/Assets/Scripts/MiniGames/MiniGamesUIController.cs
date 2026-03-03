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

    private void Awake()
    {
        DatabaseService.Init();
        var db = DatabaseService.Load();

        vocabularyList = new List<WordData>(db.words);

        foreach (var word in vocabularyList)
        {
            string translations = string.Join(", ", word.translations.Select(t => t.text + $"({t.languageId})"));
        }
    }
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

        matchGame.OnGameFinished += BackToMenu;
    }

    public void StartPracticeTextToPicture()
    {
        OpenMatchGame(vocabularyList, MatchGameMode.Practice, MatchGameType.TextToPicture);
    }

    public void StartPracticeSoundToPicture()
    {
        OpenMatchGame(vocabularyList, MatchGameMode.Practice, MatchGameType.SoundToPicture);
    }

    public void StartPracticeTextToSound()
    {
        OpenMatchGame(vocabularyList, MatchGameMode.Practice, MatchGameType.TextToSound);
    }

    public void StartTestTextToPicture()
    {
        OpenMatchGame(vocabularyList, MatchGameMode.Test, MatchGameType.TextToPicture);
    }

    public void StartTestSoundToPicture()
    {
        OpenMatchGame(vocabularyList, MatchGameMode.Test, MatchGameType.SoundToPicture);
    }

    public void StartTestTextToSound()
    {
        OpenMatchGame(vocabularyList, MatchGameMode.Test, MatchGameType.TextToSound);
    }

    public void BackToMenu()
    {
        if (matchGamePanel.activeSelf)
        {
            matchGamePanel.SetActive(false);
            miniGameButtonsPanel.SetActive(true);
        }
        else
        {
            MenuState.PanelToOpen = MenuState.PanelType.Lessons; 
            MenuState.SetLevel(MenuState.PanelLevel.Lessons);
            SceneManager.LoadScene("MainMenuScene");   
        }
    }
}