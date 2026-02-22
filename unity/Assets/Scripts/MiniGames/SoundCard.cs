using UnityEngine;
using UnityEngine.EventSystems;

public class SoundCard : BaseMatchCard
{
    private string wordKey;

    public void SetupSound(int wordId, string wordKey, MatchGame game)
    {
        base.Setup(wordId, game);
        this.wordKey = wordKey.ToLower();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    private void PlaySound()
    {
        Debug.Log($"PlaySound called for {wordKey}");
        if (!string.IsNullOrEmpty(wordKey))
        {
            UIAudioManager.Instance.PlayWord(wordKey);
        }
    }
    public override void SetSelected(bool value)
    {
        base.SetSelected(value);

        if (value)
        {
            PlaySound();
        }
    }
}

