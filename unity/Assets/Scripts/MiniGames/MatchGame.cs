using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class MatchGame : MonoBehaviour
{
    private List<WordData> words;          
    private MatchGameMode currentMode;     
    private MatchGameType currentType; 
    
    [Header("Containers")]
    [SerializeField] private Transform primaryContainer;
    [SerializeField] private Transform secondaryContainer;
    [SerializeField] private Transform slotContainer;

    [Header("Prefabs")]
    [SerializeField] private WordCard textPrefab;
    [SerializeField] private ImageCard imagePrefab;
    [SerializeField] private SoundCard soundPrefab;
    
    [SerializeField] private DropSlot slotPrefab;

    private BaseMatchCard firstSelected = null;
    private bool isChecking = false;
    
    public Sprite correctSprite;
    public Sprite wrongSprite;
    public Image resultIcon;
    public GameObject blockerPanel;
    public GameObject winPanel;
    public Button btnCloseWinPanel;

    public event Action OnGameFinished;

    [Header("Result Sounds")]
    [SerializeField] private AudioClip correctClip;
    [SerializeField] private AudioClip wrongClip;
    [SerializeField] private AudioClip winClip;
    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0;

        btnCloseWinPanel.onClick.AddListener(CloseWinPanel);
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void Setup(List<WordData> vocabulary, MatchGameMode mode, MatchGameType type)
    {        
        var selectedWords = vocabulary.OrderBy(x => UnityEngine.Random.value).Take(8).ToList();;

        var primaryWords = new List<WordData>(selectedWords);

        this.currentMode = mode;
        this.currentType = type;

        Shuffle(primaryWords);
        
        PopulatePrimaryRow(primaryWords);
        PopulateSecondaryRowAndSlots(selectedWords);
    }

    private void PopulatePrimaryRow(List<WordData> rowWords)
    {
        if (rowWords == null || rowWords.Count == 0)
        {
            Debug.LogError("No rowWords to display in primary row!");
            return;
        }

        // clean the container
        foreach (Transform child in primaryContainer)
            Destroy(child.gameObject);

        
        foreach (var word in rowWords)
            {
                BaseMatchCard cardInstance = null;

                switch (currentType)
                {
                    case MatchGameType.TextToPicture:
                    case MatchGameType.TextToSound:
                        {   
                            // create text card
                            WordCard textCard = Instantiate(textPrefab, primaryContainer);
                            textCard.Setup(word.id, this);
                            // add text
                            var finnish = word.translations.FirstOrDefault(t => t.languageId == 1);
                            if (finnish != null)
                                textCard.SetText(finnish.text);
                            
                            cardInstance = textCard;
                            break;
                        }

                    case MatchGameType.SoundToPicture:
                        {
                            // create sound card
                            SoundCard soundCard = Instantiate(soundPrefab, primaryContainer);
                            
                            var finnish = word.translations.FirstOrDefault(t => t.languageId == 1);

                            if (finnish != null)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(finnish.audio);
                                AudioClip clip = Resources.Load<AudioClip>($"Sounds/fi/{fileName}");
                                
                                if (clip != null)
                                    soundCard.SetupSound(word.id, fileName, clip, this);
                                else
                                    Debug.LogError($"AudioClip not found: {fileName}");
                            }
                            cardInstance = soundCard;
                            break;
                        } 
                }
                // make it draggable
                if (cardInstance != null && cardInstance.GetComponent<DraggableItem>() == null)
                    cardInstance.gameObject.AddComponent<DraggableItem>();
            }
    }

    private void PopulateSecondaryRowAndSlots(List<WordData> rowWords)
    {
        if (rowWords == null || rowWords.Count == 0)
        {
            Debug.LogError("No rowWords to display in secondary row!");
            return;
        }

        // clean the container
        foreach (Transform child in secondaryContainer)
            Destroy(child.gameObject);

        foreach (Transform child in slotContainer) 
            Destroy(child.gameObject);

        MatchContentType secondaryContentType = currentType switch
        {
            MatchGameType.TextToPicture => MatchContentType.Picture,   // top - text, bottom - picture
            MatchGameType.SoundToPicture => MatchContentType.Picture,   // top - picture, bottom - sound
            MatchGameType.TextToSound => MatchContentType.Sound,       // top - sound, bottom - text
            _ => MatchContentType.Picture
        };

        foreach (var word in rowWords)
        {
            switch (secondaryContentType)
            {
                case MatchContentType.Picture:
                    {
                        ImageCard imgCard = Instantiate(imagePrefab, secondaryContainer);
                        imgCard.Setup(word.id, this);
                        Sprite sprite = Resources.Load<Sprite>(word.image.Replace(".jpg", "").Replace(".png", ""));
                        if (sprite != null)
                            imgCard.SetImage(sprite);
                        break;
                    }

                case MatchContentType.Sound:
                    {
                        SoundCard soundCard = Instantiate(soundPrefab, secondaryContainer);
                            
                            var finnish = word.translations.FirstOrDefault(t => t.languageId == 1);

                            if (finnish != null)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(finnish.audio);
                                AudioClip clip = Resources.Load<AudioClip>($"Sounds/fi/{fileName}");
                                
                                if (clip != null)
                                    soundCard.SetupSound(word.id, fileName, clip, this);
                                else
                                    Debug.LogError($"AudioClip not found: {fileName}");
                            }

                        break;
                    }
            }
            DropSlot slot = Instantiate(slotPrefab, slotContainer);
            slot.Setup(word.id, this);
        }
    }

    public void SelectCard(BaseMatchCard card)
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

    private IEnumerator CheckMatch(BaseMatchCard first, BaseMatchCard second)
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

    private void PlayResultSound(AudioClip clip)
    {
        Debug.Log($"PlaySound called correct or wrong");
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
    
    public void ShowCorrect()
    {
        StartCoroutine(ShowResult(correctSprite));
        PlayResultSound(correctClip);
    }

    public void ShowWrong()
    {
        StartCoroutine(ShowResult(wrongSprite));
        PlayResultSound(wrongClip);
    }

    IEnumerator ShowResult(Sprite sprite)
    {
        resultIcon.sprite = sprite;
        resultIcon.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        resultIcon.gameObject.SetActive(false);
    }

    void ClearContainers()
    {
        foreach (Transform child in primaryContainer)
            Destroy(child.gameObject);

        foreach (Transform child in secondaryContainer)
            Destroy(child.gameObject);
    }

    public void CheckAllMatched()
    {
        // create a variable for children in primaryContainer and secondaryContainer
        DropSlot[] slots = slotContainer.GetComponentsInChildren<DropSlot>();
        
        foreach (var slot in slots)
            if (slot.CurrentWord == null || !slot.CurrentWord.IsMatched) 
                return;

        StartCoroutine(ShowWinPanel());
    }

    IEnumerator ShowWinPanel()
    {
        yield return new WaitForSeconds(1.5f);
        blockerPanel.SetActive(true);
        winPanel.SetActive(true);
        PlayResultSound(winClip);
    }

    public void CloseWinPanel()
    {
        blockerPanel.SetActive(false);
        winPanel.SetActive(false);

        BaseMatchCard[] primaryCards = primaryContainer.GetComponentsInChildren<BaseMatchCard>();
        BaseMatchCard[] secondaryCards = secondaryContainer.GetComponentsInChildren<BaseMatchCard>();

        foreach (var card in primaryCards)
            card.ResetItem();

        foreach (var card in secondaryCards)
            card.ResetItem();

        OnGameFinished?.Invoke();
    }
}