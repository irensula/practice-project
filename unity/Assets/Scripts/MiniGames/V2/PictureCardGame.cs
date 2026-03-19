using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
public class PictureCardGame : BaseMatchGameV2
{
    [Header("Card Prefabs")]
    public LargeImageCard largeImagePrefab;
    public WordInput wordInputPrefab;

    [Header("UI")]
    public Button nextButton;
    // public Button playSoundButton;

    private LargeImageCard currentImageCard;
    private WordInput currentInput;

    private int currentIndex = 0;

    protected override void BuildBoard()
    {
        ClearContainers();

        ShowNextWord();

        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(OnNextClicked);       
    }

    private void ShowNextWord()
    {
        ClearContainers();

        if (currentIndex >= words.Count)
        {
            Debug.Log("Game finished!");
            return;
        }

        var word = words[currentIndex];

        // create image
        currentImageCard = Instantiate(largeImagePrefab, primaryContainer);
        currentImageCard.Setup(word.id, this);

        // create input
        currentInput = Instantiate(wordInputPrefab, secondaryContainer);
        currentInput.Setup(word.id, this);
    }

    public void OnNextClicked()
    {
        if (currentInput == null) 
            return;

        bool isCorrect = currentInput.CheckAnswer();

        if (isCorrect)
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Wrong!");
        }
        currentIndex++;
        ShowNextWord();
    }
}
