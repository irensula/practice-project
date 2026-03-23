using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Numerics;

public class WordCardV2 : BaseMatchCardV2
{
    [SerializeField] private TMP_Text text;

    public override void Setup(int wordId, BaseMatchGameV2 game)
    {
        base.Setup(wordId, game);

        var wordData = game.GetWordById(wordId);

        var translation = wordData.translations.FirstOrDefault(t => t.languageId == 1);

        if (translation != null)
        {
            text.text = translation.text;
        }
    }
}
