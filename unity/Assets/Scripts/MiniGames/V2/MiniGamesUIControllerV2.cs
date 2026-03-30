using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class MiniGamesUIControllerV2 : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject miniGameButtonsPanel;
    public GameObject matchGamePanel;

    [Header("Game Prefabs")]
    public TextToPictureGameV2 textToPicturePrefab;
    public SoundToTextGameV2 soundToTextPrefab;

    public SoundToPictureGameV2 soundToPicturePrefab;
    public PictureCardGame pictureCardPrefab;
    private BaseMatchGameV2 currentGame;

    [Header("Vocabulary")]
    public List<WordData> vocabularyList; 
    private Database db;

    private void Awake()
    {
        DatabaseService.Init(this, OnDatabaseLoaded);

        vocabularyList = new List<WordData>(db.words);  

        foreach (var word in vocabularyList)
        {
            string translations = string.Join(", ", word.translations.Select(t => t.text + $"({t.languageId})"));
        }
    }

    void OnDatabaseLoaded()
    {
        db = DatabaseService.Load();
    }

    public void StartTextToPictureGame()
    {
        miniGameButtonsPanel.SetActive(false);
        matchGamePanel.SetActive(true);

        // delete the previous game
        if (currentGame != null)
            Destroy(currentGame.gameObject);

        // create a new game
        currentGame = Instantiate(textToPicturePrefab, matchGamePanel.transform);
        currentGame.Setup(vocabularyList, MatchGameModeV2.Practice, MatchGameTypeV2.TextToPicture);   
    }

    public void StartSoundToPictureGame()
    {
        miniGameButtonsPanel.SetActive(false);
        matchGamePanel.SetActive(true);

        // delete the previous game
        if (currentGame != null)
            Destroy(currentGame.gameObject);

        // create a new game
        currentGame = Instantiate(soundToPicturePrefab, matchGamePanel.transform);
        currentGame.Setup(vocabularyList, MatchGameModeV2.Practice, MatchGameTypeV2.SoundToPicture);
    }

    public void StartSoundToTextGame()
    {
        miniGameButtonsPanel.SetActive(false);
        matchGamePanel.SetActive(true);

        // delete the previous game
        if (currentGame != null)
            Destroy(currentGame.gameObject);

        // create a new game
        currentGame = Instantiate(soundToTextPrefab, matchGamePanel.transform);
        currentGame.Setup(vocabularyList, MatchGameModeV2.Practice, MatchGameTypeV2.SoundToText);  
    }

    public void StartPictureCardGame()
    {
        miniGameButtonsPanel.SetActive(false);
        matchGamePanel.SetActive(true);

        // delete the previous game
        if (currentGame != null)
            Destroy(currentGame.gameObject);

        // create a new game
        currentGame = Instantiate(pictureCardPrefab, matchGamePanel.transform);
        currentGame.Setup(vocabularyList, MatchGameModeV2.Practice, MatchGameTypeV2.PictureCard);   
    }

    public void ShowMiniGameMenu()
    {
        matchGamePanel.SetActive(false);
        miniGameButtonsPanel.SetActive(true);
    }
    public void BackToMenu()
    {
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