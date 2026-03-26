using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class SoundCardV2 : BaseMatchCardV2, IPointerClickHandler
{
    private string wordKey;

    protected void Awake()
    {
        base.Awake();
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
                wordKey = translation.audio.Replace("audio/fi/", "").Replace(".mp3", "");
            }
        }
    }

    private void PlaySound()
    {
        if (wordKey != null)
        {
            AudioManager.Instance.PlayWord(wordKey, AudioManager.LanguageType.Fi);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlaySound();
    }
}
