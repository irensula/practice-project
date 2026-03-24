using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LargeImageCard : BaseMatchCardV2
{
    [SerializeField] private Image image;
    [SerializeField] private Image autoPlayIcon;
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;
    public Button PlayIcon;
    public Button autoPlayToggleButton;
    private AudioSource audioSource;
    private AudioClip audioClip;

    public void Setup(int wordId, BaseMatchGameV2 game,  bool autoPlayEnabled)
    {
        base.Setup(wordId, game);

        var wordData = game.GetWordById(wordId);

        if (wordData != null && image != null)
        {
            Sprite sprite = Resources.Load<Sprite>(wordData.image.Replace(".jpg", "").Replace(".png", ""));
            if (sprite != null)
            {
                image.sprite = sprite;
            }
        }

        var translation = wordData.translations.FirstOrDefault(t => t.languageId == 1);

        if (translation != null)
        {
            string path = translation.audio
                .Replace("audio/", "Sounds/")
                .Replace(".mp3", "");

            audioClip = Resources.Load<AudioClip>(path);

            if(audioClip == null)
                Debug.LogError("Audio NOT FOUND: " + path);

        }

        SetCardAutoPlayUI(autoPlayEnabled);

        if (autoPlayEnabled)
            {
                PlaySound();
            }
    }

    protected override void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0;
    }

    public void OnPlayIconClicked()
    {
        PlaySound();
    }

    public void PlaySound()
    {
        if (audioClip == null) return;

        audioSource.PlayOneShot(audioClip);
        Debug.Log("Play the sound");
    }

    public void OnToggleAutoPlayClicked()
    {
        PictureCardGame pictureGame = game as PictureCardGame;

        if (pictureGame != null)
        {
            pictureGame.ToggleAutoPlay();
        } 
    }

    public void SetCardAutoPlayUI(bool enabled)
    {
        if (autoPlayIcon != null)
        {
            autoPlayIcon.sprite = enabled ? soundOn : soundOff;
        }
    }
}
