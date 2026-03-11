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

     private BaseMatchGameV2 currentGame;

    [Header("Vocabulary")]
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
        Debug.Log("StartTextToPictureGame startetd");    
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