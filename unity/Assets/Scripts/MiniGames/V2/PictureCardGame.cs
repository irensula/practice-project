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
    private Button nextButton;
    private Button playSoundButton;

    private LargeImageCard currentImageCard;
    private WordInput currentInput;

    private int currentIndex = 0;

    protected override void BuildBoard()
    {
        ClearContainers();

        ShowNextWord();   
    }

    private void ShowNextWord()
    {
        ClearContainers();

        if (currentIndex >= words.Count)
        {
            StartCoroutine(ShowWinPanel());
            return;
        }

        var word = words[currentIndex];

        // create image
        currentImageCard = Instantiate(largeImagePrefab, primaryContainer);
        currentImageCard.Setup(word.id, this);

        // create input
        currentInput = Instantiate(wordInputPrefab, secondaryContainer);
        currentInput.Setup(word.id, this);

        Button btn = currentInput.nextButton;

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(OnNextClicked);
    }

    public void OnNextClicked()
    {
        if (currentInput == null) 
            return;

        bool isCorrect = currentInput.CheckAnswer();

        if (isCorrect)
        {
            ShowCorrect();
        }
        else
        {
            ShowWrong();
        }
        currentIndex++;
        ShowNextWord();
    }
}
