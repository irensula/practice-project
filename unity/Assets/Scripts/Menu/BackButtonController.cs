using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonController : MonoBehaviour
{
    void OnBackButton()
    {
        string scene = SceneManager.GetActiveScene().name;

        if (scene == "MainMenuScene")
        {
            MenuController menu = MenuController.Instance;

            if (menu.optionsPanel.activeSelf)
            {
                menu.ShowMainMenu();
                return;
            }

            if (menu.lessonsPanel.activeSelf)
                menu.ShowCourses();
            else if (menu.coursesPanel.activeSelf)
                menu.ShowMainMenu();
            else if (menu.mainMenuPanel.activeSelf)
                menu.ShowLanguage();
        }
        else
        {
            MiniGamesUIController miniGames = FindObjectOfType<MiniGamesUIController>();
           if (miniGames != null && miniGames.matchGamePanel.activeSelf)
            {
                // close mini game and show minigames buttons
                miniGames.matchGamePanel.SetActive(false);
                miniGames.miniGameButtonsPanel.SetActive(true);
            }
            else
            {
                // go back to MainMenuScene
                MenuState.PanelToOpen = MenuState.PanelType.Lessons;
                SceneManager.LoadScene("MainMenuScene");
            }
        }
    }
}
