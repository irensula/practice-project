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

    [Header("Prefabs")]
    [SerializeField] private WordCard textPrefab;
    [SerializeField] private ImageCard imagePrefab;
    [SerializeField] private SoundCard soundPrefab;

    private BaseMatchCard firstSelected = null;
    private bool isChecking = false;
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
                        textCard.Setup(word.id, this);
                        var finnish = word.translations.FirstOrDefault(t => t.languageId == 1);
                        if (finnish != null)
                            textCard.SetText(finnish.text);
                        break;

                    case MatchGameType.PictureToSound:
                        ImageCard imgCard = Instantiate(imagePrefab, primaryContainer);
                        imgCard.Setup(word.id, this);
                        Sprite sprite = Resources.Load<Sprite>(word.image.Replace(".jpg", "").Replace(".png", ""));
                        if (sprite != null)
                            imgCard.SetImage(sprite);
                        break;

                    case MatchGameType.SoundToText:
                        SoundCard soundCard = Instantiate(soundPrefab, primaryContainer);
                        soundCard.Setup(word.id, this);
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
                    textCard.Setup(word.id, this);
                    var finnish = word.translations.FirstOrDefault(t => t.languageId == 1);
                    if (finnish != null)
                        textCard.SetText(finnish.text);
                    break;

                case MatchContentType.Picture:
                    ImageCard imgCard = Instantiate(imagePrefab, secondaryContainer);
                    imgCard.Setup(word.id, this);
                    Sprite sprite = Resources.Load<Sprite>(word.image.Replace(".jpg", "").Replace(".png", ""));
                    if (sprite != null)
                        imgCard.SetImage(sprite);
                    break;

                case MatchContentType.Sound:
                    SoundCard soundCard = Instantiate(soundPrefab, secondaryContainer);
                    soundCard.Setup(word.id, this);
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
        Debug.Log("The item was selected");
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
        }
        else
        {
            first.SetSelected(false);
            second.SetSelected(false);
        }

        firstSelected = null;
        isChecking = false;
    }

    void ClearContainers()
    {
        foreach (Transform child in primaryContainer)
            Destroy(child.gameObject);

        foreach (Transform child in secondaryContainer)
            Destroy(child.gameObject);
    }
}