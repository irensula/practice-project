using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class WordInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    private int wordId;
    private BaseMatchGameV2 game;
    public Button nextButton;

    public void Setup(int wordId, BaseMatchGameV2 game)
    {
        this.wordId = wordId;
        this.game = game;

        inputField.text = "";
    }

    public string GetUserInput()
    {
        return inputField.text;
    }

    public bool CheckAnswer()
    {
        var wordData = game.GetWordById(wordId);

        var correct = wordData.translations.FirstOrDefault(t => t.languageId == 1);

        if (correct == null)
            return false;

        string userAnswer = inputField.text.Trim().ToLower();
        string correctAnswer = correct.text.Trim().ToLower();
        
        return userAnswer == correctAnswer;
    }

    public int GetWordId()
    {
        return wordId;
    }
}
