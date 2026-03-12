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

    protected MatchGameModeV2 currentMode;
    protected MatchGameTypeV2 currentType;

    [SerializeField] private MatchGameUI ui;

    private AudioSource audioSource;

    private BaseMatchCardV2 firstSelected = null;
    protected bool isChecking = false;
    private Coroutine currentResultRoutine;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0;

        if (ui == null)
            ui = GetComponentInChildren<MatchGameUI>();

        // btnCloseWinPanel.onClick.AddListener(CloseWinPanel);

        // if (miniGamesUIController == null)
        //     miniGamesUIController = FindObjectOfType<MiniGamesUIController>();
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

    public void CheckAllMatched()
    {
        // create a variable for children in primaryContainer and secondaryContainer
        // DropSlot[] slots = slotContainer.GetComponentsInChildren<DropSlot>();
        
        // foreach (var slot in slots)
        //     if (slot.CurrentWord == null || !slot.CurrentWord.IsMatched) 
        //         return;

        // StartCoroutine(ShowWinPanel());
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
}
   
    // [SerializeField] private SoundCard soundPrefab;     
   
   

    // IEnumerator ShowWinPanel()
    // {
    //     yield return new WaitForSeconds(1.5f);
    //     blockerPanel.SetActive(true);
    //     winPanel.SetActive(true);
    //     PlayResultSound(winClip);
    // }

    // public void CloseWinPanel()
    // {
    //     blockerPanel.SetActive(false);
    //     winPanel.SetActive(false);

    //     BaseMatchCard[] primaryCards = primaryContainer.GetComponentsInChildren<BaseMatchCard>();
    //     BaseMatchCard[] secondaryCards = secondaryContainer.GetComponentsInChildren<BaseMatchCard>();

    //     foreach (var card in primaryCards)
    //         card.ResetItem();

    //     foreach (var card in secondaryCards)
    //         card.ResetItem();

    //     miniGamesUIController.ShowMiniGameMenu();
    // }
