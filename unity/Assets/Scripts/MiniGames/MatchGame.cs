using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public GameObject winPanel;
    public Button btnCloseWinPanel;


    void Start()
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/fi/ruoka");
        if (clip == null)
            Debug.LogError("Cannot load ruoka.mp3");

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
        var secondaryWords = new List<WordData>(selectedWords);

        Shuffle(primaryWords);
        Shuffle(secondaryWords);

        this.words = selectedWords;
        this.currentMode = mode;
        this.currentType = type;

        Debug.Log("Vocabulary: " + words.Count + ", Mode: " + mode + ", Type: " + type);
        
        PopulatePrimaryRow(primaryWords);
        PopulateSecondaryRow(secondaryWords);
        PopulateSlots(selectedWords);
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
                switch (currentType)
                {
                    case MatchGameType.TextToPicture:
                        {
                            WordCard textCard = Instantiate(textPrefab, primaryContainer);
                            textCard.Setup(word.id, this);
                            var finnish = word.translations.FirstOrDefault(t => t.languageId == 1);
                            if (finnish != null)
                                textCard.SetText(finnish.text);
                            break;
                        }

                    case MatchGameType.PictureToSound:
                        {
                            ImageCard imgCard = Instantiate(imagePrefab, primaryContainer);
                            imgCard.Setup(word.id, this);
                            Sprite sprite = Resources.Load<Sprite>(word.image.Replace(".jpg", "").Replace(".png", ""));
                            if (sprite != null)
                                imgCard.SetImage(sprite);
                        break;
                        }

                    case MatchGameType.SoundToText:
                        {
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
                            break;
                        } 
                }
            }
    }

    private void PopulateSecondaryRow(List<WordData> rowWords)
    {
        if (rowWords == null || rowWords.Count == 0)
        {
            Debug.LogError("No rowWords to display in secondary row!");
            return;
        }

        // clean the container
        foreach (Transform child in secondaryContainer)
            Destroy(child.gameObject);

        MatchContentType secondaryContentType = currentType switch
        {
            MatchGameType.TextToPicture => MatchContentType.Picture,   // top - text, bottom - picture
            MatchGameType.PictureToSound => MatchContentType.Sound,   // top - picture, bottom - sound
            MatchGameType.SoundToText => MatchContentType.Text,       // top - sound, bottom - text
            _ => MatchContentType.Picture
        };

        foreach (var word in rowWords)
        {
            switch (secondaryContentType)
            {
                case MatchContentType.Text:
                    {
                        WordCard textCard = Instantiate(textPrefab, secondaryContainer);
                        textCard.Setup(word.id, this);
                        var finnish = word.translations.FirstOrDefault(t => t.languageId == 1);
                        if (finnish != null)
                            textCard.SetText(finnish.text);
                        break;   
                    }

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
        }

    }

    private void PopulateSlots(List<WordData> words)
    {
        foreach (Transform child in slotContainer)
            Destroy(child.gameObject);
        
        foreach (var word in words)
        {
            DropSlot slot = Instantiate(slotPrefab, slotContainer);
            slot.Setup(word.id);
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
            StartCoroutine(ShowResult(correctSprite));

            CheckAllMatched();
        }
        else
        {
            first.SetSelected(false);
            second.SetSelected(false);
            StartCoroutine(ShowResult(wrongSprite));
        }

        firstSelected = null;
        isChecking = false;
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
        BaseMatchCard[] primaryCards = primaryContainer.GetComponentsInChildren<BaseMatchCard>();
        BaseMatchCard[] secondaryCards = secondaryContainer.GetComponentsInChildren<BaseMatchCard>();
        
        foreach (var card in primaryCards)
            if (!card.IsMatched) 
                return;

        foreach (var card in secondaryCards)
            if (!card.IsMatched) 
                return;

        StartCoroutine(ShowWinPanel());
    }

    IEnumerator ShowWinPanel()
    {
        yield return new WaitForSeconds(1.5f);
        winPanel.SetActive(true);
    }

    public void CloseWinPanel()
    {
        winPanel.SetActive(false);

        BaseMatchCard[] primaryCards = primaryContainer.GetComponentsInChildren<BaseMatchCard>();
        BaseMatchCard[] secondaryCards = secondaryContainer.GetComponentsInChildren<BaseMatchCard>();

        foreach (var card in primaryCards)
            card.ResetItem();

        foreach (var card in secondaryCards)
            card.ResetItem();
    }
}