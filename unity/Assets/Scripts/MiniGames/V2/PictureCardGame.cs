using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

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

    void Update()
    {
        if (currentInput == null) return;
        if (Keyboard.current == null) return;

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            GameObject selected = EventSystem.current.currentSelectedGameObject;
            TMP_InputField inputField = currentInput.GetComponentInChildren<TMP_InputField>();

            if (selected != null && inputField != null && selected == inputField.gameObject)
            {
                Debug.Log("Enter was clicked");
                ProcessAnswerAndNext();   
            }
        }
    }

    protected override void BuildBoard()
    {
        ClearContainers();

        ShowNextWord();   
    }

    private void ShowNextWord()
    {
        ClearContainers();

        
        if (words == null)
        {
            Debug.LogError("Words is NULL");
            return;
        }

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

        TMP_InputField inputField = currentInput.GetComponentInChildren<TMP_InputField>();

        inputField.ActivateInputField(); // puts cursor into input
        inputField.Select(); // makes input selected

        // next button
        Button btn = currentInput.nextButton;

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(ProcessAnswerAndNext);
    }

    public void ProcessAnswerAndNext()
    {
        if (currentInput == null) return;

        bool isCorrect = currentInput.CheckAnswer();

        if (isCorrect)
            ShowCorrect();
        else
            ShowWrong();

        currentIndex++;
        ShowNextWord();
    }
}
