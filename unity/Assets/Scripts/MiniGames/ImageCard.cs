using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageCard : MonoBehaviour, IPointerClickHandler
{
    private int wordId;
    private MatchGame manager;

    [SerializeField] private Image image;
    public void Setup(int id, MatchGame gameManager)
    {
        wordId = id;
        manager = gameManager;
    }

    public void SetImage(Sprite sprite)
    {
        if (image != null)
            image.sprite = sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        manager?.OnCardSelected(wordId);
    }
}