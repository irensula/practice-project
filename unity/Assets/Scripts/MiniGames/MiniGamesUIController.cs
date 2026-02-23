using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        Debug.Log("Loaded " + vocabularyList.Count + " words from db.json");

        foreach (var word in vocabularyList)
        {
            string translations = string.Join(", ", word.translations.Select(t => t.text + $"({t.languageId})"));
            Debug.Log($"Word ID: {word.id}, Image: {word.image}, Translations: {translations}");
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

        Debug.Log($"Opened MatchGame: Mode={mode}, Type={type}, Words={vocabulary.Count}");
    }

    public void StartPracticeTextToPicture()
    {
        OpenMatchGame(vocabularyList, MatchGameMode.Practice, MatchGameType.TextToPicture);
    }

    public void StartPracticePictureToSound()
    {
        OpenMatchGame(vocabularyList, MatchGameMode.Practice, MatchGameType.PictureToSound);
    }

    public void StartPracticeSoundToText()
    {
        OpenMatchGame(vocabularyList, MatchGameMode.Practice, MatchGameType.SoundToText);
    }

    public void StartTestTextToPicture()
    {
        OpenMatchGame(vocabularyList, MatchGameMode.Test, MatchGameType.TextToPicture);
    }

    public void StartTestPictureToSound()
    {
        OpenMatchGame(vocabularyList, MatchGameMode.Test, MatchGameType.PictureToSound);
    }

    public void StartTestSoundToText()
    {
        OpenMatchGame(vocabularyList, MatchGameMode.Test, MatchGameType.SoundToText);
    }

    public void BackToMenu()
    {
        matchGamePanel.SetActive(false);
        miniGameButtonsPanel.SetActive(true);
    }
}