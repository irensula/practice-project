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
    private MatchGame matchGame;

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
        
        matchGame = card.MatchGame;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (card != null && card.IsMatched) 
            return;

        startParent = transform.parent;
        startPosition = rectTransform.anchoredPosition;

        card.SetSelected(true);

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

        card.SetSelected(false);
        
        matchGame?.ShowWrong(); // show "wrong" icon when the card returns
    }

    public void SetMatched()
    {
        card.SetMatched();
        canvasGroup.blocksRaycasts = false; // make it non draggable
        enabled = false;
    }

    public bool IsMatched => card != null && card.IsMatched;
}