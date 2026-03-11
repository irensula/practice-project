using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class WordCardV2 : BaseMatchCardV2
{
    [SerializeField] private TMP_Text text;

    public override void Setup(int wordId, BaseMatchGameV2 game)
    {
        Debug.Log("WordCardV2 Setup called for wordId: " + wordId);
        base.Setup(wordId, game);

        var wordData = game.GetWordById(wordId);
        if (wordData != null)
        {
            var translation = wordData.translations.FirstOrDefault(t => t.languageId == 1);
            if (translation != null && text != null)
            {
                text.text = translation.text;
                Debug.Log($"SetText: {translation.text}");
            }
        }
    }
}