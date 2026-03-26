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
    private string wordKey;

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
            wordKey = translation.audio
                .Replace("audio/fi/", "")
                .Replace(".mp3", "");
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
    }

    public void OnPlayIconClicked()
    {
        PlaySound();
    }

    public void PlaySound()
    {
        AudioManager.Instance.PlayWord(wordKey, AudioManager.LanguageType.Fi);
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
