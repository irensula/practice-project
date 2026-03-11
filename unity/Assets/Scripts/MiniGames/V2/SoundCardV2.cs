using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class SoundCardV2 : BaseMatchCardV2, IPointerClickHandler
{
    private AudioClip clip;
    private AudioSource audioSource;

    protected void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0;
        audioSource.volume = 1f;
    }

    public override void Setup(int wordId, BaseMatchGameV2 game)
    {
        base.Setup(wordId, game);

        var wordData = game.GetWordById(wordId);
        if (wordData != null)
        {
            var translation = wordData.translations.FirstOrDefault(t => t.languageId == 1);
            if (translation != null && !string.IsNullOrEmpty(translation.audio))
            {
                clip = Resources.Load<AudioClip>(translation.audio.Replace("audio/", "Sounds/").Replace(".mp3", ""));
                if (clip != null)
                {
                    Debug.Log("Loaded audio clip: " + translation.audio);
                }
                else
                {
                    Debug.LogWarning("Cannot load audio clip: " + translation.audio);
                }
            }
        }
    }

    private void PlaySound()
    {
        if (clip != null)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlaySound();
    }
}
