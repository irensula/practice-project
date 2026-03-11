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
        Debug.Log("Setup called");      
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
        Debug.Log("Shuffle called");  
    }

    protected void ClearContainers()
    {
        foreach (Transform child in primaryContainer)
            Destroy(child.gameObject);

        foreach (Transform child in secondaryContainer)
            Destroy(child.gameObject);

        foreach(Transform child in slotContainer)
            Destroy(child.gameObject);
            Debug.Log("ClearContainers called");
    }

    public WordData GetWordById(int id)
    {
        Debug.Log("GetWordById");
        return words.Find(w => w.id == id);
    }
}

    // protected bool isChecking = false;

    // [Header("Animated Icons")]
    // [SerializeField] private PopAnimation resultPop;
    
    // [Header("Result Sounds")]
    // [SerializeField] private AudioClip correctClip;
    // [SerializeField] private AudioClip wrongClip;
    // [SerializeField] private AudioClip winClip;
    // private AudioSource audioSource;

    // [SerializeField] private SoundCard soundPrefab; 

    // private BaseMatchCard firstSelected = null;
    
    
    // public Sprite correctSprite;
    // public Sprite wrongSprite;
    // public Image resultIcon;
    // public GameObject blockerPanel;
    // public GameObject winPanel;
    // public Button btnCloseWinPanel;

    // void Start()
    // {
    //     audioSource = GetComponent<AudioSource>();
    //     if (audioSource == null)
    //         audioSource = gameObject.AddComponent<AudioSource>();

    //     audioSource.playOnAwake = false;
    //     audioSource.spatialBlend = 0;

    //     btnCloseWinPanel.onClick.AddListener(CloseWinPanel);

    //     if (miniGamesUIController == null)
    //         miniGamesUIController = FindObjectOfType<MiniGamesUIController>();
    // }

    // public void SelectCard(BaseMatchCard card)
    // {
    //     if (isChecking) return;

    //     if (firstSelected == null)
    //     {
    //         firstSelected = card;
    //         card.SetSelected(true);
    //         return;
    //     }

    //     if (firstSelected == card)
    //         return;

    //     StartCoroutine(CheckMatch(firstSelected, card));
    // }    

    // private IEnumerator CheckMatch(BaseMatchCard first, BaseMatchCard second)
    // {
    //     isChecking = true;
    //     second.SetSelected(true);

    //     yield return new WaitForSeconds(0.5f);

    //     if (first.WordId == second.WordId)
    //     {
    //         first.SetMatched();
    //         second.SetMatched();

    //         CheckAllMatched();
    //     }
    //     else
    //     {
    //         first.SetSelected(false);
    //         second.SetSelected(false);
            
    //     }

    //     firstSelected = null;
    //     isChecking = false;
    // }

    // private void PlayResultSound(AudioClip clip)
    // {
    //     if (clip != null)
    //         {
    //             audioSource.Stop();
    //             audioSource.clip = clip;
    //             audioSource.Play();
    //         }
    //         else
    //         {
    //             Debug.LogWarning("Result sound not assigned!");
    //         }
    // }
    
    // public void ShowCorrect()
    // {
    //     resultIcon.sprite = correctSprite;
    //     resultPop.Play();
    //     PlayResultSound(correctClip);
    // }

    // public void ShowWrong()
    // {
    //     resultIcon.sprite = wrongSprite;
    //     resultPop.Play();
    //     PlayResultSound(wrongClip);
    // }

    

    // public void CheckAllMatched()
    // {
    //     // create a variable for children in primaryContainer and secondaryContainer
    //     DropSlot[] slots = slotContainer.GetComponentsInChildren<DropSlot>();
        
    //     foreach (var slot in slots)
    //         if (slot.CurrentWord == null || !slot.CurrentWord.IsMatched) 
    //             return;

    //     StartCoroutine(ShowWinPanel());
    // }

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
