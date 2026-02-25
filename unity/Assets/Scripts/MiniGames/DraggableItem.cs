using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform startParent;
    private Vector2 startPosition;
    private Canvas canvas;
    private BaseMatchCard card;

    public int WordId => card.WordId; // expose WordId for DropSlot

    protected void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        card = GetComponent<BaseMatchCard>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        // save start position
        startPosition = rectTransform.anchoredPosition;
        startParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (card != null && card.IsMatched) 
            return;

        startParent = transform.parent;
        startPosition = rectTransform.anchoredPosition;

        transform.SetParent(canvas.transform);
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (transform.parent == canvas.transform)
        {
            transform.SetParent(startParent);
            rectTransform.anchoredPosition = startPosition;
        }
    }

    public void ReturnToStart()
    {
        transform.SetParent(startParent);
        rectTransform.anchoredPosition = startPosition;
    }

    public void SetMatched()
    {
        card.SetMatched();
    }
}