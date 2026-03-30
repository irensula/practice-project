using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageCard : BaseMatchCard
{
    [SerializeField] private Image image;
    [SerializeField] private Image overlayImage;

    public void SetImage(Sprite sprite)
    {
        if (image != null)
            image.sprite = sprite;
    }

    public override void SetMatched()
    {
        base.SetMatched();
        overlayImage.gameObject.SetActive(true);
    }
}