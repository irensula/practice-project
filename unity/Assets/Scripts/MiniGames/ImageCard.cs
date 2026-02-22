using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageCard : BaseMatchCard
{
    [SerializeField] private Image image;

    public void SetImage(Sprite sprite)
    {
        if (image != null)
            image.sprite = sprite;
    }
}