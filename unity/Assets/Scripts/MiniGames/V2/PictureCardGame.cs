using UnityEngine;
using System.Collections.Generic;
using System.Collections;
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
    private bool autoPlayEnabled = true;
    [SerializeField] private Image autoPlayIcon;
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;

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

    // show next word: create picture and input
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
        currentImageCard.Setup(word.id, this, autoPlayEnabled);

        // create input
        currentInput = Instantiate(wordInputPrefab, secondaryContainer);
        currentInput.Setup(word.id, this);  

        // call UpdateAutoPlayUI to change icons for auto play
        currentImageCard.SetCardAutoPlayUI(autoPlayEnabled);      

        TMP_InputField inputField = currentInput.GetComponentInChildren<TMP_InputField>();

        inputField.ActivateInputField(); // puts cursor into input
        inputField.Select(); // makes input selected

        // next button
        Button btn = currentInput.nextButton;

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(ProcessAnswerAndNext);
    }

    // show correct or wrong icon after submit
    public void ProcessAnswerAndNext()
    {
        if (currentInput == null) return;

        bool isCorrect = currentInput.CheckAnswer();

        if (isCorrect)
            ShowCorrect();
        else
            ShowWrong();

        StartCoroutine(NextAfterDelay(1.5f));
    }

    // show next word after 1,5 sec
    private IEnumerator NextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        currentIndex++;
        ShowNextWord();
    }

    // turn on or turn off autoplay
    public void ToggleAutoPlay()
    {
        autoPlayEnabled = !autoPlayEnabled;

        if (currentImageCard != null)
            currentImageCard.SetCardAutoPlayUI(autoPlayEnabled);

        SetGlobalAutoPlayUI(autoPlayEnabled);
    }

    // change autoplay icon: sound on or sound off
    public void SetGlobalAutoPlayUI(bool enabled)
    {
        if (autoPlayIcon != null)
        {
            autoPlayIcon.sprite = enabled ? soundOn : soundOff;
        }
    }
}
