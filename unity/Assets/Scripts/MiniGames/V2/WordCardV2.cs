using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Numerics;

public class WordCardV2 : BaseMatchCardV2, IPointerClickHandler
{
    [SerializeField] private TMP_Text text;
    private AudioSource audioSource;
    private AudioClip audioClip;

    protected override void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0;
    }

    public override void Setup(int wordId, BaseMatchGameV2 game)
    {
        base.Setup(wordId, game);

        var wordData = game.GetWordById(wordId);
        var translation = wordData.translations.FirstOrDefault(t => t.languageId == 1);

        if (translation != null)
        {
            text.text = translation.text;

            string path = translation.audio
                .Replace("audio/", "Sounds/")
                .Replace(".mp3", "");

            audioClip = Resources.Load<AudioClip>(path);

            if(audioClip == null)
                Debug.LogError("Audio NOT FOUND: " + path);
            }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlaySound();
    }

    public void PlaySound()
    {
        if (audioClip == null) return;

        audioSource.Stop();
        audioSource.clip = audioClip;
        audioSource.Play();
        Debug.Log("Play the sound");
    }
}