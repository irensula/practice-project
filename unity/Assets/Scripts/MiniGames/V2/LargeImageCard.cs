using UnityEngine;
using UnityEngine.UI;

public class LargeImageCard : BaseMatchCardV2
{
    [SerializeField] private Image image;

    public override void Setup(int wordId, BaseMatchGameV2 game)
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
    }
}
