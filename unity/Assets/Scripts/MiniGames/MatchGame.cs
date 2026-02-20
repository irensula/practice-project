using System;
using System.Collections.Generic;
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

    [Header("Prefabs")]
    [SerializeField] private WordCard textPrefab;
    [SerializeField] private ImageCard imagePrefab;
    [SerializeField] private SoundCard soundPrefab;
    // private MatchItem firstSelected;
    // private MatchItem secondSelected;

    // private MatchContentType primaryType;
    // private MatchContentType secondaryType;
    // private const int currentLanguageId = 1;

    public void Setup(List<WordData> vocabulary, MatchGameMode mode, MatchGameType type)
    {
        this.words = vocabulary;
        this.currentMode = mode;
        this.currentType = type;

        Debug.Log("Vocabulary: " + words.Count + ", Mode: " + mode + ", Type: " + type);
        
        PopulatePrimaryRow();
        PopulateSecondaryRow();
    }

    private void PopulatePrimaryRow()
    {
        if (words == null || words.Count == 0)
        {
            Debug.LogError("No words to display in primary row!");
            return;
        }

        // clean the container
        foreach (Transform child in primaryContainer)
            Destroy(child.gameObject);

        
        foreach (var word in words)
            {
                switch (currentType)
                {
                    case MatchGameType.TextToPicture:
                        WordCard textCard = Instantiate(textPrefab, primaryContainer);
                        var finnish = word.translations.FirstOrDefault(t => t.languageId == 1);
                        if (finnish != null)
                            textCard.SetText(finnish.text);
                        break;

                    case MatchGameType.PictureToSound:
                        ImageCard imgCard = Instantiate(imagePrefab, primaryContainer);
                        Sprite sprite = Resources.Load<Sprite>(word.image.Replace(".jpg", "").Replace(".png", ""));
                        if (sprite != null)
                            imgCard.SetImage(sprite);
                        break;

                    case MatchGameType.SoundToText:
                        SoundCard soundCard = Instantiate(soundPrefab, primaryContainer);
                        var sound = word.translations.FirstOrDefault(t => t.languageId == 1);
                        if (sound != null)
                        {
                            AudioClip clip = Resources.Load<AudioClip>(sound.audio.Replace(".mp3", ""));
                            if (clip != null)
                                soundCard.SetSound(clip);
                        }
                        break;
                }

                Debug.Log($"Created primary item: {word.id}");
            }
    }

    private void PopulateSecondaryRow()
    {
        if (words == null || words.Count == 0)
        {
            Debug.LogError("No words to display in secondary row!");
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

        foreach (var word in words)
        {
            switch (secondaryContentType)
            {
                case MatchContentType.Text:
                    WordCard textCard = Instantiate(textPrefab, secondaryContainer);
                    var finnish = word.translations.FirstOrDefault(t => t.languageId == 1);
                    if (finnish != null)
                        textCard.SetText(finnish.text);
                    break;

                case MatchContentType.Picture:
                    ImageCard imgCard = Instantiate(imagePrefab, secondaryContainer);
                    Sprite sprite = Resources.Load<Sprite>(word.image.Replace(".jpg", "").Replace(".png", ""));
                    if (sprite != null)
                        imgCard.SetImage(sprite);
                    break;

                case MatchContentType.Sound:
                    SoundCard soundCard = Instantiate(soundPrefab, secondaryContainer);
                    var sound = word.translations.FirstOrDefault(t => t.languageId == 1);
                    if (sound != null)
                    {
                        AudioClip clip = Resources.Load<AudioClip>(sound.audio.Replace(".mp3", ""));
                        if (clip != null)
                            soundCard.SetSound(clip);
                    }
                    break;
            }

            Debug.Log($"Created primary item: {word.id}");
        }

    }
    public void SelectItem()
    {
        Debug.Log("SelectItem clicked: ");
        
    }

    public void OnCardSelected(int wordId)
    {
        Debug.Log("Card selected with ID: " + wordId);

        // TODO: здесь можно проверять, выбрана ли уже карточка,
        // совпадает ли пара, и т.д.
    }
    // void GenerateItems()
    // {
    //     ClearContainers();

    //     List<WordData> shuffledPrimary = words.OrderBy(x => UnityEngine.Random.value).ToList();
    //     List<WordData> shuffledSecondary = words.OrderBy(x => UnityEngine.Random.value).ToList();

    //     foreach (var vocab in shuffledPrimary)
    //     {
    //         CreateItem(primaryType, primaryContainer, vocab);
    //         Translation finnish = Array.Find(vocab.translations, t => t.languageId == currentLanguageId);
    //         if (finnish != null)
    //             Debug.Log("Primary word: " + finnish.text);
    //     }

    //     foreach (var vocab in shuffledSecondary)
    //     {
    //         CreateItem(secondaryType, secondaryContainer, vocab);
    //         Translation finnish = Array.Find(vocab.translations, t => t.languageId == currentLanguageId);
    //         if (finnish != null)
    //             Debug.Log("Secondary word: " + finnish.text);
    //     }
    // }

    // void CreateItem(MatchContentType type, Transform parent, WordData word)
    // {
    //     MatchItem item = Instantiate(itemPrefab, parent);

    //     Translation finnish = Array.Find(word.translations, t => t.languageId == currentLanguageId);

    //     if (finnish == null)
    //         return;

    //     item.Setup(word.id, type, this);

    //     switch (type)
    //     {
    //         case MatchContentType.Text:
    //             item.SetText(finnish.text);
    //             break;

    //         case MatchContentType.Picture:
    //             Sprite sprite = Resources.Load<Sprite>(word.image.Replace(".jpg", "").Replace(".png", ""));
    //             if (sprite != null)
    //                 item.SetImage(sprite);
    //             else
    //                 Debug.LogError("Sprite not found: " + word.image);
    //             break;

    //         case MatchContentType.Sound:
    //             AudioClip clip = Resources.Load<AudioClip>(finnish.audio.Replace(".mp3", ""));
    //             if (clip != null)
    //                 item.SetSound(clip);
    //             else
    //                 Debug.LogError("AudioClip not found: " + finnish.audio);
    //             break;
    //     }

    //     Debug.Log("Created item: " + finnish.text + " under " + parent.name);
    // }

    // public void SelectItem(MatchItem item)
    // {
    //     if (firstSelected == null)
    //     {
    //         firstSelected = item;
    //         firstSelected.SetSelected(true);
    //         return;
    //     }

    //     if (firstSelected == item)
    //         return;

    //     secondSelected = item;
    //     secondSelected.SetSelected(true);

    //     CheckMatch();
    // }

    // void CheckMatch()
    // {
    //     if (firstSelected.id == secondSelected.id)
    //     {
    //         firstSelected.SetMatched();
    //         secondSelected.SetMatched();
    //     }
    //     else
    //     {
    //         firstSelected.SetSelected(false);
    //         secondSelected.SetSelected(false);
    //     }

    //     firstSelected = null;
    //     secondSelected = null;
    // }

    // void ClearContainers()
    // {
    //     foreach (Transform child in primaryContainer)
    //         Destroy(child.gameObject);

    //     foreach (Transform child in secondaryContainer)
    //         Destroy(child.gameObject);
    // }
}



// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine.UI;

// public class MatchGame : MonoBehaviour
// {
//     public MatchType matchType;

//     public Transform primaryRow;
//     public Transform secondaryRow;

//     public GameObject textPrefab;
//     public GameObject imagePrefab;
//     public GameObject soundPrefab;

//     private ContentType primaryType;
//     private ContentType secondaryType;

//     private List<VocabularyItem> vocabulary;
//     private List<VocabularyItem> shuffledPrimary;
//     private List<VocabularyItem> shuffledSecondary;

//     private MatchItem selectedPrimary;
//     private MatchItem selectedSecondary;

//     public enum GameMode
//     {
//         Practice,
//         Test
//     }

//     public enum MatchType { TextToPicture, PictureToSound, SoundToText }

//     public enum ContentType { Text, Picture, Sound }

//     public void StartGame()
//     {
//         ConfigureMatchType();
//         StartCoroutine(LoadVocabulary());
//     }
//     void ConfigureMatchType()
//     {
//         switch (matchType)
//         {
//             case MatchType.TextToPicture:
//                 primaryType = ContentType.Text;
//                 secondaryType = ContentType.Picture;
//                 break;

//             case MatchType.PictureToSound:
//                 primaryType = ContentType.Picture;
//                 secondaryType = ContentType.Sound;
//                 break;

