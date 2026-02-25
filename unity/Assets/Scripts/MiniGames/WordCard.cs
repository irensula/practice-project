using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class WordCard : BaseMatchCard,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    [SerializeField] private TMP_Text text;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition;
    private Transform startParent;

    public void SetText(string value)
    {
        if (text != null)
            text.text = value;
    }

    protected override void Awake()
    {
        base.Awake();
        rectTransform = GetComponent<RectTransform>();

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        if (IsMatched) return;

        startPosition = rectTransform.anchoredPosition;
        startParent = transform.parent;

        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.blocksRaycasts = true;

        if (transform.parent == transform.root)
        {
            transform.SetParent(startParent);
            rectTransform.anchoredPosition = startPosition;
        }
    }

    public void ReturnToStart()
    {
        Debug.Log("ReturnToStart");
        transform.SetParent(startParent);
        rectTransform.anchoredPosition = startPosition;
    }
}
