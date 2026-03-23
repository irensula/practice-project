using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class BaseMatchGameV2 : MonoBehaviour
{
    protected List<WordData> words;          
    
    [Header("Containers")]
    public Transform primaryContainer;
    public Transform secondaryContainer;
    public Transform slotContainer;
    public Transform resultContainer;

    protected MatchGameModeV2 currentMode;
    protected MatchGameTypeV2 currentType;

    [SerializeField] private MatchGameUI ui;

    private AudioSource audioSource;

    private BaseMatchCardV2 firstSelected = null;
    protected bool isChecking = false;
    private Coroutine currentResultRoutine;
    public MiniGamesUIControllerV2 miniGamesUIController;

    protected HorizontalLayoutGroup primaryLayout;
    protected HorizontalLayoutGroup secondaryLayout;
    [SerializeField] private int picturePrimaryTopPadding = 300;
    [SerializeField] private int pictureSecondaryTopPadding = 300;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0;

        if (ui == null)
            ui = GetComponentInChildren<MatchGameUI>();

        ui.btnCloseWinPanel.onClick.AddListener(CloseWinPanel);

        if (miniGamesUIController == null)
            miniGamesUIController = FindObjectOfType<MiniGamesUIControllerV2>();
    }

    public virtual void Setup(List<WordData> vocabulary, MatchGameModeV2 mode, MatchGameTypeV2 type)
    {        
        words = vocabulary
            .OrderBy(x => UnityEngine.Random.value)
            .Take(8)
            .ToList();

        currentMode = mode;
        currentType = type;

        Shuffle(words);
        BuildBoard(); 

        SetupLayoutForGame();   
    }

    protected abstract void BuildBoard();

    protected void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }  
    }

    protected void ClearContainers()
    {
        foreach (Transform child in primaryContainer)
            Destroy(child.gameObject);

        foreach (Transform child in secondaryContainer)
            Destroy(child.gameObject);

        foreach(Transform child in slotContainer)
            Destroy(child.gameObject);

        foreach(Transform child in resultContainer)
            Destroy(child.gameObject);
    }

    public WordData GetWordById(int id)
    {
        return words.Find(w => w.id == id);
    }

    public void SelectCard(BaseMatchCardV2 card)
    {
        if (isChecking) return;

        if (firstSelected == null)
        {
            firstSelected = card;
            card.SetSelected(true);
            return;
        }

        if (firstSelected == card)
            return;

        StartCoroutine(CheckMatch(firstSelected, card));
    }  

    private IEnumerator CheckMatch(BaseMatchCardV2 first, BaseMatchCardV2 second)
    {
        isChecking = true;
        second.SetSelected(true);

        yield return new WaitForSeconds(0.5f);

        if (first.WordId == second.WordId)
        {
            first.SetMatched();
            second.SetMatched();

            CheckAllMatched();
        }
        else
        {
            first.SetSelected(false);
            second.SetSelected(false);
            
        }

        firstSelected = null;
        isChecking = false;
    }

    public void ShowCorrect()
    {
        if (currentResultRoutine != null) StopCoroutine(currentResultRoutine);
        currentResultRoutine = StartCoroutine(ShowResultRoutine(ui.correctSprite, ui.correctClip));
    }

    public void ShowWrong()
    {
        if (currentResultRoutine != null) StopCoroutine(currentResultRoutine);
        currentResultRoutine = StartCoroutine(ShowResultRoutine(ui.wrongSprite, ui.wrongClip));
    }

    private void OnEnable()
    {
        if (ui != null)
        {
            ui.ResultPanel.SetActive(false);
            ui.blockerPanel.SetActive(false);
        }
    }

    private IEnumerator ShowResultRoutine(Sprite icon, AudioClip clip)
    {
        ui.blockerPanel.SetActive(true);
        ui.ResultPanel.SetActive(true);
        ui.resultIcon.sprite = icon;
        ui.resultPop.Play();
        PlayResultSound(clip);

        yield return new WaitForSeconds(1f);

        ui.blockerPanel.SetActive(false);
        ui.ResultPanel.SetActive(false);
    }

    private void PlayResultSound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Result sound not assigned!");
        }
    }

    public void CheckAllMatched()
    {
        // create a variable for children in primaryContainer and secondaryContainer
        DropSlotV2[] slots = slotContainer.GetComponentsInChildren<DropSlotV2>();
        
        foreach (var slot in slots)
            if (slot.CurrentWord == null || !slot.CurrentWord.IsMatched) 
                return;
    }

    
    protected IEnumerator ShowWinPanel()
    {
        yield return new WaitForSeconds(1.5f);
        ui.blockerPanel.SetActive(true);
        ui.winPanel.SetActive(true);
        PlayResultSound(ui.winClip);
    }

    public void CloseWinPanel()
    {
        ui.blockerPanel.SetActive(false);
        ui.winPanel.SetActive(false);

        BaseMatchCard[] primaryCards = primaryContainer.GetComponentsInChildren<BaseMatchCard>();
        BaseMatchCard[] secondaryCards = secondaryContainer.GetComponentsInChildren<BaseMatchCard>();

        foreach (var card in primaryCards)
            card.ResetItem();

        foreach (var card in secondaryCards)
            card.ResetItem();

        miniGamesUIController.ShowMiniGameMenu();
    }

    public virtual void OnCorrectMatch(int wordId, DropSlotV2 slot)
    {
        ShowCorrect();
        StartCoroutine(CheckAndShowWin(slotContainer));
    }

    private IEnumerator CheckAndShowWin(Transform slotContainer)
    {
        // даём анимации/звукам завершиться
        yield return new WaitForSeconds(0.5f);

        // проверяем все слоты
        DropSlotV2[] slots = slotContainer.GetComponentsInChildren<DropSlotV2>();
        bool allMatched = true;
        foreach (var slot in slots)
        {
            if (!slot.IsMatched)
            {
                allMatched = false;
                break;
            }
        }

        if (allMatched)
        {
            yield return StartCoroutine(ShowWinPanel());
        }
    }

    // layout for PictureCardGame (make it in the center and with topPaddings)
    protected void SetupLayoutForGame()
    {
        if (primaryLayout == null)
        {
            primaryLayout = primaryContainer.GetComponentInChildren<HorizontalLayoutGroup>();
            secondaryLayout = secondaryContainer.GetComponentInChildren<HorizontalLayoutGroup>();
        }

        if (primaryLayout == null)
        {
            Debug.Log("PrimaryLayout not found!");
            return;
        }

        switch (currentType)
        {
            case MatchGameTypeV2.PictureCard:
                primaryLayout.childAlignment = TextAnchor.MiddleCenter;
                secondaryLayout.childAlignment = TextAnchor.MiddleCenter;
                primaryLayout.padding.top = picturePrimaryTopPadding;
                secondaryLayout.padding.top = pictureSecondaryTopPadding;
                break;

            default:
                primaryLayout.childAlignment = TextAnchor.MiddleLeft;
                secondaryLayout.childAlignment = TextAnchor.MiddleLeft;
                primaryLayout.padding.top = 0;
                secondaryLayout.padding.top = 0;
                break;
        }
    }
}  