//             case MatchType.SoundToText:
//                 primaryType = ContentType.Sound;
//                 secondaryType = ContentType.Text;
//                 break;
//         }
//     }

//     IEnumerator LoadVocabulary()
//     {
//         vocabulary = GetMockVocabulary();

//         shuffledPrimary = new List<VocabularyItem>(vocabulary);
//         shuffledSecondary = new List<VocabularyItem>(vocabulary);

//         Shuffle(shuffledPrimary);
//         Shuffle(shuffledSecondary);

//         foreach (var item in shuffledPrimary)
//             yield return StartCoroutine(CreateItem(item, primaryType, primaryRow, true));

//         foreach (var item in shuffledSecondary)
//             yield return StartCoroutine(CreateItem(item, secondaryType, secondaryRow, false));
//     }

//     IEnumerator CreateItem(VocabularyItem item, ContentType type, Transform parent, bool isPrimary)
//     {
//         GameObject prefab = GetPrefabByType(type);
//         GameObject obj = Instantiate(prefab, parent);

//         MatchItem matchItem = obj.GetComponent<MatchItem>();
//         matchItem.Setup(item.id, this);

//         if (matchItem is ImageItem_1 imgItem)
//             imgItem.isPrimaryPanel = isPrimary;
        
//         switch(type)
//         {
//             case ContentType.Text:
//                 obj.GetComponentInChildren<TMP_Text>().text = item.word;
//                 break;
//             case ContentType.Picture:
//                 Sprite sprite = Resources.Load<Sprite>("Images/" + item.image);
//                 obj.GetComponentInChildren<Image>().sprite = sprite;
//                 break;
//             case ContentType.Sound:
//                 AudioClip clip = Resources.Load<AudioClip>("Audio/" + item.audio);
//                 obj.GetComponent<AudioSource>().clip = clip;
//                 break;
//         }

//         yield return null;
//     }

//     GameObject GetPrefabByType(ContentType type)
//     {
//         switch (type)
//         {
//             case ContentType.Text: return textPrefab;
//             case ContentType.Picture: return imagePrefab;
//             case ContentType.Sound: return soundPrefab;
//         }

//         return null;
//     }

//     void Shuffle<T>(List<T> list)
//     {
//         for (int i = 0; i < list.Count; i++)
//         {
//             int randomIndex = Random.Range(i, list.Count);
//             (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
//         }
//     }

//     public void SelectItem(MatchItem item, bool isPrimary)
//     {
//         if (isPrimary)
//         {
//             selectedPrimary?.SetSelected(false);
//             selectedPrimary = item;
//             selectedPrimary.SetSelected(true);
//         }
//         else
//         {
//             selectedSecondary?.SetSelected(false);
//             selectedSecondary = item;
//             selectedSecondary.SetSelected(true);
//         }

//         TryMatch();
//     }

//     void TryMatch()
//     {
//         if (selectedPrimary != null && selectedSecondary != null)
//         {
//             if (selectedPrimary.id == selectedSecondary.id)
//             {
//                 selectedPrimary.SetMatched();
//                 selectedSecondary.SetMatched();
//             }
//             else
//             {
//                 selectedPrimary.SetSelected(false);
//                 selectedSecondary.SetSelected(false);
//             }

//             selectedPrimary = null;
//             selectedSecondary = null;
//         }
//     }

//     List<VocabularyItem> GetMockVocabulary()
//     {
//         return new List<VocabularyItem>
//         {
//             new VocabularyItem { id = 1, word = "ruoka", image = "food", audio = "ruoka" },
//             new VocabularyItem { id = 2, word = "ruokalista", image = "menu", audio = "ruokalista" },
//             new VocabularyItem { id = 3, word = "pöytä", image = "table", audio = "poyta" },
//             new VocabularyItem { id = 4, word = "tarjoilija", image = "waiter", audio = "tarjoilija" }
//         };
//     }
// }
