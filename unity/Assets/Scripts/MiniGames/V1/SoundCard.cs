using UnityEngine;
using UnityEngine.EventSystems;

public class SoundCard : BaseMatchCard, IPointerClickHandler
{
    private string wordKey;

    protected override void Awake()
    {
        base.Awake();
    }
    public void SetupSound(int wordId, string wordKey, MatchGame game)
    {
        base.Setup(wordId, game);
        this.wordKey = wordKey.ToLower();
    }

    private void PlaySound()
    {
        if (!string.IsNullOrEmpty(wordKey))
        {
            AudioManager.Instance?.PlayWord(wordKey, AudioManager.LanguageType.Fi);
        }
    }
    public override void SetSelected(bool value)
    {
        base.SetSelected(value);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlaySound();
    }
}

