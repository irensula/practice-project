using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Numerics;

public class WordSoundCard : BaseMatchCardV2, IPointerClickHandler
{
    [SerializeField] private TMP_Text text;
    private string wordKey;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Setup(int wordId, BaseMatchGameV2 game)
    {
        base.Setup(wordId, game);

        var wordData = game.GetWordById(wordId);
        var translation = wordData.translations.FirstOrDefault(t => t.languageId == 1);

        if (translation != null)
        {
            text.text = translation.text;

            wordKey = translation.text;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlaySound();
    }

    public void PlaySound()
    {
        if (string.IsNullOrEmpty(wordKey)) return;

        AudioManager.Instance.PlayWord(wordKey, AudioManager.LanguageType.Fi);
        Debug.Log("wordKey: " + wordKey);
    }
}