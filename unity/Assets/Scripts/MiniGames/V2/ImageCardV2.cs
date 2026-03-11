using UnityEngine;
using UnityEngine.UI;

public class ImageCardV2 : BaseMatchCardV2
{
    [SerializeField] private Image image;
    [SerializeField] private Image overlayImage;

    public override void Setup(int wordId, BaseMatchGameV2 game)
    {
        Debug.Log("ImageCardV2 Setup called for wordId: " + wordId);
        base.Setup(wordId, game);

        var wordData = game.GetWordById(wordId);
        if (wordData != null && image != null)
        {
            Sprite sprite = Resources.Load<Sprite>(wordData.image.Replace(".jpg", "").Replace(".png", ""));
            if (sprite != null)
            {
                image.sprite = sprite;
                Debug.Log($"ImageCardV2: {wordData.image}");
            }
        }
    }
}
